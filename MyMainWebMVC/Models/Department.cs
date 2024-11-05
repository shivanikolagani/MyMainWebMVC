
namespace MyMainWebMVC.Models
{
    public class Department
    {
        public int DeptId { get; set; }

        public string? DeptName { get; set; }

        public static implicit operator Department(List<Department> v)
        {
            throw new NotImplementedException();
        }
    }
}
