using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace RestApiCrud.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Employee name required")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage ="Employee pin required")]
        
        public string Pin { get; set; }
        [Required(ErrorMessage = "Employee date of birth required")]
        public DateTime DateOfBirth { get; set; }
        
        public bool IsActive { get; set; }=true;
        [Required]
        public int DepartmentId { get; set; }

        // Navigation property to DepartmentModel
        [ForeignKey("DepartmentId")]
        [JsonIgnore]
        public DepartmentModel? Department { get; set; }=null;

    }
}
