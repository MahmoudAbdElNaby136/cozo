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
        private readonly ItemHandler Handler = new ItemHandler();
        [HttpGet]
        public ActionResult Index(ItemsList model)
        {
            model.Items = Handler.GetList(model); 
            ViewBag.CurrentPage = model.Page;
            ViewBag.TotalPages = model.TotalPages;
            if (model.SearchTerm==null && model.SortOrder==null)
            {
                return View(model);
            }
            return PartialView("_TablePartialView", model.Items);
        }

        [HttpGet]
        public ActionResult Form(int? id)
        {
            if (id.HasValue)
            {
                var Item = Handler.GetSingle(id);
                return View(Item);
            }
            return View(ItemPM.Create());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ItemPM itemPM)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ItemFormPartial", itemPM);
            }
            Handler.Save(itemPM);
            return RedirectToAction("Form", new { id = itemPM.Id });
        }
        [HttpPost]
        public ActionResult Delete(int? Id)
        {
            Handler.Delete(Id);
            return Json(new { });
        }

        public ActionResult DeleteOne(int? Id)
        {
            Handler.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
