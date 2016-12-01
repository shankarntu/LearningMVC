﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Models
{
    public class NavController : Controller
    {
        private IPorductsRepository repository;

        public NavController(IPorductsRepository repo) {
            repository = repo;
        }
        // GET: Nav
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);
        }
    }
}