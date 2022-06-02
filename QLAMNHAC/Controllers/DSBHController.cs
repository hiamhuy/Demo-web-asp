using QLAMNHAC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLAMNHAC.Controllers
{
    public class DSBHController : Controller
    {
        // GET: DSBH
     
        QLAMNHACVNEntities2 ql = new QLAMNHACVNEntities2();
            public ActionResult Index()
            {
                List<Baihat> list = ql.Baihat.ToList();
                ViewBag.data = list;
                return View(list);
            }

            public ActionResult Search(string search)
            {
                return View(ql.thongtinnhac.Where(n => n.tenBH.Contains(search) || search == null).ToList());
            }
            public ActionResult Create()
            {

                ViewBag.nhacS = new SelectList(ql.Nhacsi, "Id", "tenNS");
                ViewBag.theL = new SelectList(ql.Theloai, "Id", "tenTL");
                return View();
            }

            public ActionResult add(Baihat bh)
            {
                try { 
                    ql.Entry(bh).State = (System.Data.Entity.EntityState)EntityState.Modified;
                    ql.Baihat.Add(bh);
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

            public ActionResult Edit(int? id)
            {
                Baihat bh = ql.Baihat.Where(n => n.Id == id).FirstOrDefault();
                ViewBag.nhacS = new SelectList(ql.Nhacsi, "Id", "tenNS");
                ViewBag.theL = new SelectList(ql.Theloai, "Id", "tenTL");
                return View(bh);
            }
            public ActionResult sua(Baihat bh)
            {
                try { 
                    if (ModelState.IsValid)
                    {
                        ql.Entry(bh).State = (System.Data.Entity.EntityState)EntityState.Modified;
                        ql.SaveChanges();
                        TempData["ThongBao"] = "Success!";
                    }
                    return RedirectToAction("Index");
                }catch
                {
                    TempData["ThongBao"] = "Fail!";
                    return RedirectToAction("Edit");
                }
            }


            public ActionResult Delete(int? id)
            {
                Baihat bh = ql.Baihat.Where(n => n.Id == id).FirstOrDefault();
                if (bh != null)
                {
                    ql.Baihat.Remove(bh);
                    ql.SaveChanges();
                    TempData["ThongBao"] = "Success!";
                }
                return RedirectToAction("Index");
            }


        }

    }