using AppData.Entities;
using AppData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Interface
{
    public interface IOrderRepository
    {
        decimal TinhTongChiPhi(decimal gia, int soLuong, decimal giamGia, decimal phiVanChuyen, decimal thue);
        int TimSoLonNhatTongChuSoChan(int[] numbers);
        ThucAn ThemThucAn(ThucAnViewModel thucAnVM);
        IEnumerable<ThucAn> LayTatCaThucAn();
        ThucAn LayThucAnTheoId(Guid id);
        ThucAn SuaThucAn(Guid id, ThucAnViewModel thucAnVM);
        bool XoaThucAn(Guid id);
    }
}
