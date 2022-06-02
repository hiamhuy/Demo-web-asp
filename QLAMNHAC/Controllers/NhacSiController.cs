using QLAMNHAC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace QLAMNHAC.Controllers
{
    public class NhacSiController : Controller
    {
        // GET: NhacSi
        QLAMNHACVNEntities2 ql = new QLAMNHACVNEntities2();

        public ActionResult Index()
        {
            List<Nhacsi> list = ql.Nhacsi.OrderBy(n => n.Id).ToList();
            ViewBag.data = list;
            return View(list);
        }
        public ActionResult Search(string search)
        {
            return View(ql.Nhacsi.Where(n => n.tenNS.Contains(search) || search == null).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult add(Nhacsi ns)
        {
            try
            {
                    ql.Nhacsi.Add(ns);
                    ql.SaveChanges();
                    TempData["ThongBao"] = "Success!";
                    return RedirectToAction("Index");
            }
            catch
            {
                    TempData["ThongBao"] = "Fail!";
                    return RedirectToAction("Create");
            }
        }
        public ActionResult Edit(int id)
        {

            Nhacsi ns = ql.Nhacsi.Where(n => n.Id == id).FirstOrDefault();
            return View(ns);            
        }
        public ActionResult sua(Nhacsi ns)
        {
            if(ModelState.IsValid)
            {
                ql.Entry(ns).State = (System.Data.Entity.EntityState)EntityState.Modified;
                ql.SaveChanges();
                TempData["ThongBao"] = "Success!";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            Nhacsi ns = ql.Nhacsi.Where(n => n.Id == Id).FirstOrDefault();
            if(ns != null)
            {
                ql.Nhacsi.Remove(ns);
                ql.SaveChanges();
                TempData["ThongBao"] = "Success!";
            }
            return RedirectToAction("Index");
        }
    
    }
}