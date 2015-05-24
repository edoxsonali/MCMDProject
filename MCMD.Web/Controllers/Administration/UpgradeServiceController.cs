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
            ViewData["PageRole"] = 1;

            int editInputs = (Session["EditService"] != null) ? (Convert.ToInt32(Session["EditService"])) : 0;


            UpgradeServiceViewModel UpgradeServiceVM = new UpgradeServiceViewModel();
            // UpgradeServiceVM.upgradeServiceList = upgradeSerciceRepo.GetServices().ToList();
            UpgradeServiceVM.DurationList = upgradeSerciceRepo.GetDuration().ToList();
            UpgradeServiceVM.MonthsList = upgradeSerciceRepo.GetMonths().ToList();
            UpgradeServiceVM.GetUpgrdService = upgradeSerciceRepo.GetUpgrdService().ToList();

            UpgradeServiceVM.membershipListTwo = new List<MembershipTwo>() {
                new MembershipTwo {MembershipId=1,MembershipType="Directory Listing"},//{ID=1,Name="Group 1" },
                new MembershipTwo {MembershipId=2,MembershipType="Online Appointment Scheduling" },
                new MembershipTwo {MembershipId=3,MembershipType="Medical Answering Service " },
               
            };
            UpgradeServiceVM.SelectedMember = UpgradeServiceVM.membershipListTwo.Select(x => x.MembershipId).ToArray();

            if (editInputs != 0)
            {
                List<UpgradeService> NewService = upgradeSerciceRepo.GetServices().Where(x => x.UpgradeServiceId == editInputs).ToList();
                foreach (var item in NewService)
                {
                    UpgradeServiceVM.UpgradeServiceId = item.UpgradeServiceId;
                    UpgradeServiceVM.MembershipId = item.MembershipId;
                    //UpgradeServiceVM. = item.LoginId;
                    UpgradeServiceVM.DurationId = item.Durations;
                    UpgradeServiceVM.AutoRenavalId = item.AutoRenaval;
                }

            }
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



                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECTED COUNTRY :- ").AppendLine();
                    foreach (var item in UpgradeServiceVM.membershipListTwo)
                    {
                        if (item.CheckedStatus == true)
                        {
                            //Inserting Values Also in upgradeServiceL  Tabale 
                            var NewService = new UpgradeService();
                            sb.Append(item.MembershipType + ", ").AppendLine();
                            NewService.MembershipId = item.MembershipId;
                            NewService.LoginId = 1;
                            NewService.Durations = UpgradeServiceVM.DurationId;
                            NewService.AutoRenaval = UpgradeServiceVM.AutoRenavalId;
                            NewService.CreatedById = 1;
                            NewService.InactiveFlag = "N";
                            NewService.CreatedOnDate = DateTime.Now;
                            NewService.ModifiedById = 1;
                            NewService.ModifiedOnDate = DateTime.Now;
                            upgradeSerciceRepo.InsertSrvice(NewService);
                            upgradeSerciceRepo.Save();

                            //Inserting Values Also in upgradeServiceLog History Tabale 
                            //var NewSerLog = new UpgradeServiceLog();

                            //NewSerLog.MembershipId = item.MembershipId;
                            //NewSerLog.LoginId = 1;
                            //NewSerLog.Durations = UpgradeServiceVM.DurationId;
                            //NewSerLog.AutoRenaval = UpgradeServiceVM.AutoRenavalId;
                            //NewSerLog.CreatedById = 1;
                            //NewSerLog.InactiveFlag = "N";
                            //NewSerLog.CreatedOnDate = DateTime.Now;
                            //NewSerLog.ModifiedById = 1;
                            //NewSerLog.ModifiedOnDate = DateTime.Now;
                            //upgradeSerciceRepo.InsertServiceLog(NewSerLog);
                            //upgradeSerciceRepo.Save();


                            //if (Session["EditService"] != null)
                            //{

                            //    NewService.UpgradeServiceId = Convert.ToInt32(Session["EditService"]);// assign the View Model Id to Entities Id
                            //    upgradeSerciceRepo.UpdateService(NewService);
                            //    Session["EditService"] = null;
                            //}
                            //else
                            //{
                            //    upgradeSerciceRepo.InsertSrvice(NewService);



                            //};
                            //upgradeSerciceRepo.Save();
                            ViewBag.Message = "Succsessfully added..";




                        }
                    }



                    ViewBag.MembershipTwo = sb.ToString();

                    ViewBag.Message = "Succsessfully added..";
                }
            }
            catch (Exception)
            {
                //ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return RedirectToAction("Create");
        }
        public ActionResult ViewUpgradeServices(UpgradeServiceViewModel UpgradeServiceVM)
        {

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
                        @TempData["AddNewItemMessage"] = "User having Service is deleted successfully";

                    }



                }

            }
            return Json("UpgradeService", JsonRequestBehavior.AllowGet);
        }
    }
}