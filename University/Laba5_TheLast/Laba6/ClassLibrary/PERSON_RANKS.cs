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
    
    public partial class PERSON_RANKS
    {
        public int PRS_PR { get; set; }
        public int PRS_DRK { get; set; }
        public int PRS_ID { get; set; }
    
        public virtual DIC_RANKS DIC_RANKS { get; set; }
        public virtual PERSON PERSON { get; set; }
    }
}
