using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    //public interface IBranchService
    //{
    //    public IQueryable<BranchModel> Query();
    //    public ServiceBase Create(Branch record);
    //    public ServiceBase Update(Branch record);
    //    public ServiceBase Delete(int id);
    //}
    public class BranchService : ServiceBase, IService<Branch, BranchModel>
    {
        public BranchService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Branch record)
        {
            if (_db.Branches.Any(b => b.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Branch already exists");
            record.Name = record.Name?.Trim();
            _db.Branches.Add(record);
            _db.SaveChanges();
            return Success("Branch created successfully.");
        }

        public ServiceBase Delete(int Id)
        {
            var entity = _db.Branches.Include(b => b.Doctors).SingleOrDefault(b => b.Id == Id);
            if (entity is null)
                return Error("Branch can't be found!");
            _db.Branches.Remove(entity);
            _db.SaveChanges();
            return Success("Branch deleted successfully.");
        }

        public IQueryable<BranchModel> Query()
        {
         return _db.Branches.OrderBy(b => b.Name).Select(b => new BranchModel() { Record = b });

        }

        public ServiceBase Update(Branch record)
        {
            if (_db.Branches.Any(b => b.Id != record.Id && b.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Branch with the same name exists!");

            var entity = _db.Branches.SingleOrDefault(b => b.Id == record.Id);
            if (entity is null)
                return Error("Branch can't be found!");
            entity.Name = record.Name?.Trim();
            _db.Branches.Update(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Branch updated successfully.");
        }
    }
}
