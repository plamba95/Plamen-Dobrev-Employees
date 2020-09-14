using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plamen_Dobrev_Employees.Models;
using Plamen_Dobrev_Employees.Managers;

namespace Plamen_Dobrev_Employees.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index(IFormFile? file)
        {
            List<ReturnData> returnedData = new List<ReturnData>();
            if (file != null)
            {
                EmployeeDataManager employeeDataStorage = EmployeeDataManager.Instance;
                employeeDataStorage.clearData();
                using var reader = new StreamReader(file.OpenReadStream());
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine().Split(',');
                    if (line.Count() != 4)
                    {
                        ViewBag.Error = "Seems like the data is not in the correct format. Make sure sure the format is EmpID, ProjectID, DateFrom, DateTo";
                        break;
                    }
                    var formats = new[] { "yyyy-M-d", "yyyy/M/d", "d/M/yyyy", "d.M.yyyy"}.Union(CultureInfo.InvariantCulture.DateTimeFormat.GetAllDateTimePatterns()).ToArray();
                    DateTime startDate;
                    if(!DateTime.TryParseExact(line[2].Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
                    {
                        ViewBag.Error = "Unsupported DateFrom format. Allowed formats: yyyy-M-d, yyyy/M/d, d/M/yyyy, d.M.yyyy";
                        break;
                    }
                    DateTime endDate;
                    if(line[3].Trim().ToLower() == "null")
                    {
                        endDate = DateTime.Now;
                    }
                    else if(!DateTime.TryParseExact(line[3].Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                    {
                        ViewBag.Error = "Unsupported DateTo format. Allowed formats: yyyy-M-d, yyyy/M/d, d/M/yyyy, d.M.yyyy";
                        break;
                    }
                    employeeDataStorage.AddData(new EmployeeData(Int32.Parse(line[0].Trim()), Int32.Parse(line[1].Trim()), startDate, endDate));
                }
                returnedData = employeeDataStorage.ReturnData();
            }
            return View(returnedData);
        }

    }
}
