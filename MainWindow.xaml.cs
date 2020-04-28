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
using System.Windows.Navigation;
using System.Windows.Shapes;

// Database Stuff
using System.Text.RegularExpressions;
using System.Data;

namespace MedicalDatabaseApplication {

    public partial class MainWindow : Window {

        public string patID;
        public int docID;
        PersonLookup patSearch;
        DocLookup docSearch;
        Match match;
        bool validInputs = false;
        DateTime min = DateTime.Today;
        DateTime max = DateTime.Today.AddMonths(3);

        public MainWindow()
        {
            InitializeComponent();

            patSearch = new PersonLookup(this);

            // Populates The Doctor Type Combo Box
            DocType dType = new DocType();
            cmboDocType.SelectedValuePath = "typeName";
            cmboDocType.DisplayMemberPath = "typeName";
            cmboDocType.ItemsSource = dType.GetDocTypes().DefaultView;

            // Populates The Doctor Availability List
            DocAvail da = new DocAvail();
            lvDocAvail.ItemsSource = da.GetDays().DefaultView;

            // Populates The Appointment Time Combo Box
            AppointTime at = new AppointTime();
            cmboTimes.SelectedValuePath = "timeOrder"; 
            cmboTimes.DisplayMemberPath = "appointTimeStart";
            cmboTimes.ItemsSource = at.GetTimes().DefaultView;

            // Changes The First & Last Date Displayed
            calAppointment.DisplayDateStart = min;
            calAppointment.DisplayDateEnd = max;
        }

        private void btnPatAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgResult = new MessageBoxResult();

            validInputs = CheckPatInputs();

