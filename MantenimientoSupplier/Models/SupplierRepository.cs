using MantenimientoSupplier.Models;
using System.Collections.Generic;
using System.Linq;

namespace MantenimientoSupplier.Models
{
    public class SupplierRepository
    {
        public List<sp_SupplierSelectAllResult> GetAllSuppliers()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (var db = new MySupplierContext(connectionString))
            {
                return db.sp_SupplierSelectAll().ToList();
            }
        }

        public Supplier GetById(long id)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (var db = new MySupplierContext(connectionString))
            {
                return db.Supplier.SingleOrDefault(s => s.ID == id);
            }
        }

        public void Insert(Supplier sup)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (var db = new MySupplierContext(connectionString))
            {
                db.Supplier.InsertOnSubmit(sup);
                db.SubmitChanges();
            }
        }

        public void Update(Supplier sup)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (var db = new MySupplierContext(connectionString))
            {
                var original = db.Supplier.SingleOrDefault(s => s.ID == sup.ID);
                if (original != null)
                {
                    original.SupplierName = sup.SupplierName;
                    original.Address1 = sup.Address1;
                    original.City = sup.City;
                    original.State = sup.State;
                    original.Country = sup.Country;
                    original.PhoneNumber = sup.PhoneNumber;
                    original.Code = sup.Code;

                    db.SubmitChanges();
                }
            }
        }

        public void Inactivate(long id)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (var db = new MySupplierContext(connectionString))
            {
                var sup = db.Supplier.SingleOrDefault(s => s.ID == id);
                if (sup != null)
                {
                    db.Supplier.DeleteOnSubmit(sup);
                    db.SubmitChanges();
                }
            }
        }
    }
}
