using Graders.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graders.Common;
using Graders.DTO;
using System.Web.Security;
using System.Web;
using Graders.DAL;
using System.Security.Cryptography;
using System.IO;  

namespace Graders.Services
{
    public class CustomerService:ICustomerService, IDisposable
    {
        private CustomerManager _customerManager;
        private readonly TimeSpan _expirationTimeSpan;
        private DtoCustomer _cachedCustomer;
        
        public CustomerService()
        {
            _customerManager = new CustomerManager();
            this._expirationTimeSpan = FormsAuthentication.Timeout;
           }
        public CustomerLoginResults ValidateCustomer(string username, string password)
        {
            Customer customer = _customerManager.GetCustomerByUsername(username);
             
            if (customer == null)
                return CustomerLoginResults.CustomerNotExist;
           
            //only registered can login
           
            string pwd=customer.Password;
            //switch (customer.PasswordFormat)
            //{
            //    case PasswordFormat.Encrypted:
            //        pwd = _encryptionService.EncryptText(password);
            //        break;
            //    case PasswordFormat.Hashed:
            //        pwd = _encryptionService.CreatePasswordHash(password, customer.PasswordSalt, _customerSettings.HashedPasswordFormat);
            //        break;
            //    default:
            //        pwd = password;
            //        break;
            //}// will implement later

            bool isValid = pwd == customer.Password;
            if (!isValid)
                return CustomerLoginResults.WrongPassword;

            //save last login date
            //customer.LastLoginDateUtc = DateTime.UtcNow;
           // _customerService.UpdateCustomer(customer);
            return CustomerLoginResults.Successful;
        }

       public DtoCustomer GetCustomerByUsername(string username)
        {
            var customer = _customerManager.GetCustomerByUsername(username);
            return new DtoCustomer { UserName = "harveebn", Password = "hyep", RememberMe = true };
        }


       /// <summary>
       /// Sign in
       /// </summary>
       /// <param name="customer">Customer</param>
       /// <param name="createPersistentCookie">A value indicating whether to create a persistent cookie</param>
       public HttpCookie SignIn(DtoCustomer customer, bool createPersistentCookie)
       {
           var now = DateTime.UtcNow.ToLocalTime();

           var ticket = new FormsAuthenticationTicket(
               1 /*version*/,
               customer.UserName,
               now,
               now.Add(_expirationTimeSpan),
               createPersistentCookie,
               customer.UserName,
               FormsAuthentication.FormsCookiePath);

           var encryptedTicket = FormsAuthentication.Encrypt(ticket);

           var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
           cookie.HttpOnly = true;
           if (ticket.IsPersistent)
           {
               cookie.Expires = ticket.Expiration;
           }
           cookie.Secure = FormsAuthentication.RequireSSL;
           cookie.Path = FormsAuthentication.FormsCookiePath;
           if (FormsAuthentication.CookieDomain != null)
           {
               cookie.Domain = FormsAuthentication.CookieDomain;
           }

          HttpContext.Current.Response.Cookies.Add(cookie);
           _cachedCustomer = customer;
           return cookie;
       }


         public DtoCustomer GetAuthenticatedCustomer()
        {
            if (_cachedCustomer != null)
                return _cachedCustomer;
            var _httpContext = HttpContext.Current;
            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var customer = GetAuthenticatedCustomerFromTicket(formsIdentity.Ticket);
            if (customer != null)
                _cachedCustomer = customer;
            return _cachedCustomer;
        }

        public  void SignOut()
        {
            _cachedCustomer = null;
            FormsAuthentication.SignOut();
        }

        protected  DtoCustomer GetAuthenticatedCustomerFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var username = ticket.UserData;

            if (String.IsNullOrWhiteSpace(username))
                return null;
            var customer = GetCustomerByUsername(username);
                
           
            return customer;
        }
    
        public CustomerSignUpResults SignUp(string username, string password)
        {
            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var customer = GetCustomerByUsername(username);
                if (customer != null)
                    return CustomerSignUpResults.CustomerAlreadyExist;

                string encryptedPassword = EncryptText(password);
                _customerManager.Register(username, encryptedPassword);
                return CustomerSignUpResults.Successful;
            }
            return CustomerSignUpResults.Error;
        }

        public string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey =  CommonHelper.GenerateRandomDigitCode(16);

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDESalg.Key, tDESalg.IV);
            return Convert.ToBase64String(encryptedBinary);
        }

        private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }
       public void Dispose()
       {
           
       }
    }
}
