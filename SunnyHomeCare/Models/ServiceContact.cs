//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SunnyHomeCare.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceContact
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string OtherInfo { get; set; }
        public string JobTitle { get; set; }
        public int PatientId { get; set; }
    
        public virtual Patient Patient { get; set; }
    }
}
