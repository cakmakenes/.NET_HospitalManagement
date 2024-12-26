using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Branch
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Branch name is required!")]
        [StringLength(20, ErrorMessage ="Branch name must be maximum {1} characters!")]
        public string Name { get; set; }

        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
