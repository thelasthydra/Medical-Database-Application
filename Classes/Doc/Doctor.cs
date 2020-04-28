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
    class Doctor
    {

        private int docID;
        private string firstName;
        private string lastName;
        private string address;
        private string homePhone;
        private string mobilePhone;
        private string mrn;
        private string docType;

        private SQLHelper db;

        public int DocID { get => docID; set => docID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Address { get => address; set => address = value; }
        public string HomePhone { get => homePhone; set => homePhone = value; }
        public string MobilePhone { get => mobilePhone; set => mobilePhone = value; }
        public string Mrn { get => mrn; set => mrn = value; }
        public string DocType { get => docType; set => docType = value; }
        internal SQLHelper Db { get => db; set => db = value; }

        public Doctor()
        {
            Db = new SQLHelper();
        }

        public Doctor(DataRow dr)
        {
            this.docID = (int)dr["docID"];
            this.firstName = dr["firstName"].ToString();
            this.lastName = dr["lastName"].ToString();
            this.address = dr["address"].ToString();
            this.homePhone = dr["homePhone"].ToString();
            this.mobilePhone = dr["mobilePhone"].ToString();
            this.mrn = dr["mrn"].ToString();
            this.docType = dr["docType"].ToString();

            Db = new SQLHelper();
        }

        public void InsertDoctor(string[] availDays)
        {

            /// Inserts A New Doctor Into The Table

            string sql = "INSERT INTO Doctor(firstName, lastName, address, homePhone, mobilePhone, mrn, docType) VALUES(@fN, @lN, @a, @hP, @mP, @mrn, @dType)";

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
            oP[5] = new SqlParameter("@mrn", SqlDbType.VarChar);
            oP[5].Value = mrn;
            oP[6] = new SqlParameter("@dType", SqlDbType.VarChar);
            oP[6].Value = docType;

            Db.NonQuerySQL(sql, oP);

            sql = "SELECT docID FROM Doctor WHERE " +
                "firstName = @fn " +
                "AND lastName = @lN " +
                "AND address = @a " +
                "AND homePhone = @hP " +
                "AND mobilePhone = @mP " +
                "AND mrn = @mrn " +
                "AND docType = @dType";

            SqlParameter[] sp = new SqlParameter[7];
            sp[0] = new SqlParameter("@fN", SqlDbType.VarChar);
            sp[0].Value = firstName;
            sp[1] = new SqlParameter("@lN", SqlDbType.VarChar);
            sp[1].Value = lastName;
            sp[2] = new SqlParameter("@a", SqlDbType.VarChar);
            sp[2].Value = address;
            sp[3] = new SqlParameter("@hP", SqlDbType.VarChar);
            sp[3].Value = homePhone;
            sp[4] = new SqlParameter("@mP", SqlDbType.VarChar);
            sp[4].Value = mobilePhone;
            sp[5] = new SqlParameter("@mrn", SqlDbType.VarChar);
            sp[5].Value = mrn;
            sp[6] = new SqlParameter("@dType", SqlDbType.VarChar);
            sp[6].Value = docType;

            docID = (int)db.scalarSQL(sql, sp);

            DeleteAvail(docID);
            InsertAvail(docID, availDays);
        }

        public void UpdateDoctor(int docID, string[] availDays)
        {
            

            /// Updates The Doctors Info

            string sql = "UPDATE Doctor SET firstName = @fN, lastName = @lN, address = @ad, homePhone = @hPh, mobilePhone = @mPh, mrn = @mrn, docType = @dType " +
                "WHERE docID = @dID";

            SqlParameter[] sp = new SqlParameter[8];
            sp[0] = new SqlParameter("@dID", SqlDbType.Int);
            sp[0].Value = docID;
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
            sp[6] = new SqlParameter("@mrn", SqlDbType.VarChar);
            sp[6].Value = mrn;
            sp[7] = new SqlParameter("@dType", SqlDbType.VarChar);
            sp[7].Value = docType;

            db.NonQuerySQL(sql, sp);

            DeleteAvail(docID);
            InsertAvail(docID, availDays);
        }

        public bool DeleteDoctor(int docID)
        {

            /// Deletes The Doctor From The Doctor Table

            string sql = "DELETE FROM Doctor WHERE docID = @dID";

            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@dID", SqlDbType.Int);
            sp[0].Value = docID;

            DeleteAvail(docID);

            Appointment app = new Appointment();
            bool noAppointment = app.SearchAppointment(docID);

            if (noAppointment) {
                db.NonQuerySQL(sql, sp);
                return true;
            }
            else { return false; }
        }

        private void DeleteAvail(int docID)
        {

            /// Deletes All Mentions Of Doc In The Availability Table 
            /// So It Can Update The Available Days & Insert New Ones
            /// Or Delete The Doctor

            string sql = "DELETE FROM Available WHERE docID = @dID";

            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@dID", SqlDbType.Int);
            sp[0].Value = docID;

            db.NonQuerySQL(sql, sp);
        }

        private void InsertAvail(int docID, string[] days)
        {

            /// Inserts The Doctor's Availability Into The Available Table

            string sql = "INSERT INTO Available(docID, dayAvailable) VALUES(@dID, @dA)";

            foreach (string day in days)
            {
                SqlParameter[] sp = new SqlParameter[2];
                sp[0] = new SqlParameter("@dID", SqlDbType.Int);
                sp[0].Value = docID;
                sp[1] = new SqlParameter("@dA", SqlDbType.VarChar);
                sp[1].Value = day;
                db.NonQuerySQL(sql, sp);
            }            
        }
    }
}
