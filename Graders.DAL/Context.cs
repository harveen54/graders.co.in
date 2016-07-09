using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Graders.DAL
{
    public class Context:DbContext
    {
        [ThreadStatic]
        private static Context _dbContext;

        public static Context GetContext()
        {
            if(_dbContext==null)
            {
                _dbContext = new Context();
            }
            return _dbContext;
        }

        internal Context():base("name=Context")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}