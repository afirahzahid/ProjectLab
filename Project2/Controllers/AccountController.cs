using System;
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
        public ActionResult SIndex()
        {
            using (dbHostelManagementEntities db = new dbHostelManagementEntities())
            {
                return View(db.dbLogins.ToList());
            }

        }

        public ActionResult EIndex()
        {
            using (dbHostelManagementEntities db = new dbHostelManagementEntities())
            {
                return View(db.dbLogins.ToList());
            }

        }

        public ActionResult Allotment(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dbStudent student = db.dbStudents.Find(Convert.ToInt64(id));
            if (student == null)
            {
                return HttpNotFound();
            }
            int ss = db.dbAllotments.Where(x => x.A_StudentId == student.S_CNIC).Count();
            if (ss == 0)
            {
                dbAllotment entry = new dbAllotment();
                entry.A_StudentId = Convert.ToInt64(id);
                entry.A_Status = true;
                entry.A_DateIN = DateTime.Today;
                int j = db.dbRooms.Count();
                for (int k = 0; k < j; k++)
                {
                    var room = db.dbRooms.ToArray()[k];
                    int? capacity = room.RoomCapacity;
                    int alreadyalloted = db.dbAllotments.Where(x => x.A_RoomId == room.RoomId).Count();
                    if (alreadyalloted < capacity)
                    {
                        entry.A_RoomId = room.RoomId;
                        break;
                    }
                }
                if (entry.A_RoomId == 0)
                {
                    ModelState.AddModelError("", "No more rooms are available.");
                }
                db.dbAllotments.Add(entry);
                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "This Student has already been alloted a room");
            }
            return RedirectToAction("StudentsList");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dbLogin user = db.dbLogins.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginId, LoginEmail, LoginPass, LoginType, IsApproved")] dbLogin user)
        {
            try
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                if (user.LoginType == "s")
                {
                    return RedirectToAction("SIndex");
                }
                else
                {
                    return RedirectToAction("EIndex");
                }

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public ActionResult SDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dbLogin user = db.dbLogins.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            dbStudent student = db.dbStudents.SingleOrDefault(x => x.S_Email == user.LoginEmail);
            return View(student);
        }

        public ActionResult EDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dbLogin user = db.dbLogins.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            dbEmployee emp = db.dbEmployees.SingleOrDefault(x => x.EmpEmail == user.LoginEmail);
            return View(emp);
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
                    dbLogin l = new dbLogin
                    {
                        LoginEmail = s.S_Email,
                        LoginPass = s.S_Password,
                        LoginType = "s",
                    };
                    db.dbLogins.Add(l);
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
        public ActionResult Login(dbLogin l)
        {
            using (dbHostelManagementEntities db = new dbHostelManagementEntities())
            {
                var usr = db.dbLogins.SingleOrDefault(u => u.LoginEmail == l.LoginEmail);
                if (usr != null)
                {
                    if (usr.LoginPass == l.LoginPass)
                    {
                        if (usr.LoginType == "s")
                        {
                            if (usr.IsApproved == true)
                            {
                                Session["S_Email"] = usr.LoginEmail.ToString();
                                return RedirectToAction("LoggedIn");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Your registration request has not been approved by admin yet");
                            }

                        }
                        else
                        {
                            if (usr.IsApproved == true)
                            {
                                Session["EmpEmail"] = usr.LoginEmail.ToString();
                                return RedirectToAction("ELoggedIn");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Your registration request has not been approved by admin yet");
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is wrong.");
                }
            }
            return View(l);
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
                    dbLogin l = new dbLogin
                    {
                        LoginEmail = e.EmpEmail,
                        LoginPass = e.EmpPassword,
                        LoginType = "e",
                        IsApproved = false
                    };
                    db.dbLogins.Add(l);
                    db.dbEmployees.Add(e);
                    db.SaveChanges();
                    ModelState.Clear();
                    e = null;
                    ViewBag.Message1 = "Registered Successfully";

                }

            }
            return View(e);
        }

        
        
        public ActionResult ELoggedIn()
        {
            if (Session["EmpEmail"] != null)
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

        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Admin(dbLogin l)
        {
            if (l.LoginEmail == "admin123@gmail.com")
            {
                if (l.LoginPass == "admin123")
                {
                    return RedirectToAction("ALoggedin");
                }
            }
            else
            {
                ViewBag.message3 = "Login Failed";
            }
            return RedirectToAction("Admin");
        }

        public ActionResult ALoggedin()
        {
            return View();
        }
        [HttpPost]
        public JsonResult doesUserNameExist(string S_RegNo)
        {

            var user = Membership.GetUser(S_RegNo);

            return Json(user == null);
        }


    }
}
        
