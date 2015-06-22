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

namespace MCMD.Web.Controllers.Doctor
{
    public class DocUpgradeServiceController : Controller
    {
        // GET: DocUpgradeService

        public ApplicationDbContext db = new ApplicationDbContext();
        private IUpgradeService upgradeSerciceRepo;

        public DocUpgradeServiceController(IUpgradeService _upgradeSerciceRepo)
        {
            this.upgradeSerciceRepo = _upgradeSerciceRepo;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            @TempData["Name"] = Session["Name"];
            ViewData["PageRole"] = 1;

            int editDocInputs = (Session["Doctor"] != null) ? (Convert.ToInt32(Session["Doctor"])) : 0;

            UpgradeServiceViewModel UpgradeServiceVM = new UpgradeServiceViewModel();

            UpgradeServiceVM.DurationList = upgradeSerciceRepo.GetDuration().ToList();
            UpgradeServiceVM.MonthsList = upgradeSerciceRepo.GetMonths().ToList();
            UpgradeServiceVM.GetUpgrdService = upgradeSerciceRepo.GetUpgrdService().ToList();

            UpgradeServiceVM.membershipListTwo = new List<MembershipTwo>() {
                new MembershipTwo {MembershipId=1,MembershipType="Directory Listing"},//{ID=1,Name="Group 1" },
                new MembershipTwo {MembershipId=2,MembershipType="Online Appointment Scheduling" },
                new MembershipTwo {MembershipId=3,MembershipType="Medical Answering Service " },
               
            };
            UpgradeServiceVM.SelectedMember = UpgradeServiceVM.membershipListTwo.Select(x => x.MembershipId).ToArray();

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
                    //int Id = (Convert.ToInt32(Session["EditDoctor"]));
                    UpgradeServiceVM.LoginId = Convert.ToInt32(Session["Doctor"]);//Change

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECTED SERVICES :- ").AppendLine();
                    foreach (var item in UpgradeServiceVM.membershipListTwo)
                    {
                        if (item.CheckedStatus == true)
                        {
                            //Inserting Values Also in upgradeServiceL  Tabale 
                            var NewService = new UpgradeService();
                            sb.Append(item.MembershipType + ", ").AppendLine();
                            NewService.MembershipId = item.MembershipId;
                            NewService.LoginId = UpgradeServiceVM.LoginId;//Id;
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

                            UpgradeServiceLog NewSerLog = new UpgradeServiceLog();


                            NewSerLog.MembershipId = item.MembershipId;
                            NewSerLog.LoginId = UpgradeServiceVM.LoginId; //Id;
                            NewSerLog.Durations = UpgradeServiceVM.DurationId;
                            NewSerLog.AutoRenaval = UpgradeServiceVM.AutoRenavalId;
                            NewSerLog.CreatedById = 1;
                            NewSerLog.InactiveFlag = "N";
                            NewSerLog.CreatedOnDate = DateTime.Now;
                            NewSerLog.ModifiedById = 1;
                            NewSerLog.ModifiedOnDate = DateTime.Now;
                            upgradeSerciceRepo.InsertServiceLog(NewSerLog);
                            upgradeSerciceRepo.Save();
                            ViewBag.Message = "Record Successfully Added ..";
                        }
                    }

                    ViewBag.MembershipTwo = sb.ToString();

                    ViewBag.Message = "Record Successfully Added ..";
                }
            }
            catch (Exception)
            {
                //ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return RedirectToAction("Create");
        }


    }
}