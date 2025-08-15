using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minimal.Api.Extensions;
using Minimal.Application.Interfaces.Services;
using Minimal.Application.ModelViews;
using Minimal.Application.ViewModels.IOs.Administrator;
using Minimal.Application.ViewModels.IOs.Authenticate;
using Minimal.Application.ViewModels.IOs.Vehicle;

namespace Minimal.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        key = Configuration?.GetSection("Jwt")?.ToString() ?? "";
    }

    private string key = "";
    public IConfiguration Configuration { get; set; } = default!;

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddDatabase(Configuration)
            .AddJwtAuthentication(Configuration)
            .AddPresentationLayer()
            .ConfigureInjectionDependency();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors();

        app.UseEndpoints(endpoints =>
        {

            #region Home
            endpoints.MapGet("/", () => Results.Json(new Home())).AllowAnonymous().WithTags("Home");
            #endregion

            #region Administratores
            endpoints.MapPost("/Administratores/Login", async ([FromBody] InputLoginAuthenticate inputLoginAuthenticate, IAdministratorService administratorService) =>
            {
                try
                {
                    return Results.Ok(await administratorService.Login(inputLoginAuthenticate));
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).AllowAnonymous().WithTags("Administratores");

            endpoints.MapPost("/Administratores/GetAllPagination", ([FromBody] InputPaginationAdministrator inputPaginationAdministrator, IAdministratorService administratorService) =>
            {
                try
                {
                    return Results.Ok(administratorService.GetAllWithPagination(inputPaginationAdministrator));
                }
                catch
                {
                    return Results.Problem("Erro Interno");
                }
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" })
            .WithTags("Administratores");

            endpoints.MapGet("/Administratores/GetAll", async (IAdministratorService administratorService) =>
            {
                try
                {
                    return Results.Ok(await administratorService.GetAllAsync());
                }
                catch
                {
                    return Results.Problem("Erro Interno");
                }
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" })
            .WithTags("Administratores");

            endpoints.MapGet("/Administratores/GetById", async ([FromBody] InputIdentityViewAdministrator inputIdentityViewAdministrator, IAdministratorService administratorService) =>
            {
                try
                {
                    return Results.Ok(await administratorService.GetByIdAsync(inputIdentityViewAdministrator));
                }
                catch
                {
                    return Results.Problem("Erro Interno");
                }
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" })
            .WithTags("Administratores");

            endpoints.MapPost("/Administratores/Register", async ([FromBody] InputCreateAdministrator inputCreateAdministrator, IAdministratorService administratorService) =>
            {
                try
                {
                    var administratores = await administratorService.Register(inputCreateAdministrator);

                    if (!administratores.IsSuccess)
                        return Results.BadRequest(administratores.listNotification);

                    return Results.Ok(administratores);
                }
                catch
                {
                    return Results.Problem("Erro Interno");
                }
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" })
            .WithTags("Administratores");
            #endregion

            #region Vehicles
            endpoints.MapPost("/Vehicles/Create", async ([FromBody] InputCreateVehicle inputCreateVehicle, IVehicleService vehicleService) =>
            {
                try
                {
                    var vehicle = await vehicleService.Create(inputCreateVehicle);

                    if (vehicle.IsSuccess)
                        return Results.Created($"/api/vehicles/{vehicle.Value!.Id}", vehicle.Value);

                    return Results.BadRequest(vehicle.listNotification);
                }
                catch
                {
                    return Results.Problem("Erro Interno");
                }
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador, Editor" })
            .WithTags("Vehicles");

            endpoints.MapPost("/Vehicles/GetAllPagination", ([FromBody] InputPaginationVehicle inputPaginationVehicle, IVehicleService vehicleService) =>
            {
                try
                {
                    return Results.Ok(vehicleService.GetAllWithPagination(inputPaginationVehicle));
                }
                catch
                {
                    return Results.BadRequest();
                }
            }).RequireAuthorization().WithTags("Vehicles");

            endpoints.MapGet("/Vehicles/GetAll", async (IVehicleService vehicleService) =>
            {
                try
                {
                    return Results.Ok(await vehicleService.GetAllAsync());
                }
                catch
                {
                    return Results.Problem("Erro Interno");
                }
            }).RequireAuthorization().WithTags("Vehicles");

            endpoints.MapPost("/Vehicles/GetId", async ([FromBody] InputIdentityViewVehicle inputIdentityViewVehicle, IVehicleService vehicleService) =>
            {
                try
                {
                    var vehicle = await vehicleService.GetByIdAsync(inputIdentityViewVehicle);
                    if (vehicle == null) return Results.NotFound();
                    return Results.Ok(vehicle);
                }
                catch
                {
                    return Results.Problem("Erro Interno");
                }
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador,Editor" })
            .WithTags("Vehicles");

            endpoints.MapPut("/Vehicles/Update", async ([FromBody] InputIdentityUpdateVehicle inputIdentityUpdateVehicle, IVehicleService vehicleService) =>
            {
                try
                {
                    var vehicle = await vehicleService.Update(inputIdentityUpdateVehicle);

                    if (vehicle.IsSuccess)
                        return Results.Ok(vehicle.listNotification);

                    return Results.BadRequest(vehicle.listNotification);
                }
                catch
                {
                    return Results.Problem("Erro Interno");
                }
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" })
            .WithTags("Vehicles");

            endpoints.MapDelete("/Vehicles/Delete", async ([FromBody] InputIdentityDeleteVehicle inputIdentityDeleteVehicle, IVehicleService vehicleService) =>
            {
                try
                {
                    var vehicle = await vehicleService.Delete(inputIdentityDeleteVehicle);
                    if (vehicle.IsSuccess)
                        return Results.Ok(vehicle.listNotification);

                    return Results.BadRequest(vehicle.listNotification);
                }
                catch
                {
                    return Results.Problem("Erro Interno");
                }
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Administrador" })
            .WithTags("Vehicles");
            #endregion
        });
    }
}