using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MedicalDatabaseApplication
{
    class Patient
    {

        private string patientID;
        private string firstName;
        private string lastName;
        private string address;
        private string homePhone;
        private string mobilePhone;
        private string medicareNum;
        private string notes;
        private SQLHelper db;

        public string PatientID { get => patientID; set => patientID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Address { get => address; set => address = value; }
        public string HomePhone { get => homePhone; set => homePhone = value; }
        public string MobilePhone { get => mobilePhone; set => mobilePhone = value; }
        public string MedicareNum { get => medicareNum; set => medicareNum = value; }
        public string Notes { get => notes; set => notes = value; }
        internal SQLHelper Db { get => db; set => db = value; }
        

        public Patient()
        {
           Db = new SQLHelper();
        }

        public Patient(DataRow dr)
        {
            this.patientID = dr["patientID"].ToString();
            this.firstName = dr["firstName"].ToString();
            this.lastName = dr["lastName"].ToString();
            this.address = dr["address"].ToString();
            this.homePhone = dr["homePhone"].ToString();
            this.mobilePhone = dr["mobilePhone"].ToString();
            this.medicareNum = dr["medicareNum"].ToString();
            this.notes = dr["notes"].ToString();

            Db = new SQLHelper();
        }

        public void insertPatient()
        {
            string sql = "INSERT INTO Patient(firstName, lastName, address, homePhone, mobilePhone, medicareNum, notes) VALUES(@fN, @lN, @a, @hP, @mP, @mN, @n)";

            SqlParameter[] oP = new SqlParameter[7];
            oP[0] = new SqlParameter("@fN", SqlDbType.VarChar);
            oP[0].Value = firstName;
            oP[1] = new SqlParameter("@lN", SqlDbType.VarChar);
            oP[1].Value = lastName;
            oP[2] = new SqlParameter("@a", SqlDbType.VarChar);
            oP[2].Value = address;
            oP[3] = new SqlParameter("@hP", SqlDbType.VarChar);
            oP[3].Value = homePhone;
            oP[4] = new SqlParameter("@mP", SqlDbType.VarChar);
            oP[4].Value = mobilePhone;
            oP[5] = new SqlParameter("@mN", SqlDbType.VarChar);
            oP[5].Value = medicareNum;
            oP[6] = new SqlParameter("@n", SqlDbType.Text);
            oP[6].Value = notes;

            Db.NonQuerySQL(sql, oP);
        }

        public void updatePatient(string patID, string firstName, string lastName, string address, string homePhone, string mobilePhone, string medicareNum, string notes)
        {
            string sql = "UPDATE Patient SET firstName = @fN, lastName = @lN, address = @ad, homePhone = @hPh, mobilePhone = @mPh, medicareNum = @mN, notes = @n WHERE patientID = @pID";

            SqlParameter[] sp = new SqlParameter[8];
            sp[0] = new SqlParameter("@pID", SqlDbType.Int);
            sp[0].Value = patID;
            sp[1] = new SqlParameter("@fN", SqlDbType.VarChar);
            sp[1].Value = firstName;
            sp[2] = new SqlParameter("@lN", SqlDbType.VarChar);
            sp[2].Value = lastName;
            sp[3] = new SqlParameter("@ad", SqlDbType.VarChar);
            sp[3].Value = address;
            sp[4] = new SqlParameter("@hPh", SqlDbType.VarChar);
            sp[4].Value = homePhone;
            sp[5] = new SqlParameter("@mPh", SqlDbType.VarChar);
            sp[5].Value = homePhone;
            sp[6] = new SqlParameter("@mN", SqlDbType.VarChar);
            sp[6].Value = medicareNum;
            sp[7] = new SqlParameter("@n", SqlDbType.Text);
            sp[7].Value = notes;

            db.NonQuerySQL(sql, sp);
        }

        public bool deletePatient(string patID)
        {
            string sql = "DELETE FROM Patient WHERE patientID = @pID";

            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@pID", SqlDbType.Int);
            sp[0].Value = patID;

            Appointment app = new Appointment();
            bool noAppointment = app.SearchAppointment(patID);

            if (noAppointment) {
                db.NonQuerySQL(sql, sp);
                return true;
            }
            else { return false; }
        }
    }
}
