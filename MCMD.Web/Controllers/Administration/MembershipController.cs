
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCMD.ViewModel.Administration;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using MCMD.IRepository.AdminInterfaces;
using MCMD.EntityRepository.AdminRepository;
using MCMD.EntityModel.Administration;
using System.Web.Security;
using MCMD.EntityModel;

namespace MCMD.Web.Controllers.Administration
{
    public class MembershipController : Controller
    {
        // GET: Membership
        public ApplicationDbContext db = new ApplicationDbContext();
        private IMembershipRepository membershipRepository;
        public MembershipController(IMembershipRepository _membershipRepository)
        {
            this.membershipRepository = _membershipRepository;
        }

        #region Edit Membership
        public ActionResult UserEditMembership(int Id)
        {
            Session["EditMembership"] = Id;
            var varid = Id;
            return Json(new { varid }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create/View Membership
        //GET: CreateUser/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            @TempData["Name"] = Session["Name"];
            ViewData["PageRole"] = 1;

            int editInputs = (Session["EditMembership"] != null) ? (Convert.ToInt32(Session["EditMembership"])) : 0;         
            MembershipViewModel _memberShipVM = new MembershipViewModel();     
            _memberShipVM.GetMembers = membershipRepository.GetMembers().ToList();

            if (editInputs != 0)
            {
                List<MCMDMembership> _NewMembership = membershipRepository.GetMembers().Where(x => x.MembershipId == editInputs).ToList();

                foreach (var item in _NewMembership)
                {
                    _memberShipVM.MembershipType = item.MembershipType;
                    _memberShipVM.Fees = item.Fees;
                }

            }

            return View(_memberShipVM);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MembershipViewModel _memberShipVM)//Pass View Model Data to the Database Entity so send View Model object (i.e _memberShipVM )
        {
            try
            {
              
                if (ModelState.IsValid)
                {

                    var newMember = new MCMD.EntityModel.Administration.MCMDMembership();// Create entity Model Class Object   like(newMember) db.Users.Create()                     

                    newMember.MembershipType = _memberShipVM.MembershipType;  //Pass MembershipType from MembershipViewModel to MCMDMembership Entity Model using theire objects
                    newMember.Fees = _memberShipVM.Fees;                       //Pass Fees from MembershipViewModel to MCMDMembership Entity Model using theire objects(_memberShipVM and newMember )
                    newMember.InactiveFlag = "N";//_memberShipVM.member.InactiveFlag;
                    newMember.ModifiedDate = DateTime.Now;//_memberShipVM.member.ModifiedDate;
                    newMember.CheckedStatus = false;

                    if (Session["EditMembership"] != null)
                    {

                        newMember.MembershipId = Convert.ToInt32(Session["EditMembership"]);// assign the View Model Id to Entities Id
                        membershipRepository.UpdateMember(newMember);
                        Session["EditMembership"] = null;
                    }
                    else
                    {
                        membershipRepository.InsertMember(newMember);

                    };
                    membershipRepository.Save();
                    @TempData["SuccessMessage"] = "Succsessfully added..";


                }

            }
            catch (Exception)
            {
                
                @TempData["Message"] = "Unable to save ";
            }
            return RedirectToAction("Create");


        }
        #endregion



    }


}