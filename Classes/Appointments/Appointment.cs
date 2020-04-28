using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace MedicalDatabaseApplication
{
    class Appointment
    {

        SQLHelper db;

        public Appointment()
        {
            db = new SQLHelper();
        }

        public void InsertAppointment(int docID, string appointmentDate, string timeStart, string timeEnd, string patID)
        {
            string sql = "INSERT INTO Appointment (docID, appointDay, appointTimeStart, appointTimeEnd, patientID) " +
                "VALUES (@dID, @aPD, @aTS, @aTE, @pID)";

            SqlParameter[] sp = new SqlParameter[5];
            sp[0] = new SqlParameter("@dID", SqlDbType.Int);
            sp[0].Value = docID;
            sp[1] = new SqlParameter("@aPD", SqlDbType.Date);
            sp[1].Value = appointmentDate;
            sp[2] = new SqlParameter("@aTS", SqlDbType.VarChar);
            sp[2].Value = timeStart;
            sp[3] = new SqlParameter("@aTE", SqlDbType.VarChar);
            sp[3].Value = timeEnd;
            sp[4] = new SqlParameter("@pID", SqlDbType.VarChar);
            sp[4].Value = patID;

            db.NonQuerySQL(sql, sp);
        }

        public bool SearchAppointment(int docID)
        {
            string sql = "SELECT COUNT(docID) FROM Appointment WHERE docID = @dID";

            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@dID", SqlDbType.Int);
            sp[0].Value = docID;

            int rows = (int)db.scalarSQL(sql, sp);

            if (rows == 0) {
                return true;
            } else { return false; }
        }

        public bool SearchAppointment(string patID)
        {
            string sql = "SELECT COUNT(patientID) FROM Appointment WHERE patientID = @pID";

            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@pID", SqlDbType.VarChar);
            sp[0].Value = patID;

            int rows = (int)db.scalarSQL(sql, sp);

            if (rows == 0) {
                return true;
            }
            else { return false; }
        }
    }
}
