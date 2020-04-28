using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace MedicalDatabaseApplication
{
    class DocAvail
    {

        internal SQLHelper database;

        public DocAvail()
        {
            database = new SQLHelper();
        }

        public DataTable GetDays()
        {
            // Selects All Days But The Weekend
            string sql = "SELECT nameofDay FROM DaysofWeek " +
                            "WHERE nameofDay NOT IN (SELECT nameofDay FROM DaysofWeek WHERE nameofDay Like 'S%') " +
                            "ORDER BY dayOrder";

            // Selects Entire Week
            //string sql = "SELECT nameofDay FROM DaysofWeek ORDER BY dayOrder";

            // Only Selects Weekends
            //string sql = "SELECT nameofDay FROM DaysofWeek WHERE nameofDay Like 'S%' ORDER BY dayOrder";

            DataTable dt = database.executeSQL(sql);

            return dt;
        }

        public DataTable GetAvailability(int docID)
        {
            string sql = "SELECT nameofDay FROM DaysofWeek WHERE nameofDay NOT IN " +
                "(SELECT dayAvailable FROM Available WHERE docID = @dID)";

            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@dID", SqlDbType.Int);
            sp[0].Value = docID;

            DataTable dt = database.executeSQL(sql, sp);

            return dt;
        }
    }
}
