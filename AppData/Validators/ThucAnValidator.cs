using AppData.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Validators
{
    public class ThucAnValidator : AbstractValidator<ThucAnViewModel>
    {
        public ThucAnValidator()
        {
            RuleFor(x => x.Ten)
                .NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(30).WithMessage("Tên không được vượt quá 30 kí tự.");

            RuleFor(x => x.DonGia)
                .GreaterThan(0).WithMessage("Đơn giá phải là số dương.");

            RuleFor(x => x.SoLuongTonKho)
                .GreaterThanOrEqualTo(0).WithMessage("Số lượng tồn kho không được nhỏ hơn 0.");
        }
    }
}
