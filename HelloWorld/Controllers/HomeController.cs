using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace HelloWorld.Controllers
{
    //[Logging]
    //[AuthorizeIPAddress]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //int x = 1;
            //int y = x / (x - 1);
            return View();
        }

        [HttpGet]
        public ActionResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RsvpForm(Models.GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
                return View("Thanks", guestResponse);
            else return View();
        }

        //public ActionResult Product()
        //{
        //var myProduct = new Product
        //{
        //    ProductId = 1,
        //    Name = "Kayak",
        //    Description = "A boat for one person",
        //    Category = "water-sports",
        //    Price = 200m,
        //};

        //    return View(myProduct);
        //}

        //public ActionResult Products()
        //{
        //    var products = new Product[]
        //    {
        //new Product{ ProductId = 1, Name = "First One", Price = 1.11m, ProductCount =0},
        //new Product{ ProductId = 2, Name="Second One", Price = 2.22m,ProductCount =1},
        //new Product{ ProductId = 3, Name="Third One", Price = 3.33m,ProductCount =0},
        //new Product{ ProductId = 4, Name="Fourth One", Price = 4.44m,ProductCount =3},
        //    };

        //    return View(products);
        //}

        private IProductRepository productRepository;

        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }


        public ActionResult Product()
        {
            return View(productRepository.Products.First());
        }

        //[OutputCache(Duration = 15, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult Products()
        {
            return View(productRepository.Products);
        }

        public PartialViewResult IncrementCount()
        {
            int count = 0;

            // Check if MyCount exists
            if (Session["MyCount"] != null)
            {
                count = (int)Session["MyCount"];
                count++;
            }

            // Create the MyCount session variable
            Session["MyCount"] = count;

            return new PartialViewResult();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                Session["UserName"] = loginModel.UserName;
                return RedirectToAction("Index");
            }
            else return View();
        }

        public ActionResult Logoff()
        {
            Session["UserName"] = null;
            return RedirectToAction("Index");
        }

        public PartialViewResult DisplayLoginName()
        {
            return new PartialViewResult();
        }

        public ActionResult SetCookie()
        {
            // Name the cookie as MyCookie for later retrieval
            var cookie = new HttpCookie("MyCookie");

            // This cookie will expire about one minute, depends on the browser
            cookie.Expires = DateTime.Now.AddSeconds(30);

            // This cookie will have a simple string value of myUserName
            // but it can be any kind of object.
            cookie.Value = "myUserName";

            // Add the cookie to the response to send it to the browser
            HttpContext.Response.Cookies.Add(cookie);

            return View(cookie);
        }

        public ActionResult GetCookie()
        {
            return View(HttpContext.Request.Cookies["MyCookie"]);
        }

        [Authorize]
        [IsAdministrator]

        public ActionResult Notes()
        {
            return View();
        }
    }
}