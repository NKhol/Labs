﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_CyberneticsEntities : DbContext
    {
        public DB_CyberneticsEntities()
            : base("name=DB_CyberneticsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DIC_AUDIENCE> DIC_AUDIENCE { get; set; }
        public virtual DbSet<DIC_CHAIRS> DIC_CHAIRS { get; set; }
        public virtual DbSet<DIC_DEGREE> DIC_DEGREE { get; set; }
        public virtual DbSet<DIC_RANKS> DIC_RANKS { get; set; }
        public virtual DbSet<PERSON> PERSON { get; set; }
        public virtual DbSet<PERSON_AUDIENCE> PERSON_AUDIENCE { get; set; }
        public virtual DbSet<PERSON_CHAIR> PERSON_CHAIR { get; set; }
        public virtual DbSet<PERSON_RANKS> PERSON_RANKS { get; set; }
        public virtual DbSet<LOGIN> LOGIN { get; set; }
    }
}
