using System.ComponentModel.DataAnnotations;

namespace CompanyDashboard.Models
{
    public class DepartmentModel
    {
            public int Id { get; set; }
            [Required]
            public string DepartmentName { get; set; }

            // Navigation property to EmployeeModel
            public ICollection<EmployeeModel> Employees { get; set; }
        
    }
}
