using System.ComponentModel.DataAnnotations;

namespace RestApiCrud.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please provide department name")]
        public string DepartmentName { get; set; }

        
    }
}
