using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Doctor name is required!")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required!")]
        [StringLength(20, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Surname { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public List<DoctorPatient> DoctorPatients { get; set; } = new List<DoctorPatient>();

    }
}
