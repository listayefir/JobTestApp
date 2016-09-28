using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.Abstract;//заменить на эбстрэкт
using TestApplication.Models;

namespace TestApplication.Controllers
{
    public class LannistersController : Controller
    {
        //заменить на абстрактный
        private ILannisterRepository _repository;

        public LannistersController(ILannisterRepository repository)
        {
            _repository = repository;
        }
        // GET: Lannisters
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Table()
        {
            LannistersViewModel model = new LannistersViewModel();

            foreach (var lannister in _repository.Lannisters)
            {
                var newLannister = new LannisterViewModel
                {
                    Id = lannister.Id,
                    Title = lannister.Title,
                    Parent = _repository.Lannisters
                                .Where(x => x.Id == lannister.ParentId)
                                .Select(x => x.Title)
                                .FirstOrDefault(),
                    Created = lannister.Created.Date,
                    Description = lannister.Description
                };
              
                model.Lannisters.Add(newLannister);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Table(string orderBy, string thenBy, bool orderByDescending, bool thenByDescending)
        {
            LannistersViewModel model = new LannistersViewModel();

            foreach (var lannister in _repository.Lannisters)
            {
                var newLannister = new LannisterViewModel
                {
                    Id = lannister.Id,
                    Title = lannister.Title,
                    Parent = _repository.Lannisters
                                .Where(x => x.Id == lannister.ParentId)
                                .Select(x => x.Title)
                                .FirstOrDefault(),
                    Created = lannister.Created.Date,
                    Description = lannister.Description
                };

                model.Lannisters.Add(newLannister);
            }

            var firstPropInfo = typeof(LannisterViewModel).GetProperty(orderBy);
            var secondtPropInfo = typeof(LannisterViewModel).GetProperty(thenBy);

            if (orderByDescending && thenByDescending) model.Lannisters = model.Lannisters.OrderByDescending(x => firstPropInfo.GetValue(x)).ThenByDescending(x=>secondtPropInfo.GetValue(x)).ToList();
            else if (orderByDescending && !thenByDescending) model.Lannisters = model.Lannisters.OrderByDescending(x => firstPropInfo.GetValue(x)).ThenBy(x => secondtPropInfo.GetValue(x)).ToList();
            else if (!orderByDescending && thenByDescending) model.Lannisters = model.Lannisters.OrderBy(x => firstPropInfo.GetValue(x)).ThenByDescending(x => secondtPropInfo.GetValue(x)).ToList();
            else model.Lannisters = model.Lannisters.OrderBy(x => firstPropInfo.GetValue(x)).ThenBy(x => secondtPropInfo.GetValue(x)).ToList();

            return View(model);
        }

        public ActionResult List()
        {
            return View();
        }
    }
}