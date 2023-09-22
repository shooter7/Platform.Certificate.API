using Platform.Certificate.API.Common.Extensions;
using Platform.Certificate.API.Common.Helpers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Add services to the container.
services.AddControllersConfig();
services.AddHttpContextAccessor();
services.AddMyDatabase(builder.Configuration);
services.AddMyAuthentication();
services.AddMyCors(Constants.AllowOrigin);
services.AddMyMapper();
services.AddMyValidation();
services.AddMySwagger();
services.AddMyScope();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseCors(Constants.AllowOrigin);
app.UseAuthorization();

app.UseStaticFiles();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();