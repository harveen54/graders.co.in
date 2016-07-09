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
                    var dto = ((MyPrincipal)(HttpContext.Current.User)).Customer;
                    return new Customer { Username = dto.UserName, Password = dto.Password };
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

        public Customer GetCustomerByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            using (var customer = new EntityRepositoryBase<Customer>())
            {
                return customer.Single(q => q.Username == username);
            }
            //var customer = new Customer { Username = "Harveen", Password = "Harveen" };
            //return customer;
        }

        public string GetEncryptionKey()
        {
            using(var setting= new EntityRepositoryBase<Setting>())
            {
               return setting.Single(q => q.Name == "EncryptionKey").Value;
            }
        }

        public void Register(string username, string encPassword)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(encPassword))
            {
                var customer = new Customer
                {
                    Username = username,
                    Password = encPassword,
                    AdminComment = "",
                    CreatedOnUtc = DateTime.Now,
                    
                    HasShoppingCartItems = false,
                    LastIpAddress = CommonHelper.GetCurrentIpAddress(),
                    LastActivityDateUtc = DateTime.Now,
                    LastLoginDateUtc = DateTime.Now,
                    RoleId = 1

                };
                using(var cust = new EntityRepositoryBase<Customer>())
                {
                    cust.Add(customer);
                }
            }
        }
    }
}