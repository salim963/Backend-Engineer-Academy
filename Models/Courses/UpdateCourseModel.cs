using System.ComponentModel.DataAnnotations;

namespace Backendv2.Models.Courses
{
    public class UpdateCourseModel
    {
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
