using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDatabaseApplication {
    class DataGrab{

        private SQLHelper db;

        public DataGrab()
        {
            db = new SQLHelper();
        }

        public string findFirstName(string id)
        {
            string firstName;

            string sql = "SELECT firstName FROM Patient WHERE patientID = @id";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@id", SqlDbType.VarChar);
            sqlParam[0].Value = id;

            firstName = (string)db.scalarSQL(sql, sqlParam);

            return firstName;
        }

        public string findLastName(string id)
        {
            string lastName;

            string sql = "SELECT lastName FROM Patient WHERE patientID = @id";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@id", SqlDbType.VarChar);
            sqlParam[0].Value = id;

            lastName = (string)db.scalarSQL(sql, sqlParam);

            return lastName;
        }

        public string findAddress(string id)
        {
            string address;

            string sql = "SELECT address FROM Patient WHERE patientID = @id";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@id", SqlDbType.VarChar);
            sqlParam[0].Value = id;

            address = (string)db.scalarSQL(sql, sqlParam);

            return address;
        }

        public string findMedicare(string id)
        {
            string medicare;

            string sql = "SELECT medicareNum FROM Patient WHERE patientID = @id";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@id", SqlDbType.VarChar);
            sqlParam[0].Value = id;

            medicare = (string)db.scalarSQL(sql, sqlParam);

            return medicare;
        }

        public string findHomePhone(string id)
        {
            string homePh;

            string sql = "SELECT homePhone FROM Patient WHERE patientID = @id";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@id", SqlDbType.VarChar);
            sqlParam[0].Value = id;

            homePh = (string)db.scalarSQL(sql, sqlParam);

            return homePh;
        }

        public string findMobilePhone(string id)
        {
            string mobilePh;

            string sql = "SELECT mobilePhone FROM Patient WHERE patientID = @id";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@id", SqlDbType.VarChar);
            sqlParam[0].Value = id;

            mobilePh = (string)db.scalarSQL(sql, sqlParam);

            return mobilePh;
        }

        public string findNotes(string id)
        {
            string notes;

            string sql = "SELECT notes FROM Patient WHERE patientID = @id";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@id", SqlDbType.VarChar);
            sqlParam[0].Value = id;

            notes = (string)db.scalarSQL(sql, sqlParam);

            return notes;
        }


        public string findFirstName(int id)
        {
            string name;

            string sql = "SELECT firstName FROM Doctor WHERE docID = @id";

            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@id", SqlDbType.Int);
            sp[0].Value = id;

            name = (string)db.scalarSQL(sql, sp);

            return name;
        }

        public string findLastName(int id)
        {
            string name;

            string sql = "SELECT lastName FROM Doctor WHERE docID = @id";

            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@id", SqlDbType.Int);
            sp[0].Value = id;

            name = (string)db.scalarSQL(sql, sp);

            return name;
        }

        public string findAddress(int id)
        {
            string address;

            string sql = "SELECT address FROM Doctor WHERE docID = @id";

            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@id", SqlDbType.Int);
            sp[0].Value = id;

            address = (string)db.scalarSQL(sql, sp);

            return address;
        }

        public string findHomePhone(int id)
        {
            string homeP;

            string sql = "SELECT homePhone FROM Doctor WHERE docID = @id";

            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@id", SqlDbType.Int);
            sp[0].Value = id;

            homeP = (string)db.scalarSQL(sql, sp);

            return homeP;
        }

        public string findMobilePhone(int id)
        {
            string mobile;

            string sql = "SELECT mobilePhone FROM Doctor WHERE docID = @id";

            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@id", SqlDbType.Int);
            sp[0].Value = id;

            mobile = (string)db.scalarSQL(sql, sp);

            return mobile;
        }

        public string findMRN(int id)
        {
            string mrn;

            string sql = "SELECT mrn FROM Doctor WHERE docID = @id";

            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@id", SqlDbType.Int);
            sp[0].Value = id;

            mrn = (string)db.scalarSQL(sql, sp);

            return mrn;
        }

        public string findDocType(int id)
        {
            string dType;

            string sql = "SELECT docType FROM Doctor WHERE docID = @id";

            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@id", SqlDbType.Int);
            sp[0].Value = id;

            dType = (string)db.scalarSQL(sql, sp);

            return dType;
        }
    }
}
