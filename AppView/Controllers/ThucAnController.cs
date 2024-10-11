using AppView.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class ThucAnController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IValidator<ThucAnViewModel> _validator;

        public ThucAnController(IHttpClientFactory clientFactory, IValidator<ThucAnViewModel> validator)
        {
            _clientFactory = clientFactory;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("APIClient");
            var response = await client.GetAsync("/api/Order/lay-tat-ca-thuc-an");
            if (response.IsSuccessStatusCode)
            {
                var thucAnList = await response.Content.ReadFromJsonAsync<List<ThucAnViewModel>>();
                return View(thucAnList);
            }

            return View(new List<ThucAnViewModel>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ThucAnViewModel thucAnVM)
        {
            var validationResult = await _validator.ValidateAsync(thucAnVM);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(thucAnVM);
            }

            var client = _clientFactory.CreateClient("APIClient");
            var response = await client.PostAsJsonAsync("/api/Order/them-thuc-an", thucAnVM);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi thêm mới thức ăn.");
            return View(thucAnVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = _clientFactory.CreateClient("APIClient");
            var response = await client.GetAsync($"/api/Order/lay-thuc-an/{id}");
            if (response.IsSuccessStatusCode)
            {
                var thucAn = await response.Content.ReadFromJsonAsync<ThucAnViewModel>();
                return View(thucAn);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ThucAnViewModel thucAnVM)
        {
            var validationResult = await _validator.ValidateAsync(thucAnVM);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(thucAnVM);
            }

            var client = _clientFactory.CreateClient("APIClient");
            var response = await client.PutAsJsonAsync($"/api/Order/sua-thuc-an/{id}", thucAnVM);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi cập nhật thông tin thức ăn.");
            return View(thucAnVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = _clientFactory.CreateClient("APIClient");

            var response = await client.GetAsync($"/api/Order/lay-thuc-an/{id}");
            if (response.IsSuccessStatusCode)
            {
                var thucAn = await response.Content.ReadFromJsonAsync<ThucAnViewModel>();
                return View(thucAn);
            }

            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var client = _clientFactory.CreateClient("APIClient");

            var response = await client.DeleteAsync($"/api/Order/xoa-thuc-an/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi xóa thức ăn.");
            return RedirectToAction("Index");
        }
    }
}
