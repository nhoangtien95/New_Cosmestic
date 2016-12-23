using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopMyPham.Models;

namespace ShopMyPham.ViewModel
{
    public class IndexViewModel
    {
        public IList<Loai> Loai { get; set; }
        public IList<Loai> Eye { get; set; }
        public IList<Loai> Face { get; set; }
        public IList<Loai> Tool { get; set; }
        public IList<Loai> Nails { get; set; }
        public IList<Loai> Skin { get; set; }
        public IList<ThuongHieu> ThuongHieu { get; set; }
        public IndexViewModel(IList<Loai> Loai, IList<Loai> Eye, IList<Loai> Face, IList<Loai> Tool, IList<Loai> Nails, IList<Loai> Skin, IList<ThuongHieu> ThuongHieu)
        {
            this.Loai = Loai;
            this.Eye = Eye;
            this.Face = Face;
            this.Tool = Tool;
            this.Nails = Nails;
            this.Skin = Skin;
            this.ThuongHieu = ThuongHieu;
        }
        public IndexViewModel()
        {
            // Default
            Loai = new List<Loai>();
            Eye = new List<Loai>();
            Face = new List<Loai>();
            Tool = new List<Loai>();
            Nails = new List<Loai>();
            Skin = new List<Loai>();
            ThuongHieu = new List<ThuongHieu>();
        }
    }
}