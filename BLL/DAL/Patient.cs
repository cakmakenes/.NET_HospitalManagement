using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Patient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Patient name is required!")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required!")]
        [StringLength(20, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Surname { get; set; }
        public bool IsFemale { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }

        public List<DoctorPatient> DoctorPatients { get; set; } = new List<DoctorPatient>();
    }
}
