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
        public ActionResult Table(string orderBy, string thenBy, int? searchById, string searchByTitle, string searchByDescription, string searchByParent,
            string searchByDateFrom, string searchByDateTo)
        {
            LannistersViewModel model = new LannistersViewModel();
            if (string.IsNullOrEmpty(searchByDateFrom)) searchByDateFrom = DateTime.MinValue.ToString();
            if (string.IsNullOrEmpty(searchByDateTo)) searchByDateTo = DateTime.MaxValue.ToString();
            var a = DateTime.Parse(searchByDateFrom);
            var b = DateTime.Parse(searchByDateTo);


            var target = _repository.Lannisters
                .Where(x => x.Id == (searchById ?? x.Id))
                .Where(x => x.Title.IndexOf(searchByTitle ?? "", StringComparison.OrdinalIgnoreCase) != -1)
                .Where(x => x.Description != null ? x.Description.IndexOf(searchByDescription ?? "", StringComparison.OrdinalIgnoreCase) != -1 : true)//тут надо что-то сделать
                .Where(x => String.IsNullOrEmpty(searchByParent) ? true : x.ParentId == _repository.Lannisters.Where(y => y.Title == searchByParent).FirstOrDefault().Id)
                .Where(x => x.Created.Date.CompareTo(DateTime.Parse(searchByDateFrom))>=0&&x.Created.Date.CompareTo(DateTime.Parse(searchByDateTo))<=0);

            foreach (var lannister in target)
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

            //var firstPropInfo = typeof(LannisterViewModel).GetProperty(orderBy);
            //var secondtPropInfo = typeof(LannisterViewModel).GetProperty(thenBy);

            //if (orderByDescending && thenByDescending) model.Lannisters = model.Lannisters.OrderByDescending(x => firstPropInfo.GetValue(x)).ThenByDescending(x=>secondtPropInfo.GetValue(x)).ToList();
            //else if (orderByDescending && !thenByDescending) model.Lannisters = model.Lannisters.OrderByDescending(x => firstPropInfo.GetValue(x)).ThenBy(x => secondtPropInfo.GetValue(x)).ToList();
            //else if (!orderByDescending && thenByDescending) model.Lannisters = model.Lannisters.OrderBy(x => firstPropInfo.GetValue(x)).ThenByDescending(x => secondtPropInfo.GetValue(x)).ToList();
            //else model.Lannisters = model.Lannisters.OrderBy(x => firstPropInfo.GetValue(x)).ThenBy(x => secondtPropInfo.GetValue(x)).ToList();

            return View(model);
        }

        public ActionResult List()
        {
            return View();
        }
    }
}