            if (validInputs) {

                // Checks to see if the user really wants to add a patient and didn't accidently hit the add button
                msgResult = MessageBox.Show("Are You Sure You Want To Add This Patient?", "Are You Sure?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                // Checking what button was pushed on the previous message box
                if (msgResult == MessageBoxResult.Yes) {
                    Patient pat = new Patient();

                    // Sends all the info in the textboxes to the patient class for further use
                    pat.FirstName = txtfirstName.Text.Trim();
                    pat.LastName = txtlastName.Text.Trim();
                    pat.Address = txtaddress.Text.Trim();
                    pat.HomePhone = txtHomePhone.Text.Trim();
                    pat.MobilePhone = txtMobile.Text.Trim();
                    pat.MedicareNum = txtMedicare.Text.Trim();
                    pat.Notes = txtNotes.Text.Trim();

                    // Inserts the patient's info into the database
                    pat.insertPatient();

                    // Confirmation message
                    MessageBox.Show("Patient Added", "The SAND", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else {
                    // Confirms that no patient was added
                    MessageBox.Show("Patient Not Added", "The SAND", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else {
                MessageBox.Show("One or More Inputs Are Invalid, Please Try Again", "The SAND", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPatSearch_Click(object sender, RoutedEventArgs e)
        {
            OpenPatLookup();
        }

        public void PatSearchPt2()
        {
            DataGrab pDG = new DataGrab();

            // Defines the variables
            string firstName, lastName, address, medicare, homePh, mobilePh, notes;

            // Puts values in the variables
            firstName = pDG.findFirstName(patID);
            lastName = pDG.findLastName(patID);
            address = pDG.findAddress(patID);
            medicare = pDG.findMedicare(patID);
            homePh = pDG.findHomePhone(patID);
            mobilePh = pDG.findMobilePhone(patID);
            notes = pDG.findNotes(patID);

            // Puts the variable values in the textboxes
            txtfirstName.Text = firstName;
            txtlastName.Text = lastName;
            txtaddress.Text = address;
            txtMedicare.Text = medicare;
            txtHomePhone.Text = homePh;
            txtMobile.Text = mobilePh;
            txtNotes.Text = notes;

            string fullName = firstName + " " + lastName;
            txtAppPatientName.Text = fullName;

            txtAppPatientName.IsEnabled = false;
        }

        private void btnPatEdit_Click(object sender, RoutedEventArgs e)
        {
            Patient pat = new Patient();

            validInputs = CheckPatInputs();

            if (validInputs) {
                // Makes sure that there is a patient selected to edit the info of
                if (patID != "0") {

                    string firstName, lastName, address, medicare, homePh, mobilePh, notes;

                    firstName = txtfirstName.Text;
                    lastName = txtlastName.Text;
                    address = txtaddress.Text;
                    medicare = txtMedicare.Text;
                    homePh = txtHomePhone.Text;
                    mobilePh = txtMobile.Text;
                    notes = txtNotes.Text;

                    // Sends the info the the Patient class to be updated with all the relevant infomation
                    pat.updatePatient(patID, firstName, lastName, address, homePh, mobilePh, medicare, notes);

                    // Lets the user know that details have been updated
                    MessageBox.Show("Patient Details Updated", "Updating Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else {
                    // Tells the user that no patient has been selected so there is nothing to update
                    MessageBox.Show("ERROR: Please Have A Patient Selected To Edit", "Patient Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else {
                MessageBox.Show("One or More Fields Have Incorrect Inputs, Please Try Again", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPatClear_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Closes all windows related to this program when the main window is closed
            // Stops multiple instances from being open at once without the user knowing
            App.Current.Shutdown();
        }

        public bool CheckPatInputs()
        {
            Checker c = new Checker();
            match = c.CheckName(txtfirstName.Text);
            if (!match.Success) {
                txtfirstName.Background = Brushes.Pink;
                return false;
            }
            else { txtfirstName.Background = Brushes.White; }

            match = c.CheckName(txtlastName.Text);
            if (!match.Success) {
                txtlastName.Background = Brushes.Pink;
                return false;
            }
            else { txtlastName.Background = Brushes.White; }

            match = c.CheckAddress(txtaddress.Text);
            if (!match.Success) {
                txtaddress.Background = Brushes.Pink;
                return false;
            }
            else { txtaddress.Background = Brushes.White; }

            match = c.CheckMedicare(txtMedicare.Text);
            if (!match.Success) {
                txtMedicare.Background = Brushes.Pink;
                return false;
            }
            else { txtMedicare.Background = Brushes.White; }

            match = c.CheckPhone(txtHomePhone.Text);
            if (!match.Success) {
                txtHomePhone.Background = Brushes.Pink;
                return false;
            }
            else { txtHomePhone.Background = Brushes.White; }

            match = c.CheckPhone(txtMobile.Text);
            if (!match.Success) {
                txtMobile.Background = Brushes.Pink;
                return false;
            }
            else { txtMobile.Background = Brushes.White; }

            // Everything Matched The Set Patterns 
            return true;
        }

        public bool CheckDocInputs()
        {
            Checker c = new Checker();
            match = c.CheckName(txtpracfirstName.Text);
            if (!match.Success) {
                txtpracfirstName.Background = Brushes.Pink;
                return false;
            }
            else { txtpracfirstName.Background = Brushes.White; }

            match = c.CheckName(txtpraclastName.Text);
            if (!match.Success) {
                txtpraclastName.Background = Brushes.Pink;
                return false;
            }
            else { txtpraclastName.Background = Brushes.White; }

            match = c.CheckAddress(txtpracaddress.Text);
            if (!match.Success) {
                txtpracaddress.Background = Brushes.Pink;
                return false;
            }
            else { txtpracaddress.Background = Brushes.White; }

            match = c.CheckMRN(txtpracMRN.Text);
            if (!match.Success) {
                txtpracMRN.Background = Brushes.Pink;
                return false;
            }
            else { txtpracMRN.Background = Brushes.White; }

            match = c.CheckPhone(txtpracHomePhone.Text);
            if (!match.Success) {
                txtpracHomePhone.Background = Brushes.Pink;
                return false;
            }
            else { txtpracHomePhone.Background = Brushes.White; }

            match = c.CheckPhone(txtpracMobile.Text);
            if (!match.Success) {
                txtpracMobile.Background = Brushes.Pink;
                return false;
            }
            else { txtpracMobile.Background = Brushes.White; }

            if (lvDocAvail.SelectedIndex == -1) {
                lvDocAvail.Background = Brushes.Pink;
                return false;
            }
            else { lvDocAvail.Background = Brushes.White; }

            if (cmboDocType.SelectedIndex == -1) {
                cmboDocType.Background = Brushes.Pink;
                return false;
            }
            else { cmboDocType.Background = Brushes.Gray; }

            // Everything Matched The Set Patterns 
            return true;
        }

        private void BtnPracAdd_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult mbr = new MessageBoxResult();

            // Checks to See If The Inputs Are Valid
            validInputs = CheckDocInputs();

            // If They Are Continue
            if (validInputs) {
                mbr = MessageBox.Show("Are You Sure You Want To Add This Doctor?", "The SAND", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                string dType = GetDocType();

                if (mbr == MessageBoxResult.Yes) {
                    Doctor doc = new Doctor();

                    string[] availDays = GetAvailDays();

                    // Sets Variables For Insertion
                    doc.FirstName = txtpracfirstName.Text.Trim();
                    doc.LastName = txtpraclastName.Text.Trim();
                    doc.Address = txtpracaddress.Text.Trim();
                    doc.HomePhone = txtpracHomePhone.Text.Trim();
                    doc.MobilePhone = txtpracMobile.Text.Trim();
                    doc.Mrn = txtpracMRN.Text.Trim();
                    doc.DocType = dType;

                    // Triggers Insertion
                    doc.InsertDoctor(availDays);
                    this.docID = doc.DocID;

                    MessageBox.Show("Doctor Has Been Added", "The SAND", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else { MessageBox.Show("One or More Inputs Are Invalid, Please Check To Make Sure All Are Correct", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void BtnPracEdit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = new MessageBoxResult();

            validInputs = CheckDocInputs();

            if (validInputs) {

                mbr = MessageBox.Show("Are You Sure You Want To Update The Current Doctor?", "The SAND", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                if (mbr == MessageBoxResult.Yes) { 

                    Doctor doc = new Doctor();

                    string dType = GetDocType();

                    string[] availDays = GetAvailDays();

                    doc.FirstName = txtpracfirstName.Text.Trim();
                    doc.LastName = txtpraclastName.Text.Trim();
                    doc.Address = txtpracaddress.Text.Trim();
                    doc.HomePhone = txtpracHomePhone.Text.Trim();
                    doc.MobilePhone = txtpracMobile.Text.Trim();
                    doc.Mrn = txtpracMRN.Text.Trim();
                    doc.DocType = dType;

                    doc.UpdateDoctor(docID, availDays);

                    MessageBox.Show("The Doctor Has Been Updated Successfully", "The SAND", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnPracSearch_Click(object sender, RoutedEventArgs e)
        {
            OpenDocLookup();
        }

        public void PracSearchPt2()
        {
            DataGrab dDG = new DataGrab();
            
            txtpracfirstName.Text = dDG.findFirstName(docID);
            txtpraclastName.Text = dDG.findLastName(docID);
            txtpracaddress.Text = dDG.findAddress(docID);
            txtpracHomePhone.Text = dDG.findHomePhone(docID);
            txtpracMobile.Text = dDG.findMobilePhone(docID);
            txtpracMRN.Text = dDG.findMRN(docID);
            cmboDocType.Text = dDG.findDocType(docID);

            string fullName = txtpracfirstName.Text + " " + txtpraclastName.Text;

            txtAppDocName.Text = fullName;

            if (tabs.SelectedIndex == 2) {
                calAppointment.IsEnabled = true;

                DocAvail da = new DocAvail();
                DataTable dt;
                dt = da.GetAvailability(docID);

                foreach (DataRow dr in dt.Rows) {
                    for (DateTime day = min; day <= max; day = day.AddDays(1)) {
                        if (day.DayOfWeek.ToString() == dr["nameofDay"].ToString()) {
                            calAppointment.BlackoutDates.Add(new CalendarDateRange(day));
                        }
                    }
                }

                txtAppDocName.IsEnabled = false;

            }
        }

        private string GetDocType()
        {
            // Puts The Selected DocType Into A String Variable
            DataRowView drv1 = (DataRowView)cmboDocType.SelectedItem;
            string dType = drv1.Row["typeName"].ToString();

            return dType;
        }

        private string[] GetAvailDays()
        {
            // Stuff Setup For The Loop Below
            string[] availDays = new string[lvDocAvail.SelectedItems.Count];
            int i = 0;

            // Converts The Selected Days To A String Array
            foreach (DataRowView drv2 in lvDocAvail.SelectedItems) {
                availDays[i] = drv2.Row["nameofDay"].ToString().Trim();
                i++;
            }

            return availDays;
        }

        private void txtAppPatientName_GotFocus(object sender, RoutedEventArgs e)
        {
            OpenPatLookup();
        }

        private void txtAppDocName_GotFocus(object sender, RoutedEventArgs e)
        {
            OpenDocLookup();
        }

        private void OpenPatLookup()
        {
            bool lookupOpen;
            lookupOpen = false;

            // Checks to see if the Patient Lookup window is open
            foreach (Window openWin in Application.Current.Windows) {
                if (openWin.GetType().Name == "PersonLookup") {
                    lookupOpen = true;
                    break;
                }
            }

            if (lookupOpen) {
                // Reloads the datagrid table so that any new patients added or updated information will appear 
                patSearch.LoadTable();

                // If the Patient Lookup window is open then it will just show it
                patSearch.Show();
            }
            else {
                // Otherwise it will create a new instance of the window
                PersonLookup patSearch = new PersonLookup(this);
                // Then open it
                patSearch.Show();
            }
            // Gets the patients ID from the Patient Lookup table for further use
            this.patID = patSearch.patID;
        }

        private void OpenDocLookup()
        {
            bool lookupOpen;
            lookupOpen = false;

            // Checks to see if the Patient Lookup window is open
            foreach (Window openWin in Application.Current.Windows) {
                if (openWin.GetType().Name == "DocLookup") {
                    lookupOpen = true;
                    break;
                }
            }

            if (lookupOpen) {
                // Reloads the datagrid table so that any new patients added or updated information will appear 
                docSearch.LoadTable();

                // If the Patient Lookup window is open then it will just show it
                docSearch.Show();
            }
            else {
                // Otherwise it will create a new instance of the window
                DocLookup docSearch = new DocLookup(this);
                // Then open it
                docSearch.Show();
            }
        }

        private void ClearAll()
        {
            // Clears everything
            patID = "0";
            txtfirstName.Text = "";
            txtlastName.Text = "";
            txtaddress.Text = "";
            txtMedicare.Text = "";
            txtHomePhone.Text = "";
            txtMobile.Text = "";
            txtNotes.Text = "";

            docID = 0;
            txtpracfirstName.Text = "";
            txtpraclastName.Text = "";
            txtpracaddress.Text = "";
            txtpracHomePhone.Text = "";
            txtpracMobile.Text = "";
            txtpracMRN.Text = "";
            cmboDocType.Text = "";
            lvDocAvail.UnselectAll();
        }

        private void BtnPracClear_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void LvDocAvail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void CmboDocType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabs.SelectedIndex == 0 || tabs.SelectedIndex == 1) {
                ClearAll();
            }
            else if (tabs.SelectedIndex == 2) {
                ClearAppointment();
            }
        }

        private void CmboTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void BtnAppConfirm_Click(object sender, RoutedEventArgs e)
        {
            validInputs = CheckAppInputs();

            if (validInputs) {
                Appointment app = new Appointment();

                string appointStart, appointEnd, appointDate;

                DataRowView drv = (DataRowView)cmboTimes.SelectedItem;
                appointStart = drv.Row["appointTimeStart"].ToString();
                appointEnd = drv.Row["appointTimeEnd"].ToString();

                appointDate = calAppointment.SelectedDate.Value.ToShortDateString();

                app.InsertAppointment(docID, appointDate, appointStart, appointEnd, patID);

                MessageBox.Show("Appointment Successfully Added", "The Sand", MessageBoxButton.OK, MessageBoxImage.Information);
            } else { MessageBox.Show("One or More Inputs Are Wrong, Please Try Again", "The SAND", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private bool CheckAppInputs()
        {
            if (patID == "") {
                txtAppPatientName.Background = Brushes.Pink;
                return false;
            }
            else { txtAppPatientName.Background = Brushes.White; }

            if (docID < 0) {
                txtAppDocName.Background = Brushes.Pink;
                return false;
            } else { txtAppDocName.Background = Brushes.White; }

            if (!calAppointment.SelectedDate.HasValue) {
                calAppointment.Background = Brushes.Pink;
                return false;
            }
            else { calAppointment.Background = Brushes.White; }

            if (cmboTimes.SelectedIndex < 0) {
                return false;
            }

            return true;
        }

        private void BtnAppReset_Click(object sender, RoutedEventArgs e)
        {
            ClearAppointment();
        }

        private void ClearAppointment()
        {
            patID = "";
            docID = -1;

            txtAppDocName.Text = txtAppPatientName.Text = "";
            txtAppDocName.Background = txtAppPatientName.Background = Brushes.White;

            txtAppPatientName.IsEnabled = true;
            txtAppDocName.IsEnabled = true;

            calAppointment.BlackoutDates.Clear();
            calAppointment.IsEnabled = false;

            cmboTimes.Text = "";
        }
    }
};