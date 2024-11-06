
using System.ComponentModel;

namespace MyMainWebMVC.Models
{
    public class Department
    {
        [DisplayName("Department Id")]
        public int DeptId { get; set; }

        [DisplayName("Department Name")]
        public string? DeptName { get; set; }
        public string Action { get; set; }

        
    }
}
