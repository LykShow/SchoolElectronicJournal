using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolElectronicJournal.Models.ViewModels;
using SchoolElectronicJournal.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using SchoolElectronicJournal.Models;
using Microsoft.AspNetCore.Identity;

namespace SchoolElectronicJournal.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> UserManager;
        private SignInManager<User> SignInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }     
        [HttpGet]
        public ActionResult Login()
        {
            
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await SignInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    
                        var use = await UserManager.FindByEmailAsync(login.Email);
                        return RedirectToAction("List", "Home", new { id=use.Id});
                    
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(login);
        }
    
        [HttpGet]
        public IActionResult Registr()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registr(Register register)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Name = register.Name, SoName = register.SoName, Email = register.Email, UserName=register.Email};
                var result = await UserManager.CreateAsync(user, register.Password);
                if(result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false);
                    return RedirectToAction("List", "Home", new { id = user.Id });
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            return View(register);
        }
      
        public async Task<IActionResult> Logout()
        {

            await SignInManager.SignOutAsync();
            return RedirectToAction("List", "Home");
        }
       
    }
}
