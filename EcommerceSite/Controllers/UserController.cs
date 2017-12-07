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
            var up = new List<UserPurchased>();
            try
            {
                up = _context.UserPurchased.Where(i => i.ItemNumber == items.First().ItemNumber).ToList();
            }
            catch 
            {
            }
            
            ViewBag.number = up.Count;

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
            ItemViewModel vm = new ItemViewModel();
            try
            {
                var item = _context.Items.FirstOrDefault(i => i.ItemNumber == id);
                vm.Number = item.ItemNumber;
                vm.Price = item.Price;
                vm.ImageURL = item.ImageURL;
                vm.Name = item.Name;
                vm.Desc = item.Desc;
            }
            catch
            {

            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditItem(ItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
               var item = _context.Items.FirstOrDefault(i => i.ItemNumber == model.Number);
                item.Name = model.Name;
                item.Price = model.Price;
                item.Desc = model.Desc;
                item.ImageURL = model.ImageURL;

                await _context.SaveChangesAsync();
            }
            catch
            {
            }

            return RedirectToAction("Index");

        }

        public IActionResult Details(int id)
        {
            var emails = new List<string>();
            try
            {
                emails = _context.UserPurchased.Where(u => u.ItemNumber == id).Select(p => p.User.Email).ToList();
            }
            catch
            {
            }
            
     
            
            
            return View(emails);
        }
    }
}