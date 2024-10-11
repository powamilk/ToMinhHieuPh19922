using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModels
{
    public class ThucAnViewModel
    {
        public string Ten { get; set; }
        public string Loai { get; set; }
        public int SoLuongTonKho { get; set; }
        public decimal DonGia { get; set; }
        public DateTime NgayNhapKho { get; set; }
        public bool TinhTrang { get; set; }
    }
}
