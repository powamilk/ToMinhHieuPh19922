using AppView.Models;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Thêm các dịch vụ vào DI container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IValidator<ThucAnViewModel>, ThucAnViewModelValidator>();

// Cấu hình HttpClient để gọi API
builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7223");
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});

var app = builder.Build();

// Cấu hình xử lý lỗi và HSTS (Chỉ kích hoạt trong môi trường production).
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // The default HSTS value is 30 days.
}

// Cấu hình HTTPS, tệp tĩnh và định tuyến.
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Cấu hình định tuyến cho controller và action.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Chạy ứng dụng.
app.Run();
