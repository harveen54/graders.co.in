using Graders.Common;
using Graders.DTO;
using Graders.Services;
using Graders.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Graders.Web.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult UserLogin(DtoCustomer model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (var customerService = new CustomerService())
                {
                   
                    var loginResult = customerService.ValidateCustomer(model.UserName, model.Password);
                    switch (loginResult)
                    {
                        case CustomerLoginResults.Successful:
                            {
                                var customer = customerService.GetCustomerByUsername(model.UserName);

                                //sign in new customer
                                HttpCookie cookie = customerService.SignIn(customer, model.RememberMe);
                                HttpContext.Response.Cookies.Add(cookie);
                                //logs
                                ////activity log
                                //_customerActivityService.InsertActivity("PublicStore.Login", _localizationService.GetResource("ActivityLog.PublicStore.Login"), customer);

                                if (String.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                                    return RedirectToAction("Index", "Home");

                                return Redirect(returnUrl);
                            }
                        case CustomerLoginResults.CustomerNotExist:
                            ModelState.AddModelError("", Common.Resource.CustomerLogin_DoesNotExist);
                            break;
                        case CustomerLoginResults.WrongPassword:
                        default:
                            ModelState.AddModelError("", Common.Resource.CustomerLogin_WrongPassword);
                            break;
                    }
                }
            }
            return View("Login",model);
        }

   
        public ActionResult LogOut()
        {
             using (var customerService = new CustomerService())
             {
                 customerService.SignOut();
             }
             HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
             cookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Response.Cookies.Add(cookie);
             return RedirectToAction("Index","Home");
        }


        public ActionResult Register()
        {
            return View();
        }
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                int flag = 0;
                if(model.ConfirmPassword!=model.Password)
                {
                    ModelState.AddModelError("", Common.Resource.CustomerSignUp_WrongPassword);
                    flag = 1;
                }
                if (String.IsNullOrEmpty(model.UserName))
                {
                    ModelState.AddModelError("", Common.Resource.CustomerSignUp_UsernameRequired);
                    flag = 1;
                }
                if (!CommonHelper.IsValidEmail(model.UserName))
                {
                    ModelState.AddModelError("", Common.Resource.CustomerSignUp_WrongEmailFormat);
                    flag = 1;
                }
                if (String.IsNullOrWhiteSpace(model.Password))
                {
                    ModelState.AddModelError("", Common.Resource.CustomerSignUp_PasswordRequired);
                    flag = 1;
                }
                if (flag == 1)
                {
                    return View(model);
                }
                else
                {
                    using (var customerService = new CustomerService())
                    {
                        var signupResult = customerService.SignUp(model.UserName, model.Password);
                        switch (signupResult)
                        {
                            case CustomerSignUpResults.Successful:
                                {

                                    if (String.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                                        return RedirectToAction("Index", "Home");

                                    return Redirect(returnUrl);
                                }
                            case CustomerSignUpResults.CustomerAlreadyExist:
                                {
                                    ModelState.AddModelError("", Common.Resource.CustomerSignUp_UserExistsAlready);
                                    return View(model);
                                    break;
                                }
                            case CustomerSignUpResults.Error:
                                {
                                    ModelState.AddModelError("", Common.Resource.CustomerSignUp_Error);
                                    return View(model);
                                    break;
                                }
                        }

                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}