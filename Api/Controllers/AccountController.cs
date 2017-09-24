using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;

namespace Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(
            IConfiguration configuration
            )
        {
            _configuration = configuration;
        }

        public IActionResult LogIn(string returnUrl)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            if (
                model.UserName.Equals(_configuration["adminUser"], StringComparison.InvariantCultureIgnoreCase)
                && model.Password.Equals(_configuration["password"])
                )
            {
                // Create the identity from the user info
                var identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    ClaimTypes.Name, 
                    ClaimTypes.Role);

                identity.AddClaim(
                    new Claim(
                        ClaimTypes.NameIdentifier, 
                        model.UserName));

                identity.AddClaim(
                    new Claim(
                        ClaimTypes.Name, model.UserName));

                // Authenticate using the identity
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    principal
                    //, 
                    //new AuthenticationProperties { IsPersistent = false }
                    );


                var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
                var returnUrl = "~/Section/Index";

                if (TempData.Peek("ReturnUrl") != null)
                {
                    var tempReturnUrl = TempData["ReturnUrl"].ToString();
                    if (!string.IsNullOrEmpty(tempReturnUrl) && !tempReturnUrl.Equals("/"))
                    {
                        returnUrl = TempData["ReturnUrl"].ToString();
                    }
                }

                return LocalRedirect(returnUrl);
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> LogOff()
        {
           await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LogIn");
        }
    }
}