using Graders.Common;
using Graders.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Graders.Services
{
    public interface ICustomerService
    {
         CustomerLoginResults ValidateCustomer(string username, string password);
        DtoCustomer GetCustomerByUsername(string username);

        HttpCookie SignIn(DtoCustomer customer, bool createPersistentCookie);

        void SignOut();
        DtoCustomer GetAuthenticatedCustomer();

        CustomerSignUpResults SignUp(string  username, string password);
    }
}
