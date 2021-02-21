using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolElectronicJournal.Models;
using SchoolElectronicJournal.Interface;
using SchoolElectronicJournal.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace SchoolElectronicJournal.Controllers
{
    public class UserController : Controller
    {
       
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            ViewBag.UserId = userid;
            return View(user);
        }

                               
       
        public async Task<IActionResult> Edit(string id)
        {
            var use = await _userManager.FindByIdAsync(id);
            if (use == null)
            {
                return NotFound();
            }
            EditUserViewModel editUserViewModel = new EditUserViewModel()
            {
                Id = use.Id,
                Email = use.Email,
                Name = use.Name,
                SoName = use.SoName
            };

            return View(editUserViewModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                user.Name = editUserViewModel.Name;
                user.SoName = editUserViewModel.SoName;
                user.Email = editUserViewModel.Email;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "User", new {userid = id });
            }
            return View(editUserViewModel);
           
        }

        public async Task<IActionResult> ChangePassword(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return NotFound();

            }
            ChangePaswordVieewModel changePaswordVieewModel = new ChangePaswordVieewModel()
            {
                Id = user.Id
            };
            return View("ChangePassword", changePaswordVieewModel);
        }
        [HttpPost]
        
        public async Task<IActionResult> ChangePassword(string id, ChangePaswordVieewModel changePaswordVieewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                var result = await _userManager
                   .ChangePasswordAsync(user, changePaswordVieewModel.OldPassword, changePaswordVieewModel.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User", new {userid=id});
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View(changePaswordVieewModel);
        } 

       
        
    }
}
