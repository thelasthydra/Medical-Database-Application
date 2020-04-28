using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDatabaseApplication
{
    class DocType
    {
        private string typeName;
        private string typeDesc;

        private SQLHelper db;

        public string TypeName { get => typeName; set => typeName = value; }
        public string TypeDesc { get => typeDesc; set => typeDesc = value; }
        internal SQLHelper Db { get => db; set => db = value; }

        public DocType()
        {
            db = new SQLHelper();
        }

        public DocType(DataRow dr)
        {
            this.typeName = dr["typeName"].ToString();

            db = new SQLHelper();
        }

        public DataTable GetDocTypes()
        {
            SQLHelper database = new SQLHelper();

            string sql = "SELECT typeName FROM DocType";

            DataTable dt = database.executeSQL(sql);

            return dt;
        }

    }
}
