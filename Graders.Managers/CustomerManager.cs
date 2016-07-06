using Graders.Common;
using Graders.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graders.Managers
{
    public class CustomerManager
    {
        public static Customer Customer
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // The user is authenticated. Return the user from the forms auth ticket.
                    var dto= ((MyPrincipal)(HttpContext.Current.User)).Customer;
                    return new Customer { UserName = dto.UserName, Password = dto.Password };
                }
                else if (HttpContext.Current.Items.Contains("User"))
                {
                    // The user is not authenticated, but has successfully logged in.
                    return (Customer)HttpContext.Current.Items["User"];
                }
                else
                {
                    return null;
                }
            }
        }

        public  Customer GetCustomerByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            //var query = from c in _customerRepository.Table
            //            orderby c.Id
            //            where c.Username == username
            //            select c;
            //var customer = query.FirstOrDefault();
            var customer = new Customer { UserName = "Harveen", Password = "Harveen" };
            return customer;
        }

        public void Register(string username, string encPassword)
        {
            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(encPassword))
            {
                var customer = new Customer { UserName = username, Password = encPassword };
            }
        }
    }
}