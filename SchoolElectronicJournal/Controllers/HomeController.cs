using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolElectronicJournal.Interface;
using SchoolElectronicJournal.Models;
using Microsoft.AspNetCore.Identity;

namespace SchoolElectronicJournal.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        

        public IActionResult Index()
        {
          
            return View("HomeList");
        }
        [Authorize]
        public async Task<IActionResult> List(string id)
        {

           var use = HttpContext.User.Identity.Name;

            var user = userManager.Users.Single(x => x.UserName == use);
            if (string.IsNullOrEmpty(id))
            {
                id = user.Id;
            }
            var role = await userManager.GetRolesAsync(user);
             ViewBag.UserId = id;
             return View(role);           
            
        }
    }
}
