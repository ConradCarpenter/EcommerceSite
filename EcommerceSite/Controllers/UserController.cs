using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceSite.Data;
using EcommerceSite.Models;
using Microsoft.AspNetCore.Identity;
using EcommerceSite.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceSite.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ItemContext _context;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public UserController(ItemContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName,
            model.Password, false, false);
            
            if (result.Succeeded)
            {
                var user = User;
                return RedirectToAction("Index","Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        { 
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new AppUser { UserName = model.UserName, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction(nameof(UserController.Index), "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var CurrentUser = await _userManager.GetUserAsync(HttpContext.User);
            var items = _context.Items.Where(p => p.User == CurrentUser).ToList();
            return View(items);
        }
        
        [HttpGet]
        public IActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(ItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var CurrentUser = await _userManager.GetUserAsync(HttpContext.User);

            Item item = new Item();

            item.Name = model.Name;
            item.Price = model.Price;
            item.ImageURL = model.ImageURL;
            item.Desc = model.Desc;
            item.User = CurrentUser;

            await _context.Items.AddAsync(item);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(UserController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                Item item = _context.Items.FirstOrDefault(i => i.ItemNumber == id);

                _context.Remove(item);

                await _context.SaveChangesAsync();

                TempData["success"] = "Item Deleted";
            }
            catch
            {
                TempData["fail"] = "Something Went Wrong";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditItem(int id)
        {
            try
            {

            }
            catch
            {

            }
            return View();
        }
    }
}