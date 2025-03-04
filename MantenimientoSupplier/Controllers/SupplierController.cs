using MantenimientoSupplier.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MantenimientoSupplier.Controllers
{
    public class SupplierController : Controller
    {
        private SupplierRepository _repo = new SupplierRepository();

        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var suppliers = _repo.GetAllSuppliers();

            return View(suppliers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Supplier model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repo.Insert(model);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public ActionResult Edit(long id)
        {
            var sup = _repo.GetById(id);
            if (sup == null) return HttpNotFound();
            return View(sup);
        }

        [HttpPost]
        public ActionResult Edit(Supplier model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repo.Update(model);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public ActionResult Delete(long id)
        {
            var sup = _repo.GetById(id);
            if (sup == null) return HttpNotFound();
            return View(sup);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                _repo.Inactivate(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

    }
}
