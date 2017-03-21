﻿using IngolStadtNatur.Entities.NH.Objects;
using IngolStadtNatur.Entities.NH.Observations;
using IngolStadtNatur.Services.NH.Objects;
using IngolStadtNatur.Services.NH.Observations;
using IngolStadtNatur.Web.Shell.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace IngolStadtNatur.Web.Shell.Controllers
{
    public class ObservationController : Controller
    {
        public ActionResult Index()
        {
            var nodeManager = new NodeManager();
            return View("CategoryGroupList", CategoryListGroupModel.Convert(nodeManager.GetRoot()));
        }

        [ChildActionOnly]
        public ActionResult GetCategoryChild(long id)
        {
            var nodeManager = new NodeManager();
            var node = nodeManager.Get(id);

            if (node is Category)
            {
                return PartialView("_CategoryGroupListItem", CategoryListGroupItemModel.Convert((Category)node));
            }
            else
            {
                return PartialView("_SpeciesGroupListItem", SpeciesListGroupItemModel.Convert((Species)node));
            }
        }

        public ActionResult Category(long id)
        {
            NodeManager nodeManager = new NodeManager();
            return View("CategoryGroupList", CategoryListGroupModel.Convert(nodeManager.Get(id) as Category));
        }



        public ActionResult Thanks()
        {
            return View();
        }

        public ActionResult CreateCategoryObservation(long id)
        {
            NodeManager nodeManager = new NodeManager();
            return View("CreateCategoryObservation", CreateCategoryObservationModel.Convert(nodeManager.Get(id) as Category));
        }

        [HttpPost]
        public ActionResult CreateCategoryObservation(CreateCategoryObservationModel model)
        {
            NodeManager nodeManager = new NodeManager();

            if (ModelState.IsValid)
            {
                ObservationManager observationManager = new ObservationManager();

                var observation = new CategoryObservation()
                {
                    Comment = model.Comment,
                    Coordinates = model.Coordinates,
                    CreationDate = DateTime.Now,
                    MeasurementDate = model.Date,
                    Node = nodeManager.Get(model.Category.Id),
                };

                observationManager.Create(observation);

                if (model.Shot != null)
                {
                    ShotManager shotManager = new ShotManager();

                    var shot = new Shot()
                    {
                        Name = observation.Id + Path.GetExtension(model.Shot.FileName),
                        Observation = observation
                    };

                    model.Shot.SaveAs(Path.Combine(ConfigurationManager.AppSettings["Shots"], shot.Name));

                    shotManager.Create(shot);
                }

                return RedirectToAction("Thanks", "Observation");
            }

            model.Category = CategoryModel.Convert(nodeManager.Get(model.Category.Id) as Category);
            return View("CreateCategoryObservation", model);
        }

        public ActionResult CreateQuickObservation()
        {
            return View("CreateQuickObservation", new CreateQuickObservationModel());
        }

        [HttpPost]
        public ActionResult CreateQuickObservation(CreateQuickObservationModel model)
        {
            if (ModelState.IsValid)
            {
                NodeManager nodeManager = new NodeManager();
                ObservationManager observationManager = new ObservationManager();

                var observation = new CategoryObservation()
                {
                    Comment = model.Comment,
                    Coordinates = model.Coordinates,
                    CreationDate = DateTime.Now,
                    MeasurementDate = model.Date,
                    Node = nodeManager.GetRoot()
                };

                observationManager.Create(observation);

                if (model.Shot != null)
                {
                    ShotManager shotManager = new ShotManager();

                    var shot = new Shot()
                    {
                        Name = observation.Id + Path.GetExtension(model.Shot.FileName),
                        Observation = observation
                    };

                    model.Shot.SaveAs(Path.Combine(ConfigurationManager.AppSettings["Shots"], shot.Name));
                    shotManager.Create(shot);
                }

                return RedirectToAction("Thanks", "Observation");
            }

            return View("CreateQuickObservation", model);
        }


        public ActionResult CreateSpeciesObservation(long id)
        {
            NodeManager nodeManager = new NodeManager();
            return View("CreateSpeciesObservation", CreateSpeciesObservationModel.Convert(nodeManager.Get(id) as Species));
        }

        [HttpPost]
        public ActionResult CreateSpeciesObservation(CreateSpeciesObservationModel model)
        {
            NodeManager nodeManager = new NodeManager();

            if (ModelState.IsValid)
            {
                ObservationManager observationManager = new ObservationManager();

                var observation = new SpeciesObservation()
                {
                    Comment = model.Comment,
                    Coordinates = model.Coordinates,
                    CreationDate = DateTime.Now,
                    MeasurementDate = model.Date,
                    Node = nodeManager.Get(model.Species.Id)
                };

                observationManager.Create(observation);

                if (model.Shot != null)
                {
                    ShotManager shotManager = new ShotManager();

                    var shot = new Shot()
                    {
                        Name = observation.Id + Path.GetExtension(model.Shot.FileName),
                        Observation = observation
                    };

                    model.Shot.SaveAs(Path.Combine(ConfigurationManager.AppSettings["Shots"], shot.Name));

                    shotManager.Create(shot);
                }

                return RedirectToAction("Thanks", "Observation");
            }

            model.Species = SpeciesModel.Convert(nodeManager.Get(model.Species.Id) as Species);
            return View("CreateSpeciesObservation", model);
        }

        public JsonResult GetSpeciesNames(string query)
        {
            NodeManager nodeManager = new NodeManager();

            List<string> commonNames = nodeManager.SpeciesRepository.Query(m => m.CommonName.ToLower().Contains(query.ToLower())).Select(m => m.CommonName).ToList();
            List<string> scientificNames = nodeManager.SpeciesRepository.Query(m => m.ScientificName.ToLower().Contains(query.ToLower())).Select(m => m.ScientificName).ToList();
            return this.Json(commonNames.Union(scientificNames), JsonRequestBehavior.AllowGet);
        }
    }
}