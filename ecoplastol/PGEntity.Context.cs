﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ecoplastol
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ecoplastolEntities : DbContext
    {
        public ecoplastolEntities()
            : base("name=ecoplastolEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<uzytkownicy> uzytkownicy { get; set; }
        public virtual DbSet<parametry> parametry { get; set; }
        public virtual DbSet<wyroby_druty> wyroby_druty { get; set; }
        public virtual DbSet<itf_cc> itf_cc { get; set; }
        public virtual DbSet<itf_icc> itf_icc { get; set; }
        public virtual DbSet<itf_kategorie> itf_kategorie { get; set; }
        public virtual DbSet<itf_litery> itf_litery { get; set; }
        public virtual DbSet<itf_odch> itf_odch { get; set; }
        public virtual DbSet<itf_sr> itf_sr { get; set; }
        public virtual DbSet<itf_trn> itf_trn { get; set; }
        public virtual DbSet<trace_kategoria> trace_kategoria { get; set; }
        public virtual DbSet<trace_material> trace_material { get; set; }
        public virtual DbSet<trace_mfr> trace_mfr { get; set; }
        public virtual DbSet<trace_pe_m> trace_pe_m { get; set; }
        public virtual DbSet<trace_pe_o> trace_pe_o { get; set; }
        public virtual DbSet<trace_producent> trace_producent { get; set; }
        public virtual DbSet<trace_sdr> trace_sdr { get; set; }
        public virtual DbSet<trace_sr> trace_sr { get; set; }
        public virtual DbSet<trace_litery> trace_litery { get; set; }
        public virtual DbSet<wyroby_zakres_sdr> wyroby_zakres_sdr { get; set; }
        public virtual DbSet<wyroby_typ> wyroby_typ { get; set; }
        public virtual DbSet<wyroby_zast_zaworu> wyroby_zast_zaworu { get; set; }
        public virtual DbSet<wyroby> wyroby { get; set; }
        public virtual DbSet<zmiany> zmiany { get; set; }
        public virtual DbSet<operatorzy_maszyn> operatorzy_maszyn { get; set; }
        public virtual DbSet<wady_nn> wady_nn { get; set; }
        public virtual DbSet<meldunki_wynik> meldunki_wynik { get; set; }
        public virtual DbSet<meldunki_wynik_prz_maszyny> meldunki_wynik_prz_maszyny { get; set; }
        public virtual DbSet<zlecenia_produkcyjne> zlecenia_produkcyjne { get; set; }
        public virtual DbSet<meldunki_wady_nn> meldunki_wady_nn { get; set; }
        public virtual DbSet<meldunki> meldunki { get; set; }
        public virtual DbSet<maszyny> maszyny { get; set; }
    }
}
