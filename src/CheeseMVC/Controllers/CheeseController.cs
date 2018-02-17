using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Cheese> cheeses = CheeseData.GetAll();

            return View(cheeses);
        }

        public IActionResult Add()
        {

            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                
                // Add the new cheese to my existing cheeses
                CheeseData.Add(addCheeseViewModel.CreateCheese());

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);

        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                CheeseData.Remove(cheeseId);
            }

            return Redirect("/");
        }


        public IActionResult Edit(int id)
        {
            //TODO
            var cheese = CheeseData.GetById(id);
            AddEditCheeseViewModel addedit = new AddEditCheeseViewModel();

            addedit.Name = cheese.Name;
            addedit.Description = cheese.Description;
            addedit.Type = cheese.Type;
            addedit.cheeseId = cheese.CheeseId;
                

            return View(addedit);
        }

        [HttpPost]
        public IActionResult Edit(AddEditCheeseViewModel addEditCheeseViewModel)
        {
            //TODO

            var cheese = CheeseData.GetById(addEditCheeseViewModel.cheeseId);
            cheese.Name = addEditCheeseViewModel.Name;
            cheese.Description = addEditCheeseViewModel.Description;

            return Redirect("/");

        }
    }
}
