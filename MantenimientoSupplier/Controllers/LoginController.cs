using System.Web.Mvc;

namespace MantenimientoSupplier.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            // acepta "admin" como usuario y contraseña.
            if (username == "admin" && password == "admin")
            {
                Session["User"] = username;
                return RedirectToAction("Index", "Supplier");
            }
            else
            {
                ViewBag.ErrorMessage = "Usuario o contraseña incorrectos";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }

    }
}
