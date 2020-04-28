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
    class Doctors : List<Doctor>
    {

        public Doctors()
        {
            SQLHelper database = new SQLHelper();

            string sql = "SELECT docID, firstName, lastName, address, homePhone, mobilePhone, mrn, docType FROM Doctor";

            DataTable dt = database.executeSQL(sql);

            foreach (DataRow dr in dt.Rows) {
                Doctor newDoc = new Doctor(dr);
                this.Add(newDoc);
            }
        }

        public Doctors(string pracLastName)
        {
            SQLHelper database = new SQLHelper();

            string sql = "SELECT docID, firstName, lastName, address, homePhone, mobilePhone, mrn, docType FROM Doctor WHERE lastName LIKE @lN ORDER BY lastName";

            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@lN", SqlDbType.VarChar);
            parameter[0].Value = "%" + pracLastName + "%";

            DataTable dt = database.executeSQL(sql, parameter);

            foreach (DataRow dr in dt.Rows) {
                Doctor newDoc = new Doctor(dr);
                this.Add(newDoc);
            }
        }
    }
}
