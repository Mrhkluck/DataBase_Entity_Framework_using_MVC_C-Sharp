using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DB_FirstEntity.Models;

namespace DB_FirstEntity.Controllers
{
   [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        
        CollegeDbEntities2 db = new CollegeDbEntities2(); //Context object for db operator

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)//Login Query
            {
                bool isValidUser = db.Logins.Any
                                 (u => u.Username == login.Username && u.Password == login.Password);
                if (isValidUser)
                {
                    //ViewBag.UserName = login.UserName;
                    // TempData["UserName"] = login.Username;

                    //Cookies Creation
                    /* HttpCookie ht = new HttpCookie("PGDAC");
                     ht.Values.Add("UserName", login.Username);
                     ht.Values.Add("loginTime", DateTime.Now.ToString());

                     //to create persistent cookie
                     ht.Expires = DateTime.Now.AddMinutes(40);

                     //writing cookies data with current responcse
                     Response.Cookies.Add(ht);*/


                    //using Session
                    //Session["UserName"]=login.Username;
                    //Session["LoginTime"] = DateTime.Now;

                    FormsAuthentication.SetAuthCookie(login.Username, false);

                    // return RedirectToAction("UserProfile");
                    return RedirectToAction("Index","Student");

                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username/password");
                    return View();
                }
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Login login)
        {
            if (ModelState.IsValid)
            {
                db.Logins.Add(login); //to save user data in context
                db.SaveChanges(); //To save user data in database
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult UserProfile()
        //Cheaking and Reading Cookies Data
        {
            /* if (Request.Cookies["PGDAC"] != null)
             {
                 ViewBag.UserName = Request.Cookies["PGDAC"]["UserName"];
                 ViewBag.LoginTime = Request.Cookies["PGDAC"]["LoginTime"];

                 //ViewBag.UserName = TempData["UserName"];
                 return View();
             }
             else
             {
                 return View("Login");
             }*/
            //using session
            if (Session.IsNewSession)
            {
                return View("SessionExpired");
            }

            //using session
            if (Session["UserName"] != null)
            {
                ViewBag.UserName = Session["UserName"];
                ViewBag.LoginTime = Session["LoginTime"];
                ViewBag.TotalVisitors = HttpContext.Application["totalvisitor"];//to see no. of user accessing

                return View();
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult SignOut()
        {
            /* Request.Cookies["PGDAC"].Expires = DateTime.Now.AddMilliseconds(-1);
             Request.Cookies.Remove("PGDAC");*/
            //return View("Login");
            //HttpCookie cookies = Request.Cookies["PGDAC"];
            //cookies.Expires = DataTime.Now.AddMilliseconds(-1);
            //Response.Cookies.Add(cookie);


            // Session.Abandon();

            FormsAuthentication.SignOut();
            return View("Login");
        }
        public ActionResult SessionExpired()
        {
            return View();
        }
    }
}