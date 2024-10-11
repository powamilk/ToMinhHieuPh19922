using FluentValidation;

namespace AppView.Models
{
    public class ThucAnViewModelValidator : AbstractValidator<ThucAnViewModel>
    {
        public ThucAnViewModelValidator()
        {
            RuleFor(x => x.Ten)
                .NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(30).WithMessage("Tên không được vượt quá 30 ký tự.");

            RuleFor(x => x.DonGia)
                .GreaterThan(0).WithMessage("Đơn giá phải lớn hơn 0.");

            RuleFor(x => x.SoLuongTonKho)
                .GreaterThanOrEqualTo(0).WithMessage("Số lượng tồn kho không được nhỏ hơn 0.");
        }
    }
}
