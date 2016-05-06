﻿using Microsoft.AspNet.Mvc;
using ASPNetExercises.Models;
using ASPNetExercises.ViewModels;

namespace ASPNetExercises.Controllers
{
    public class CategoryController : Controller
    {
        AppDbContext _db;
        public CategoryController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            CategoryModel model = new CategoryModel(_db);
            CategoryViewModel viewModel = new CategoryViewModel();
            viewModel.Categories = model.GetAll();
            return View(viewModel);
        }
    }
}