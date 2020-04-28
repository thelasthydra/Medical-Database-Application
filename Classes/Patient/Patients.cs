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
    class Patients : List<Patient>
    {

        public Patients()
        {

            SQLHelper database = new SQLHelper();

            string sql = "SELECT patientID, firstName, lastName, address, homePhone, mobilePhone, medicareNum, notes FROM Patient";

            DataTable dt = database.executeSQL(sql);

            foreach (DataRow dr in dt.Rows) {
                Patient newPat = new Patient(dr);
                this.Add(newPat);
            }
        }

        public Patients(string patLastName)
        {
            SQLHelper database = new SQLHelper();

            string sql = "SELECT patientID, firstName, lastName, address, homePhone, mobilePhone, medicareNum, notes FROM Patient WHERE lastName LIKE @lN ORDER BY lastName";

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@lN", SqlDbType.VarChar);
            parameter[0].Value = "%" + patLastName + "%";

            DataTable dt = database.executeSQL(sql, parameter);

            foreach (DataRow dr in dt.Rows) {
                Patient newPat = new Patient(dr);
                this.Add(newPat);
            }

        }
    }
}
