using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopMyPham.Models;

namespace ShopMyPham.ViewModel
{
    public class CartModel
    {
        public List<CartItem> Items { get; } = new List<CartItem>();

        public void Add(CartItem item)
        {
            var cartItem = Items.Find(product => product.SanPhamID == item.SanPhamID);
            if (cartItem == null)
                Items.Add(item);
            else
                cartItem.SoLuong += item.SoLuong;
        }

        public decimal Total()
        {
            var total = Items.Sum(product => (product.SoLuong * product.SanPham.GiaBan));
            return total;
        }

        public void Update(int SanPhamID, int quantity)
        {            
            var item = Items.Find(product => product.SanPhamID == SanPhamID);
            item.SoLuong = quantity;
        }

        public void Remove(int SanPhamID)
        {
            var item = Items.Find(product => product.SanPhamID == SanPhamID);
            Items.Remove(item);
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}