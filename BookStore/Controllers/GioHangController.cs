using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        #region Giỏ hàng
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int iMaSach, string strURL)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.Find(n => n.iMaSach == iMaSach);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSach);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang(int iMaSP)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == iMaSP);
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult GioHang()
        {
            ViewBag.TongTien = TongTien();
            ViewBag.TongSoLuong = TongSoLuong();
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        #endregion
        #region Đặt hàng
        public void Clear_Cart()
        {
            List<GioHang> GH = LayGioHang();
            GH.Clear();
        }

        [HttpPost]
        public ViewResult DatHang(ThongTinDatHang TTDH)
        {
            if (ModelState.IsValid)
            {
                var giaTien = LayGioHang().Select(x => x.ThanhTien).Sum();
                var listHang = LayGioHang();
                string fileName = @"D:\duan_c#\" + TTDH.Name + TTDH.Phone + ".txt";
                FileInfo fi = new FileInfo(fileName);

                try
                {
                    if (fi.Exists)
                    {
                        fi.Delete();
                    }

                    using (StreamWriter sw = fi.CreateText())
                    {
                        sw.WriteLine("Hoa Don Vao Ngay " + DateTime.Now.ToString());
                        sw.WriteLine("Ten Khach Hang: " + TTDH.Name);
                        sw.WriteLine("Dia Chi: " + TTDH.Address);
                        sw.WriteLine("SDT: " + TTDH.Phone);
                        foreach (var item in listHang)
                        {
                            sw.WriteLine("---Don Hang: " + item.sTenSach + " --- Ma " + item.iMaSach + " ---Ten " + item.sTenSach + " ---SL " + item.iSoLuong + " ---Gia " + item.ThanhTien);
                        }
                        sw.WriteLine("----- Gia: " + giaTien + "------");
                        sw.WriteLine("Done! ");
                    }
                    Clear_Cart();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }
                return View("Thanks");
            }
            else
            {
                return View(TTDH);
            }
        }
        #endregion
    }
}