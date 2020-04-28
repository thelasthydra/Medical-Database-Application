using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MedicalDatabaseApplication {
    /// <summary>
    /// Interaction logic for DocLookup.xaml
    /// </summary>
    public partial class DocLookup : Window {

        public int docID;
        MainWindow mw;

        public DocLookup(MainWindow _mw)
        {
            InitializeComponent();

            mw = _mw;

            LoadTable();
        }

        public void LoadTable()
        {
            Doctors docs = new Doctors();
            dgOutput.ItemsSource = docs;
        }

        public void LoadTable(string lastName) // Only shows the patient(s) with a matching last name
        {
            Doctors docs = new Doctors(lastName);
            dgOutput.ItemsSource = docs;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            Doctor selectedDoc;

            // Checks to see if something actually has been selected
            if (dgOutput.SelectedIndex >= 0) {

                // Selects the patient selected so you can extract info
                selectedDoc = (Doctor)dgOutput.SelectedItem;

                // Stores the patient ID in a variable 
                docID = selectedDoc.DocID;

                // Sends the patient ID back to the Main Window so it can be used there
                mw.docID = this.docID;

                // Triggers the method "PatSearchPt2" so that the patient info can be filled out
                mw.Show();

                mw.PracSearchPt2();

                this.Close();

                ResetSearch();
            }
            else {
                // Informs the user that they need to have a patient selected when clicking this button
                MessageBox.Show("Please Select A Patient Or Cancel", "Error - No Patient Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            bool del = false;
            MessageBoxResult msgResult = new MessageBoxResult();

            // Checks to see if you really want to delete the patient
            msgResult = MessageBox.Show("Are You Sure You Want To Delete This Doctor?", "WARNING", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            switch (msgResult) {
                case MessageBoxResult.Yes:
                    del = true;
                    break;
                case MessageBoxResult.No:
                    del = false;
                    break;
            }
            if (!del) {
                // If you don't wish to delete the patient it informs you that that hasn't happened yet
                MessageBox.Show("Doctor Has NOT Been Deleted", "Crisis Averted", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else {

                Doctor selectedDoc;

                // Checks to see if a patient has actually been selected
                if (dgOutput.SelectedIndex >= 0) {

                    bool didDelete;

                    selectedDoc = (Doctor)dgOutput.SelectedItem;

                    // Gets the selected patient's ID to be used when deleting
                    docID = selectedDoc.DocID;

                    // Deletes the patient
                    Doctor doc = new Doctor();
                    didDelete = doc.DeleteDoctor(docID);

                    // Refreshes the table so that the change can be seen
                    LoadTable();

                    if (!didDelete) {
                        MessageBox.Show("Doctor Has Not Been Deleted As They Have Appointments", "The SAND", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Enter Doctor Last Name" || txtSearch.Text == "") {
                MessageBox.Show("Please Enter A Last Name To Search By");
            }
            else {
                string lastName = txtSearch.Text;
                LoadTable(lastName);
                btnReset.Visibility = Visibility.Visible;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Enter Doctor Last Name") {
                txtSearch.Text = "";
                txtSearch.Foreground = Brushes.Black;
            }
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "") {
                ResetSearch();
            }
        }

        private void ResetSearch() // Changes from no text back to ghost text
        {
            txtSearch.Text = "Enter Doctor Last Name";
            txtSearch.Foreground = Brushes.Gray;
        }
    }
}
