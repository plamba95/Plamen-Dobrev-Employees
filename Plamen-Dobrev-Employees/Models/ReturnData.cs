using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plamen_Dobrev_Employees.Models
{
    public class ReturnData
    {
        public int Empl1ID { get; set; }
        public int Empl2ID { get; set; }
        public int ProjectID { get; set; }
        public int DaysWorked { get; set; }

        public ReturnData(int empl1ID, int empl2ID, int projectID, int daysWorked)
        {
            Empl1ID = empl1ID;
            Empl2ID = empl2ID;
            ProjectID = projectID;
            DaysWorked = daysWorked;
        }
    }
}
