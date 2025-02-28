using MantenimientoSupplier.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MantenimientoSupplier.Controllers
{
    public class SupplierController : Controller
    {
        private SupplierRepository _repo = new SupplierRepository();

        public ActionResult Index(int page = 1)
        {
            var suppliers = _repo.GetAllSuppliers();

            int pageSize = 10;
            int totalItems = suppliers.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            if (page < 1) page = 1;
            if (page > totalPages && totalPages > 0) page = totalPages;

            var pagedSuppliers = suppliers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View(pagedSuppliers);
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
