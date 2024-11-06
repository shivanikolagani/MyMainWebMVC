using Microsoft.Extensions.DependencyModel;
using MyMainWebMVC.Models;
using System.ComponentModel;

namespace MyMainWebMVC.Models
{
   
public class Employee
    {
        [DisplayName("Employee Id")]
        public int EmpId { get; set; }

        [DisplayName("Employee Name")]
        public string? EmpName { get; set; }

        public int? DeptId { get; set; }

        public int? LibraryId { get; set; }

        public string Action { get; set; }
    }
}

