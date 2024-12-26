using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class PatientService : ServiceBase, IService<Patient,PatientModel>
    {
     
        public PatientService(Db db) : base(db)
        {
        }


        public IQueryable<PatientModel> Query()
        {
            return _db.Patients.Include(p => p.DoctorPatients).ThenInclude(dp => dp.Doctor).OrderByDescending(p => p.Id).Select(p => new PatientModel() { Record = p });
        }
        public ServiceBase Create(Patient record)
        {
            if (_db.Patients.Any(p=> p.Name.ToLower() == record.Name.ToLower().Trim() && p.IsFemale == record.IsFemale && p.BirthDate == record.BirthDate))
                return Error("Patient with the same name, birth date and gender exists!");
            record.Name = record.Name?.Trim();
            _db.Patients.Add(record);
            _db.SaveChanges();
            return Success("Patient created successfully");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Patients.Include(p => p.DoctorPatients).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return Error("Patient can't be found!");
            _db.DoctorPatients.RemoveRange(entity.DoctorPatients);
            _db.Patients.Remove(entity);
            _db.SaveChanges();
            return Success("Patient deleted successfully.");
        }

    
        public ServiceBase Update(Patient record)
        {
            if (_db.Patients.Any(p => p.Id != record.Id && p.Name.ToLower() == record.Name.ToLower().Trim() &&
                p.IsFemale == record.IsFemale && p.BirthDate == record.BirthDate))
                return Error("Patient with the same name, birth date and gender exists!");
            var entity = _db.Patients.Include(p => p.DoctorPatients).SingleOrDefault(p => p.Id == record.Id);
            if (entity is null)
                return Error("Patient not found!");
            _db.DoctorPatients.RemoveRange(entity.DoctorPatients);
            entity.Name = record.Name?.Trim();
            entity.Surname = record.Surname?.Trim();
            entity.IsFemale = record.IsFemale;  
            entity.BirthDate = record.BirthDate;
            entity.Height = record.Height;
            entity.Weight = record.Weight;
            //entity.SpeciesId = record.SpeciesId;
            entity.DoctorPatients = record.DoctorPatients;
            _db.Patients.Update(entity);
            _db.SaveChanges();
            return Success("Patient updated successfully.");


        }
    }
}
