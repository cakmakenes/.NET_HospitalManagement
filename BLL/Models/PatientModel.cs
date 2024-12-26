using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class PatientModel
    {
        public Patient Record { get; set; }
        public string Name => Record.Name;
        public string Surname => Record.Surname;

        [DisplayName("Is Female?")]
        public string IsFemale => Record.IsFemale ? "Female" : "Male";
        
        [DisplayName("Birth Date")]
        public string BirthDate => Record.BirthDate is null ? string.Empty : Record.BirthDate.Value.ToString("MM/dd/yyyy");
        public string Height => Record.Height.HasValue ? Record.Height.Value.ToString("N2") : string.Empty;
        public string Weight => (Record.Weight ?? 0).ToString("N1");

        public string Doctors => string.Join("<br>",Record.DoctorPatients.Select(dp => dp.Doctor?.Name + " " + dp.Doctor?.Surname));

        [DisplayName("Doctor(s)")]
        public List<int> DoctorIds
        {
            get => Record.DoctorPatients?.Select(dp=>dp.DoctorId).ToList();
            set => Record.DoctorPatients = value.Select(v => new DoctorPatient() { DoctorId = v }).ToList();
        }
         
    } 
}
