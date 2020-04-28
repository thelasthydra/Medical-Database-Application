using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDatabaseApplication {
    class AppointTime {

        private string startTime;
        private string endTime;
        private SQLHelper db;

        public string StartTime { get => startTime; set => startTime = value; }
        public string EndTime { get => endTime; set => endTime = value; }

        public AppointTime()
        {
            db = new SQLHelper();
        }

        public DataTable GetTimes()
        {
            string sql = "SELECT appointTimeStart, appointTimeEnd, timeOrder FROM AppointmentTime";

            DataTable dt = db.executeSQL(sql);

            return dt;
        }

    }
}
