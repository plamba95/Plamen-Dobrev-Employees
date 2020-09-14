using Plamen_Dobrev_Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plamen_Dobrev_Employees.Managers
{    
    public class EmployeeDataManager
    {
        private static readonly EmployeeDataManager instance = new EmployeeDataManager();

        private static Dictionary<int, List<EmployeeData>> employeesHashedData = new Dictionary<int, List<EmployeeData>>();
        // Creating hash map of the data, so it can be grouped by project id
        static EmployeeDataManager()
        {
        }
        private EmployeeDataManager()
        {
        }
        public static EmployeeDataManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void AddData(EmployeeData employeeData)
        {
            if (employeesHashedData.ContainsKey(employeeData.ProjectID))
            {
                employeesHashedData[employeeData.ProjectID].Add(employeeData);
            }
            else
            {
                employeesHashedData.Add(employeeData.ProjectID, new List<EmployeeData>() { employeeData });
            }
        }

        public void clearData()
        {
            employeesHashedData.Clear();
        }

        public List<ReturnData> ReturnData()
        {
            List<ReturnData> myList = new List<ReturnData>();
            foreach (var employeeDataList in employeesHashedData) 
                // Sort the list of employees by their project start date, 
                // so everyone had started after the previous one.
            {
                employeeDataList.Value.Sort((x, y) => DateTime.Compare(x.DateFrom, y.DateFrom));
                for (int i = 0; i < employeeDataList.Value.Count - 2; i++)
                {
                    EmployeeData employeeData = employeeDataList.Value[i];
                    for (int j = i+1; j < employeeDataList.Value.Count - 1; j++) {
                        EmployeeData nextEmployeeData = employeeDataList.Value[j];
                        if (DateTime.Compare(employeeData.DateTo, nextEmployeeData.DateFrom) >= 0)
                        // If "i" employee stopped working before "j" employee started working therefore they haven't worked together
                        {
                            int daysTogether;
                            if (DateTime.Compare(employeeData.DateTo, nextEmployeeData.DateTo) < 0) 
                            // if "i" employee had stopped working on the project before the "j" employee...
                            {
                                daysTogether = (employeeData.DateTo.Date - nextEmployeeData.DateFrom.Date).Days; 
                                // ...then the days they spent together are from the start date of the "j" employee
                                // until the end date of the "i" employee
                            }
                            else  
                            // if "j" employee had stopped working on the project before the "i" employee...
                            {
                                daysTogether = (nextEmployeeData.DateTo.Date - nextEmployeeData.DateFrom.Date).Days;
                                // ...then the days they spent together are from the start date until the end date of the "j" employee
                            }
                            myList.Add(new ReturnData(employeeData.EmpID, nextEmployeeData.EmpID, employeeDataList.Key, daysTogether));
                        }
                    }
                }                
            }
            myList.Sort((a, b) => b.DaysWorked.CompareTo(a.DaysWorked));
            return myList;
        }
    }
}
