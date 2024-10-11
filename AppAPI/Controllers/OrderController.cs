using AppData.Repositories.Interface;
using AppData.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IValidator<ThucAnViewModel> _validator;

        public OrderController(IOrderRepository orderRepository, IValidator<ThucAnViewModel> validator)
        {
            _orderRepository = orderRepository;
            _validator = validator;
        }


        [HttpGet("tinh-tong-chi-phi")]
        public IActionResult TinhTongChiPhi(decimal gia, int soLuong, decimal giamGia, decimal phiVanChuyen, decimal thue)
        {
            var tongChiPhi = _orderRepository.TinhTongChiPhi(gia, soLuong, giamGia, phiVanChuyen, thue);
            return Ok(tongChiPhi);
        }

        [HttpPost("tim-so-lon-nhat-tong-chu-so-chan")]
        public IActionResult TimSoLonNhatTongChuSoChan([FromBody] int[] numbers)
        {
            var result = _orderRepository.TimSoLonNhatTongChuSoChan(numbers);
            return Ok(result);
        }

        [HttpPost("them-thuc-an")]
        public async Task<IActionResult> ThemThucAn([FromBody] ThucAnViewModel thucAnVM)
        {
            var validationResult = await _validator.ValidateAsync(thucAnVM);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));
            }

            var thucAn = _orderRepository.ThemThucAn(thucAnVM);
            return CreatedAtAction(nameof(LayThucAnTheoId), new { id = thucAn.ID }, thucAn);
        }

        [HttpGet("lay-tat-ca-thuc-an")]
        public IActionResult LayTatCaThucAn()
        {
            var thucAns = _orderRepository.LayTatCaThucAn();
            return Ok(thucAns);
        }

        [HttpGet("lay-thuc-an/{id}")]
        public IActionResult LayThucAnTheoId(Guid id)
        {
            var thucAn = _orderRepository.LayThucAnTheoId(id);
            if (thucAn == null)
            {
                return NotFound(new { Message = "Id Thức ăn không tồn tại." });
            }
            return Ok(thucAn);
        }

        [HttpPut("sua-thuc-an/{id}")]
        public async Task<IActionResult> SuaThucAn(Guid id, [FromBody] ThucAnViewModel thucAnVM)
        {
            var validationResult = await _validator.ValidateAsync(thucAnVM);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));
            }

            var updatedThucAn = _orderRepository.SuaThucAn(id, thucAnVM);
            if (updatedThucAn == null)
            {
                return NotFound(new { Message = "Id Thức ăn không tồn tại." });
            }
            return Ok(updatedThucAn);
        }

        [HttpDelete("xoa-thuc-an/{id}")]
        public IActionResult XoaThucAn(Guid id)
        {
            var result = _orderRepository.XoaThucAn(id);
            if (!result)
            {
                return NotFound(new { Message = "Id Thức ăn không tồn tại." });
            }
            return NoContent();
        }
    }
}
