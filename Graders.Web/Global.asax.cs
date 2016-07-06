using Graders.Common;
using Graders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Graders.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            using (var customerService = new CustomerService())
            {
                HttpContextBase http = new HttpContextWrapper(HttpContext.Current) as HttpContextBase;

                customerService.GetAuthenticatedCustomer();

                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    var identity = new GenericIdentity(authTicket.Name, "Forms");
                    var principal = new MyPrincipal(identity);

                    var customer = customerService.GetCustomerByUsername(authTicket.Name);
                    
                //    string userData = ((FormsIdentity)(Context.User.Identity)).Ticket.UserData;
                    principal.Customer = customer;

                    // string userData = ((FormsIdentity)(Context.User.Identity)).Ticket.UserData;

                    Context.User = principal;
                } 
            }
        }
    }
}
