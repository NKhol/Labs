//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class PERSON_AUDIENCE
    {
        public int PAU_PR { get; set; }
        public int PAU_DAU { get; set; }
        public int PAU_ID { get; set; }
    
        public virtual DIC_AUDIENCE DIC_AUDIENCE { get; set; }
        public virtual PERSON PERSON { get; set; }
    }
}
