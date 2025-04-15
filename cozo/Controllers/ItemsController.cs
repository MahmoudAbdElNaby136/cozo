using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cozo.Handler;
using cozo.Models;
using cozo.ViewModels;
using EntityState = System.Data.Entity.EntityState;

namespace cozo.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemHandler _handler = new ItemHandler();

        public ActionResult Index(string search, string sort)
        {
            var items = _handler.GetList(search, sort);
            ViewBag.Search = search;
            ViewBag.Sort = sort;
            return View(items);
        }

        public ActionResult Form(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    var model = _handler.GetSingle(id.Value);
                    return View(model);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return RedirectToAction("Index");
                }
            }

            return View(ItemPM.Create()); // Create mode
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Form(ItemPM model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    _handler.Save(model);
        //    return RedirectToAction("Form", new { id = model.Id });
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Form(ItemPM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _handler.Save(model);
            return RedirectToAction("Form", new { id = model.Id });
        }


        public ActionResult Delete(int id)
        {
            _handler.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
