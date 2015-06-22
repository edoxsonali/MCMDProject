using MCMD.EntityModel;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;
using MCMD.IRepository.AdminInterfaces;
using MCMD.ViewModel.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MCMD.Web.Controllers.Administration
{
    public class UpgradeServiceController : Controller
    {
        // GET: UpgradeService
        public ApplicationDbContext db = new ApplicationDbContext();
        private IUpgradeService upgradeSerciceRepo;

        public UpgradeServiceController(IUpgradeService _upgradeSerciceRepo)
        {
            this.upgradeSerciceRepo = _upgradeSerciceRepo;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserEditService(int Id)
        {
            Session["EditService"] = Id;
            var varid = Id;

            return Json(new { redirectUrl = Url.Action("Create", "UpgradeService", new { varid }), isRedirect = true, JsonRequestBehavior.AllowGet });
        }
        public ActionResult Create()
         {
             @TempData["Name"] = Session["Name"];
            ViewData["PageRole"] = 1;

            int Id = (Convert.ToInt32(Session["EditDoctor"]));

            int editInputs = (Session["EditService"] != null) ? (Convert.ToInt32(Session["EditService"])) : 0;


            UpgradeServiceViewModel UpgradeServiceVM = new UpgradeServiceViewModel();

            UpgradeServiceVM.DurationList = upgradeSerciceRepo.GetDuration().ToList();
            UpgradeServiceVM.MonthsList = upgradeSerciceRepo.GetMonths().ToList();
            UpgradeServiceVM.GetUpgrdService = upgradeSerciceRepo.GetUpgrdService().ToList();

            UpgradeServiceVM.MembershipList = upgradeSerciceRepo.GetMembers().Where(x => x.InactiveFlag == "N").ToList();
            UpgradeServiceVM.SelectedMember = UpgradeServiceVM.MembershipList.Select(x => x.MembershipId).ToArray();

            //if (editInputs != 0)
            //{
            //    List<UpgradeService> NewService = upgradeSerciceRepo.GetServices().Where(x => x.LoginId == 4).ToList();
              
            //    foreach (var item in NewService)
            //    {
            //        int logid = item.LoginId;
            //        int memID = item.MembershipId;

            //        var AllUserInfo = (from u in db.Memberships
            //                           join m in db.upgradeServices on u.MembershipId equals m.MembershipId 
            //                           where m.InactiveFlag == "N" && m.LoginId == logid
            //                           select new
            //                           {
            //                               CheckedStatus=m.CheckedStatus,
            //                               MembershipType=u.MembershipType,
            //                               MembershipId=u.MembershipId,
            //                           }).ToList();

            //        var AllUserInfoo = (from u in db.Memberships
            //                           join m in db.upgradeServices on u.MembershipId equals m.MembershipId
            //                            where m.InactiveFlag == "N" && u.MembershipId != memID
            //                           select new
            //                           {
            //                               CheckedStatus = u.CheckedStatus,
            //                               MembershipType = u.MembershipType,
            //                               MembershipId = u.MembershipId,
            //                           }).ToList();

            //        List<MCMDMembership> Membershipget = new List<MCMDMembership>();

            //        foreach (var itemm in AllUserInfo)
            //        {
            //            var s = new MCMDMembership();
            //            s.CheckedStatus = itemm.CheckedStatus;
            //            s.MembershipType = itemm.MembershipType;
            //            s.MembershipId = itemm.MembershipId;
            //            Membershipget.Add(s);

            //        }
            //        foreach (var itemm in AllUserInfoo)
            //        {
            //            var s = new MCMDMembership();
            //            s.CheckedStatus = itemm.CheckedStatus;
            //            s.MembershipType = itemm.MembershipType;
            //            s.MembershipId = itemm.MembershipId;
            //            Membershipget.Add(s);

            //        }
            //        UpgradeServiceVM.MembershipList = Membershipget;
            //        UpgradeServiceVM.UpgradeServiceId = item.UpgradeServiceId;
            //        UpgradeServiceVM.DurationId = item.Durations;
            //        UpgradeServiceVM.AutoRenavalId = item.AutoRenaval;
            //        Session["EditService"] = null;
            //    }


            //}
            //else
            //{

            //    UpgradeServiceVM.MembershipList = upgradeSerciceRepo.GetMembers().Where(x => x.InactiveFlag == "N").ToList();
            //    UpgradeServiceVM.SelectedMember = UpgradeServiceVM.MembershipList.Select(x => x.MembershipId).ToArray();

            //}
            return View(UpgradeServiceVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UpgradeServiceViewModel UpgradeServiceVM)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    int Id = (Convert.ToInt32(Session["EditDoctor"]));


                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECTED SERVICES :- ").AppendLine();
                    foreach (var item in UpgradeServiceVM.MembershipList)
                    {
                        if (item.CheckedStatus == true)
                        {
                            //Inserting Values Also in upgradeServiceL  Tabale 
                            var NewService = new UpgradeService();
                            sb.Append(item.MembershipType + ", ").AppendLine();
                            NewService.MembershipId = item.MembershipId;
                            NewService.LoginId = Id;
                            NewService.Durations = UpgradeServiceVM.DurationId;
                            NewService.AutoRenaval = UpgradeServiceVM.AutoRenavalId;
                            NewService.CheckedStatus = true;
                            NewService.CreatedById = 1;
                            NewService.InactiveFlag = "N";
                            NewService.CreatedOnDate = DateTime.Now;
                            NewService.ModifiedById = 1;
                            NewService.ModifiedOnDate = DateTime.Now;
                            upgradeSerciceRepo.InsertSrvice(NewService);
                            upgradeSerciceRepo.Save();

                            //Inserting Values Also in upgradeServiceLog History Tabale 
                            //var NewSerLog = new UpgradeServiceLog();

                            UpgradeServiceLog NewSerLog = new UpgradeServiceLog();


                            NewSerLog.MembershipId = item.MembershipId;
                            NewSerLog.LoginId = Id;
                            NewSerLog.Durations = UpgradeServiceVM.DurationId;
                            NewSerLog.AutoRenaval = UpgradeServiceVM.AutoRenavalId;
                            NewService.CheckedStatus = true;
                            NewSerLog.CreatedById = 1;
                            NewSerLog.InactiveFlag = "N";
                            NewSerLog.CreatedOnDate = DateTime.Now;
                            NewSerLog.ModifiedById = 1;
                            NewSerLog.ModifiedOnDate = DateTime.Now;
                            upgradeSerciceRepo.InsertServiceLog(NewSerLog);
                            upgradeSerciceRepo.Save();


                            //if (Session["EditService"] != null)
                            //{

                            //    NewService.UpgradeServiceId = Convert.ToInt32(Session["EditService"]);// assign the View Model Id to Entities Id
                            //    upgradeSerciceRepo.UpdateService(NewService);
                            //    //Session["EditService"] = null;
                            //}
                            //else
                            //{
                            //    upgradeSerciceRepo.InsertSrvice(NewService);



                            //};
                            //upgradeSerciceRepo.Save();

                            @TempData["SuccessMessage"] = "Successfully added..";



                        }
                    }

                    ViewBag.Message = "Successfully added..";
                }
            }
            catch (Exception)
            {
                //ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
                @TempData["Message"] = "Unable to save changes";
            }
            return RedirectToAction("Create");
        }
        public ActionResult ViewUpgradeServices(UpgradeServiceViewModel UpgradeServiceVM)
        {
            @TempData["Name"] = Session["Name"];

            UpgradeServiceVM.GetUpgrdService = upgradeSerciceRepo.GetUpgrdService().ToList();
            return View(UpgradeServiceVM);
        }

        [HttpPost]
        public ActionResult BatchDelete(int[] deleteInputs)
        {

            if (deleteInputs != null)
            {
                foreach (var item in deleteInputs)
                {

                    UpgradeService service = db.upgradeServices.Find(item);
                    if (User == null)
                    {
                        // return HttpNotFound();
                    }
                    else
                    {
                        service.InactiveFlag = "Y";
                        service.ModifiedOnDate = DateTime.Now;

                        upgradeSerciceRepo.UpdateService(service);
                        upgradeSerciceRepo.Save();
                        @TempData["SuccessMessage"] = "User having Service is deleted successfully";

                    }



                }

            }
            return Json("UpgradeService", JsonRequestBehavior.AllowGet);
        }
    }
}