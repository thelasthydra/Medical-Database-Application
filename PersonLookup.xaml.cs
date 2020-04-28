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

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MedicalDatabaseApplication
{
    public partial class PersonLookup : Window
    {

        public string patID;
        MainWindow mw;

        public PersonLookup(MainWindow _mw)
        {
            InitializeComponent();

            mw = _mw;

            // Makes sure the table is as up to date as possible 
            LoadTable();

            patID = "0";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient;

            // Checks to see if something actually has been selected
            if (dgOutput.SelectedIndex >= 0) {

                // Selects the patient selected so you can extract info
                selectedPatient = (Patient)dgOutput.SelectedItem;

                // Stores the patient ID in a variable 
                patID = selectedPatient.PatientID;

                // Sends the patient ID back to the Main Window so it can be used there
                mw.patID = this.patID;
                // Triggers the method "PatSearchPt2" so that the patient info can be filled out
                mw.PatSearchPt2();
                mw.Show();

                this.Close();

                ResetSearch();

            } else {
                // Informs the user that they need to have a patient selected when clicking this button
                MessageBox.Show("Please Select A Patient Or Cancel", "Error - No Patient Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {         

            bool del = false;
            MessageBoxResult msgResult = new MessageBoxResult();

            // Checks to see if you really want to delete the patient
            msgResult = MessageBox.Show("Are You Sure You Want To Delete This Patient?", "WARNING", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
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
                MessageBox.Show("Patient Has NOT Been Deleted", "Crisis Averted", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else {

                Patient selectedPatient;

                // Checks to see if a patient has actually been selected
                if (dgOutput.SelectedIndex >= 0) {

                    selectedPatient = (Patient)dgOutput.SelectedItem;
     
                    // Gets the selected patient's ID to be used when deleting
                    patID = selectedPatient.PatientID;

                    // Deletes the patient
                    Patient pat = new Patient();
                    bool didDelete = pat.deletePatient(patID);

                    // Refreshes the table so that the change can be seen
                    LoadTable();

                    if (!didDelete) {
                        MessageBox.Show("Patient Has Not Been Deleted As They Have Appointments", "The SAND", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        public void LoadTable() // This method is used to load the table to save lines
        { 
            Patients patients = new Patients();
            dgOutput.ItemsSource = patients;
        }

        public void LoadTable(string lastName) // Only shows the patient(s) with a matching last name
        {
            Patients patients = new Patients(lastName);
            dgOutput.ItemsSource = patients;
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e) // Changes the ghost text to actual text
        {
            if (txtSearch.Text == "Enter Patient Last Name") {
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
            txtSearch.Text = "Enter Patient Last Name";
            txtSearch.Foreground = Brushes.Gray;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Enter Patient Last Name" || txtSearch.Text == "") {
                MessageBox.Show("Please Enter A Last Name To Search By");
            } else {
                string lastName = txtSearch.Text;
                LoadTable(lastName);
                btnReset.Visibility = Visibility.Visible;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            LoadTable();
            btnReset.Visibility = Visibility.Hidden;
        }
    }
}
