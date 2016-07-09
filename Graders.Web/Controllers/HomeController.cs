using Graders.Common;
using Graders.DTO;
using Graders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graders.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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
            return PartialView("Login", model);
        }

    }
}