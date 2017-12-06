using System.Web.Mvc;

namespace TestDatabaze.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Uzivatele()
        {
            if (Session["Id"] != null)
            {
                var Users = DbCommands.SelectPersons("SELECT * FROM Users");

                return View(Users);
            }
            else
            {
                return RedirectToAction("Prihlaseni");
            }
        }

        public ActionResult Registrace()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrace(UserProfile objUser)
        {
            if (ModelState.IsValid)
            {
                if (DbCommands.UserExists(objUser.Username) == false)   
                {
                    DbCommands.InsertPerson(objUser);

                    return RedirectToAction("Index");
                }
            }
            return View(objUser);
        }

        public ActionResult Prihlaseni()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Prihlaseni(UserProfile objUser)
        {
                if (DbCommands.UserExists(objUser.Username))
                {
                    UserProfile p = DbCommands.SelectPerson(objUser.Username);

                var pw = Md5.CalculateMD5Hash(objUser.Password);

                    if (pw.ToLower() == p.Password.ToLower())
                    {
                        Session["Id"] = p.Id;
                        Session["UserName"] = p.Username;
                        Session["Admin"] = p.IsAdmin;

                    return RedirectToAction("Index");
                    }
                }

            return View(objUser);
        }

        public ActionResult Odhlaseni()
        {
            if (ModelState.IsValid)
            {
                if (Session["Id"] != null)
                {
                    Session.Clear();
                }

            }
            return RedirectToAction("Index");
        }

    }
}