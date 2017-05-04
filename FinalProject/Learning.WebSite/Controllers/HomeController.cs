
using System.Linq;
using System.Web.Mvc;
using Learning.Business;
using Learning.WebSite.Models;

namespace Learning.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserManager userManager;
        private readonly IClassManager classManager;
        private readonly IUserClassManager userClassManager;


        public HomeController(IUserManager userManager, IClassManager classManager, IUserClassManager userClassManager)
        {
            this.userManager = userManager;
            this.classManager = classManager;
            this.userClassManager = userClassManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new Learning.WebSite.Models.UserModel { Id = user.Id, Name = user.Name };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserName, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Register(registerModel.UserName, registerModel.Password);

                Session["User"] = new Models.UserModel { Id = user.Id, Name = user.Name };

                System.Web.Security.FormsAuthentication.SetAuthCookie(registerModel.UserName, false);

                return Redirect(returnUrl ?? "~/");
            }

            return View(registerModel);
        }

        public ActionResult ClassList()
        {
            var classes = classManager.Classes
                                        .Select(t => new Learning.WebSite.Models.ClassModel
                                        { ClassId = t.ClassId, ClassName = t.ClassName, ClassDescription = t.ClassDescription, ClassPrice = t.ClassPrice })
                                        .ToArray();
            return View(classes);
        }

        [Authorize]
        [HttpGet]
        public ViewResult EnrollInClass()
        {
            var userAddClass = new Models.UserAddClass();
            userAddClass.Classes = new[]{
            new SelectListItem{ Text="C#", Value="1"},
            new SelectListItem{ Text="ASP.NET MVC", Value="2"},
            new SelectListItem{ Text="Android", Value="3"},
            new SelectListItem{ Text="Design Patterns", Value="4"}
            };
            return View(userAddClass);
        }

        [HttpPost]
        public ActionResult EnrollInClass(Models.UserAddClass userAddClass)
        {            
            var user = (Learning.WebSite.Models.UserModel)Session["User"];
            userAddClass.UserId = user.Id;            
            var item = userClassManager.Add(userAddClass.UserId, userAddClass.SelectedClassId);
            return View("AddClass", userAddClass);
        }

        [Authorize]
        public ActionResult EnrolledClasses()
        {
            var user = (Learning.WebSite.Models.UserModel)Session["User"];

            var classes = userClassManager.GetAll(user.Id)
                                        .Select(t => new Learning.WebSite.Models.UserClassModel
                                        { ClassId = t.ClassId, ClassName = t.ClassName, ClassDescription = t.ClassDescription, ClassPrice = t.ClassPrice });
            return View(classes);
        }

    }
}