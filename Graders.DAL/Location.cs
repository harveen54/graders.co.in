//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Graders.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Location
    {
        public Location()
        {
            this.InsCourseLocationMapping = new HashSet<InsCourseLocationMapping>();
        }
    
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    
        public virtual ICollection<InsCourseLocationMapping> InsCourseLocationMapping { get; set; }
    }
}
