//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace weatherpro.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class place
    {
        public int pid { get; set; }
        public string city { get; set; }
        public Nullable<double> coord_lon { get; set; }
        public Nullable<double> coord_lat { get; set; }
    
        public virtual wday wday { get; set; }
        public virtual wthreeh wthreeh { get; set; }
    }
}
