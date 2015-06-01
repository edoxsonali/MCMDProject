using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.IRepository.AdminInterfaces;
using MCMD.EntityModel;
using MCMD.EntityModel.Doctor;
using System.Data.Entity;
using MCMD.ViewModel.Administration;

namespace MCMD.EntityRepository.AdminRepository
{
    public class SpecialityRepository : ISpecialityRepository
    {

        private ApplicationDbContext DBcontext;
        public SpecialityRepository(ApplicationDbContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }
        public IEnumerable<Speciality> GetSpecialitys()
        {
         
            return DBcontext.Specialitys.ToList();
        }
        public Speciality GetSpecialityByID(int ID)
        {
            return DBcontext.Specialitys.Find(ID);
        }
        public void InsertSpeciality(SpecialityViewModel specialityVM,Speciality speciality)
        {

            speciality.SpecialityName = specialityVM.SpecialityName;
            speciality.InactiveFlag = "N";
            speciality.ModifiedDate = DateTime.Now;

            DBcontext.Specialitys.Add(speciality);
        }
        public void UpdateSpeciality(Speciality speciality)
        {
            DBcontext.Entry(speciality).State = EntityState.Modified;
        }
        public void DeleteSpeciality(int SpecialityID)
        {
            Speciality speciality = DBcontext.Specialitys.Find(SpecialityID);
            DBcontext.Specialitys.Remove(speciality);
        }
        //public Speciality CheckSpeciality(string specialityName)
        //{
        //    var cSpeciality = DBcontext.Specialitys.FirstOrDefault(x => x.SpecialityName == specialityName);
        //    return cSpeciality;
        //}
        public void Save()
        {
            DBcontext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBcontext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
