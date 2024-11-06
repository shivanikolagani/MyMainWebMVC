using System.ComponentModel;

namespace MyMainWebMVC.Models
{
    public class EmpDept
    {
        [DisplayName("Department Id")]
        public int DeptId { get; set; }

        [DisplayName("Employee Id")]
        public int EmpId { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Department Name")]
        public string DeptName { get; set; }
       

    }
}

