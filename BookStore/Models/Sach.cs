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
    
    public partial class Sach
    {
        public Sach()
        {
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            this.ThamGias = new HashSet<ThamGia>();
        }
    
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public string MoTa { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public string AnhBia { get; set; }
        public Nullable<int> SoLuongTon { get; set; }
        public Nullable<int> MaChuDe { get; set; }
        public Nullable<int> MaNXB { get; set; }
        public Nullable<int> Moi { get; set; }
    
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual ChuDe ChuDe { get; set; }
        public virtual NhaXuatBan NhaXuatBan { get; set; }
        public virtual ICollection<ThamGia> ThamGias { get; set; }
    }
}
