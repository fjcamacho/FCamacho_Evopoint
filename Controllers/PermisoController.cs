using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCamacho_EvoPoint.Models;
namespace FCamacho_EvoPoint.Controllers
{
    public class PermisoController : Controller
    {

        // GET: Permiso
        public ActionResult Index()
        {
            return View();
        }

        //Fetch Data From Database
        [HttpGet]
        public ActionResult GetData()
        {
            using (FCPruebaDBEntities db = new FCPruebaDBEntities())
            {
                try
                {
                    List<vwPermiso> permisolist = db.vwPermisoes.ToList<vwPermiso>();
                    return Json(new { data = permisolist }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.StackTrace, JsonRequestBehavior.AllowGet });
                }
            }
        }

        //Method for Insert and Update
        [HttpGet]
        public ActionResult CreateOrUpdate(int id = 0)
        {
            if (id == 0)
                return View(new Permiso());
            else
            {
                using (FCPruebaDBEntities db = new FCPruebaDBEntities())
                {
                    return View(db.Permisoes.Where(x => x.Id == id).FirstOrDefault<Permiso>());
                }
            }
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(Permiso permisoObj)
        {
            using (FCPruebaDBEntities db = new FCPruebaDBEntities())
            {
                if (permisoObj.Id == 0)
                {
                    db.Permisoes.Add(permisoObj);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    db.Entry(permisoObj).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated", JsonRequestBehavior.AllowGet });
                }
            }

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (FCPruebaDBEntities db = new FCPruebaDBEntities())
            {
                Permiso permisoObj = db.Permisoes.Where(x => x.Id == id).FirstOrDefault<Permiso>();
                db.Permisoes.Remove(permisoObj);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted", JsonRequestBehavior.AllowGet });
            }
        }

    }
}