using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Hostel.WebAPI.Data;
using Hostel.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=hostel_api.db"));
builder.Services.AddScoped<ILezajService, LezajService>();
builder.Services.AddScoped<IRezervacijaService, RezervacijaService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title="HostelHub Web API", Version="v1", Description="RESTful API za upravljanje hostelom - rezervacije ležajeva" });
});
builder.Services.AddCors(o => o.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json","HostelHub API v1"); c.RoutePrefix=string.Empty; });
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
using (var scope=app.Services.CreateScope()) { scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.EnsureCreated(); }
app.Run();
