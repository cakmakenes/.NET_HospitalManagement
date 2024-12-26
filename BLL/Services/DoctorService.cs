using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class DoctorService : ServiceBase, IService<Doctor, DoctorModel>
    {
        public DoctorService(Db db) : base(db)
        {
        }

        public IQueryable<DoctorModel> Query()
        {
            return _db.Doctors.Include(d => d.Branch).Include(d=> d.DoctorPatients).ThenInclude(dp=>dp.Patient).Select(d=> new DoctorModel() { Record = d});
        }

        public ServiceBase Create(Doctor record)
        {
            if (_db.Doctors.Any(d => d.Name.ToLower() == record.Name.ToLower().Trim() && d.Surname.ToLower() == record.Surname.ToLower()))
               return Error("Doctor with the same name and surname exists!");
            record.Name = record.Name?.Trim();
            _db.Doctors.Add(record);
            _db.SaveChanges();
            return Success("Doctor created successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Doctors.Include(p => p.DoctorPatients).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return Error("Doctor can't be found!");
            _db.DoctorPatients.RemoveRange(entity.DoctorPatients);
            _db.Doctors.Remove(entity);
            _db.SaveChanges();
            return Success("Doctor deleted successfully.");
        }

       

        public ServiceBase Update(Doctor record)
        {
            if (_db.Doctors.Any(d => d.Name.ToLower() == record.Name.ToLower().Trim() && d.Surname.ToLower() == record.Surname.ToLower() && d.BranchId == record.BranchId))
                return Error("Doctor with the same properties exists!");
            var entity = _db.Doctors.Include(d => d.DoctorPatients).SingleOrDefault(p => p.Id == record.Id);
            if (entity is null)
                return Error("Doctor not found!");
            _db.DoctorPatients.RemoveRange(entity.DoctorPatients);
            entity.Name = record.Name.Trim();
            entity.Surname = record.Surname.Trim();
          
            entity.BranchId = record.BranchId;
            entity.DoctorPatients = record.DoctorPatients;
            _db.Doctors.Update(entity);
            _db.SaveChanges();
            return Success("Doctor updated successfully.");
        }
    }
}
