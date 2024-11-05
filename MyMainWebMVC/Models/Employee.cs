using Microsoft.Extensions.DependencyModel;
using MyMainWebMVC.Models;

namespace MyMainWebMVC.Models
{
   
public class Employee
    {
        public int EmpId { get; set; }

        public string? EmpName { get; set; }

        public int? DeptId { get; set; }

        public int? LibraryId { get; set; }

        //public  Department? Dept { get; set; }

        //public  Library? Library { get; set; }
    }
}
