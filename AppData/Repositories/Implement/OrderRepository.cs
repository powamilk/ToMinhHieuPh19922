using AppData.Entities;
using AppData.Repositories.Interface;
using AppData.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.Implement
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public decimal TinhTongChiPhi(decimal gia, int soLuong, decimal giamGia, decimal phiVanChuyen, decimal thue)
        {
            var tienHang = gia * soLuong;
            var tienGiamGia = tienHang * giamGia / 100;
            var tongChiPhi = (tienHang - tienGiamGia) + phiVanChuyen + ((tienHang - tienGiamGia) * thue / 100);
            return tongChiPhi;
        }

        public int TimSoLonNhatTongChuSoChan(int[] numbers)
        {
            var result = numbers
                .Where(n => n.ToString().Sum(c => c - '0') % 2 == 0)
                .DefaultIfEmpty(int.MinValue)
                .Max();

            return result;
        }

        public ThucAn ThemThucAn(ThucAnViewModel thucAnVM)
        {
            var thucAn = _mapper.Map<ThucAn>(thucAnVM);
            thucAn.ID = Guid.NewGuid();
            _context.ThucAns.Add(thucAn);
            _context.SaveChanges();
            return thucAn;
        }

        public IEnumerable<ThucAn> LayTatCaThucAn()
        {
            return _context.ThucAns.ToList();
        }

        public ThucAn LayThucAnTheoId(Guid id)
        {
            return _context.ThucAns.Find(id);
        }

        public ThucAn SuaThucAn(Guid id, ThucAnViewModel thucAnVM)
        {
            var existingThucAn = _context.ThucAns.Find(id);
            if (existingThucAn == null)
            {
                return null;
            }

            _mapper.Map(thucAnVM, existingThucAn);
            _context.SaveChanges();
            return existingThucAn;
        }

        public bool XoaThucAn(Guid id)
        {
            var thucAn = _context.ThucAns.Find(id);
            if (thucAn == null)
            {
                return false;
            }

            _context.ThucAns.Remove(thucAn);
            _context.SaveChanges();
            return true;
        }
    }
}
