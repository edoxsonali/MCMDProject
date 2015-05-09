using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using Moq;
using MCMD.IRepository.AdminInterfaces;
using MCMD.EntityRepository.AdminRepository;
//using MCMD.IRepository.DoctorInterfaces;
//using MCMD.EntityRepository.DoctorRepository;


namespace MCMD.DependencyInjection
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext
        requestContext, Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            // put bindings here          
            // ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            ninjectKernel.Bind<IUserRepository>().To<UserRepository>();      
            ninjectKernel.Bind<ISpecialityRepository>().To<SpecialityRepository>();      
            ninjectKernel.Bind<IMembershipRepository>().To<MembershipRepository>();
            ninjectKernel.Bind<IDoctorPersonalInfoRepository>().To<DoctorPersonalInfoRepository>();
            ninjectKernel.Bind<IDoctorClinicInformation>().To<DoctorClinicInformationRepository>();
        }
     
    }
}
