using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plamen_Dobrev_Employees.Models
{
    public class EmployeeData
    {
        public int EmpID { get; set; }
        public int ProjectID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public EmployeeData(int empID, int projectID, DateTime dateFrom, DateTime dateTo)
        {
            EmpID = empID;
            ProjectID = projectID;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
}
