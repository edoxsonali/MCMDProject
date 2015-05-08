using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Doctor;

namespace MCMD.IRepository.DoctorInterfaces
{
    public interface IRegisterRepository : IDisposable
    {
         IEnumerable<Register> GetRegisters();
         IEnumerable<Speciality> GetSpecialitys();
         IEnumerable<Title> GetTitles();
         Register GetRegisterByID(int UserId);
         void InsertRegDoc(Register Register);
         void UpdateRegDoc(Register Register);
         void DeleteRegDoc(int RegisterID);
       
         void Save();
    }
}
