﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project2.Models;

namespace Project2.Controllers
{
    public class AccountController : Controller
    {
        private object db;

        // GET: Account
        public ActionResult Index()
        {
            using (dbHostelManagementEntities db = new dbHostelManagementEntities())
            {
                return View(db.dbStudents.ToList());
            }
            
        }
        [HttpGet]
        public ActionResult Register()
        {
            dbStudent s = new dbStudent();
            return View(s);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(dbStudent s)
        {
            if (ModelState.IsValid)
            {
                using (dbHostelManagementEntities db = new dbHostelManagementEntities())
                {
                    db.dbStudents.Add(s);
                    db.SaveChanges();
                    ModelState.Clear();
                    s = null;
                    ViewBag.Message = "Registration Successful";

                }
                
            }
            return View(s);
        }
        // Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(dbStudent s)
        {
            using (dbHostelManagementEntities db = new dbHostelManagementEntities())
            {
                var usr = db.dbStudents.SingleOrDefault(u => u.S_Email == s.S_Email );
                if (usr != null)
                {
                    if(usr.S_Password == s.S_Password)
                    {
                        Session["S_CNIc"] = usr.S_CNIC.ToString();
                        Session["S_Email"] = usr.S_Email.ToString();
                        return RedirectToAction("LoggedIn");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email or Password is wrong.");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is wrong.");
                }
          
            }
            return View(s);
        }
        public ActionResult LoggedIn()
        {
            if(Session["S_CNIC"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult ERegister()
        {
            dbEmployee e = new dbEmployee();
            return View(e);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ERegister(dbEmployee e)
        {
            if (ModelState.IsValid)
            {
                using (dbHostelManagementEntities db = new dbHostelManagementEntities())
                {
                    db.dbEmployees.Add(e);
                    db.SaveChanges();
                    ModelState.Clear();
                    e = null;
                    ViewBag.Message1 = "Registered Successfully";

                }
            }
            return View(e);
        }

        public ActionResult ELogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ELogin(dbEmployee e)
        {
            using (dbHostelManagementEntities db = new dbHostelManagementEntities())
            {
                var usr = db.dbEmployees.SingleOrDefault(u => u.EmpEmail == e.EmpEmail) ;
                if (usr != null)
                {
                    if (usr.EmpPassword == e.EmpPassword)
                    {
                        Session["EmpCNIc"] = usr.EmpCNIC.ToString();
                        Session["EmpEmail"] = usr.EmpEmail.ToString();
                        return RedirectToAction("ELoggedIn");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email or Password is wrong.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is wrong.");
                }
            }
            return View(e);
        }
        public ActionResult ELoggedIn()
        {
            if (Session["EmpCNIC"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ELogin");
            }
        }
        public ActionResult EmployeList()
        {
            using (dbHostelManagementEntities db = new dbHostelManagementEntities())
            {
                return View(db.dbEmployees.ToList());
            }
        }

        [HttpPost]
        public JsonResult doesUserNameExist(string S_RegNo)
        {

            var user = Membership.GetUser(S_RegNo);

            return Json(user == null);
        }


    }
}