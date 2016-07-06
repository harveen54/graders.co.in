﻿using Graders.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Graders.Common
{
    
        public class MyPrincipal : IPrincipal
        {
            public MyPrincipal(IIdentity identity)
            {
                Identity = identity;
            }

            public IIdentity Identity
            {
                get;
                private set;
            }

            public DtoCustomer Customer { get; set; }

            public bool IsInRole(string role)
            {
                return true;
            }
        }
    
}