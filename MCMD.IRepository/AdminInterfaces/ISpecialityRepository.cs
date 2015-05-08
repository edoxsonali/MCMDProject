using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Doctor;


namespace MCMD.IRepository.AdminInterfaces
{
    public interface ISpecialityRepository : IDisposable
    {
        IEnumerable<Speciality> GetSpecialitys();
        Speciality GetSpecialityByID(int ID);
        void InsertSpeciality(Speciality Speciality);
        void UpdateSpeciality(Speciality Speciality);
        void DeleteSpeciality(int SpecialityID);
    //    void CheckSpeciality(string SpecialityName);
        void Save();
   
    }
}
