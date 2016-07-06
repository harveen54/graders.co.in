using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graders.Common
{
    public enum CustomerLoginResults
    {
        /// <summary>
        /// Login successful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// Customer dies not exist (email or username)
        /// </summary>
        CustomerNotExist = 2,
        /// <summary>
        /// Wrong password
        /// </summary>
        WrongPassword = 3,
        /// <summary>
        /// Account have not been activated
        /// </summary>
        NotActive = 4,
        /// <summary>
        /// Customer has been deleted 
        /// </summary>
        Deleted = 5,
        /// <summary>
        /// Customer not registered 
        /// </summary>
        NotRegistered = 6,
    }

    public enum CustomerSignUpResults
    {
        Successful = 1,

        CustomerAlreadyExist = 2,

        Error=3,
    }
}