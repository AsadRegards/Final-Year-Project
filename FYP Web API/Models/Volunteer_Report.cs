//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FYP_Web_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Volunteer_Report
    {
        public int report_id { get; set; }
        public string volunteer_name { get; set; }
        public string report { get; set; }
        public int volunteer_id { get; set; }
        public int issue_id { get; set; }
    
        public virtual issue_table issue_table { get; set; }
        public virtual Volunteer_table Volunteer_table { get; set; }
    }
}
