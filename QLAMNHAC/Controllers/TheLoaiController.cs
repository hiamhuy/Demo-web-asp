using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLAMNHAC.Models;

namespace QLAMNHAC.Controllers
{
    public class TheLoaiController : Controller
    {
        // GET: TheLoai
        QLAMNHACVNEntities2 ql = new QLAMNHACVNEntities2();
        public ActionResult Index()
        {
            List<Theloai> list = ql.Theloai.OrderBy(n=>n.Id).ToList();
            ViewBag.data = list;
            return View(list);
        }
        public ActionResult them()
        {
            return View();
        }
  
        public ActionResult Create(Theloai t)
        {
            try
            {
                using (QLAMNHACVNEntities2 ql = new QLAMNHACVNEntities2())
                {
                    ql.Theloai.Add(t);
                    ql.SaveChanges();
                    TempData["ThongBao"] = "Success!";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ThongBao"] = "Fail!";
                return RedirectToAction("them");
            }
        }
        public ActionResult sua(int Id)
        {
            Theloai tl = ql.Theloai.SingleOrDefault(n => n.Id == Id);
            return View(tl);
        }
        public ActionResult edit(Theloai t)
        {
            if (ModelState.IsValid)
            {
                ql.Entry(t).State = System.Data.Entity.EntityState.Modified;
                ql.SaveChanges();
                TempData["ThongBao"] = "Success!";

            }
            return RedirectToAction("Index");
        }
        public ActionResult xoa(int Id)
        {
            Theloai tl = ql.Theloai.SingleOrDefault(n => n.Id == Id);
            if (tl != null)
            {
                ql.Theloai.Remove(tl);
                ql.SaveChanges();
                TempData["Thongbao"] = "Success!";
            }
            return RedirectToAction("Index");
        }
    }
}