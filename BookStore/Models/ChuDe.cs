//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChuDe
    {
        public ChuDe()
        {
            this.Saches = new HashSet<Sach>();
        }
    
        public int MaChuDe { get; set; }
        public string TenChuDe { get; set; }
    
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
