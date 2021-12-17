﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_Manila.Web.Controllers
{
    [Authorize(Policy = "OnlyAdmin")]
    public class ClaimsController : Controller
    {
        private UserManager<IdentityUser> userManager;

        public ClaimsController(UserManager<IdentityUser> userMgr)
        {
            userManager = userMgr;
        }

        [AllowAnonymous]
        public ViewResult Index() => View(User?.Claims);

        public ViewResult Create() => View();

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post(string claimType, string claimValue)
        {
            IdentityUser user = await userManager.GetUserAsync(HttpContext.User);
            Claim claim = new Claim(claimType, claimValue, ClaimValueTypes.String);
            IdentityResult result = await userManager.AddClaimAsync(user, claim);
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string claimValues)
        {
            IdentityUser user = await userManager.GetUserAsync(HttpContext.User);
            string[] claimValuesArray = claimValues.Split(";");
            string claimType = claimValuesArray[0], claimValue = claimValuesArray[1], claimIssuer = claimValuesArray[2];
            Claim claim = User.Claims.FirstOrDefault(x => x.Type == claimType && x.Value == claimValue && x.Issuer == claimIssuer);
            IdentityResult result = await userManager.RemoveClaimAsync(user, claim);
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
            return View("Index");
        }

        void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}