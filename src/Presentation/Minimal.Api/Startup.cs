using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minimal.Api.Extensions;
using Minimal.Application.Interfaces.Services;
using Minimal.Application.ModelViews;
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
            .ConfigureContext(Configuration)
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

            //#region Administratores
            //string GerarTokenJwt(Administrator administrator)
            //{
            //    if (string.IsNullOrEmpty(key)) return string.Empty;

            //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //    var claims = new List<Claim>()
            //    {
            //        new Claim("Email", administrator.Email),
            //        new Claim("Perfil", administrator.Perfil),
            //        new Claim(ClaimTypes.Role, administrator.Perfil),
            //    };

            //    var token = new JwtSecurityToken(
            //        claims: claims,
            //        expires: DateTime.Now.AddDays(1),
            //        signingCredentials: credentials
            //    );

            //    return new JwtSecurityTokenHandler().WriteToken(token);
            //}

            //endpoints.MapPost("/administratores/login", ([FromBody] LoginDTO loginDTO, IAdministratorServico administratorServico) =>
            //{
            //    var adm = administratorServico.Login(loginDTO);
            //    if (adm != null)
            //    {
            //        string token = GerarTokenJwt(adm);
            //        return Results.Ok(new AdministratorLogado
            //        {
            //            Email = adm.Email,
            //            Perfil = adm.Perfil,
            //            Token = token
            //        });
            //    }
            //    else
            //        return Results.Unauthorized();
            //}).AllowAnonymous().WithTags("Administratores");

            //endpoints.MapGet("/administratores", ([FromQuery] int? pagina, IAdministratorServico administratorServico) =>
            //{
            //    var adms = new List<AdministratorModelView>();
            //    var administratores = administratorServico.Todos(pagina);
            //    foreach (var adm in administratores)
            //    {
            //        adms.Add(new AdministratorModelView
            //        {
            //            Id = adm.Id,
            //            Email = adm.Email,
            //            Perfil = adm.Perfil
            //        });
            //    }
            //    return Results.Ok(adms);
            //})
            //.RequireAuthorization()
            //.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
            //.WithTags("Administratores");

            //endpoints.MapGet("/Administratores/{id}", ([FromRoute] int id, IAdministratorServico administratorServico) =>
            //{
            //    var administrator = administratorServico.BuscaPorId(id);
            //    if (administrator == null) return Results.NotFound();
            //    return Results.Ok(new AdministratorModelView
            //    {
            //        Id = administrator.Id,
            //        Email = administrator.Email,
            //        Perfil = administrator.Perfil
            //    });
            //})
            //.RequireAuthorization()
            //.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
            //.WithTags("Administratores");

            //endpoints.MapPost("/administratores", ([FromBody] AdministratorDTO administratorDTO, IAdministratorServico administratorServico) =>
            //{
            //    var validacao = new ErrosDeValidacao
            //    {
            //        Mensagens = new List<string>()
            //    };

            //    if (string.IsNullOrEmpty(administratorDTO.Email))
            //        validacao.Mensagens.Add("Email não pode ser vazio");
            //    if (string.IsNullOrEmpty(administratorDTO.Senha))
            //        validacao.Mensagens.Add("Senha não pode ser vazia");
            //    if (administratorDTO.Perfil == null)
            //        validacao.Mensagens.Add("Perfil não pode ser vazio");

            //    if (validacao.Mensagens.Count > 0)
            //        return Results.BadRequest(validacao);

            //    var administrator = new Administrator
            //    {
            //        Email = administratorDTO.Email,
            //        Senha = administratorDTO.Senha,
            //        Perfil = administratorDTO.Perfil.ToString() ?? Perfil.Editor.ToString()
            //    };

            //    administratorServico.Incluir(administrator);

            //    return Results.Created($"/administrator/{administrator.Id}", new AdministratorModelView
            //    {
            //        Id = administrator.Id,
            //        Email = administrator.Email,
            //        Perfil = administrator.Perfil
            //    });

            //})
            //.RequireAuthorization()
            //.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
            //.WithTags("Administratores");
            //#endregion

            #region Vehicles
            endpoints.MapPost("/Vehicles/Create", async ([FromBody] InputCreateVehicle inputCreateVehicle, IVehicleService vehicleService) =>
            {
                var vehicle = await vehicleService.Create(inputCreateVehicle);

                if (vehicle.IsSuccess)
                    return Results.Created($"/api/vehicles/{vehicle.Value!.Id}", vehicle.Value);

                return Results.BadRequest(vehicle.listNotification);
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
            .WithTags("Vehicles");

            endpoints.MapPost("/Vehicles/GetAllPagination", ([FromBody] InputPaginationVehicle inputPaginationVehicle, IVehicleService vehicleService) =>
            {
                var vehicles = vehicleService.GetAllWithPagination(inputPaginationVehicle);

                return Results.Ok(vehicles);
            }).RequireAuthorization().WithTags("Vehicles");

            endpoints.MapGet("/Vehicles/GetAll", async (IVehicleService vehicleService) =>
            {
                var vehicles = await vehicleService.GetAllAsync();

                return Results.Ok(vehicles);
            }).RequireAuthorization().WithTags("Vehicles");

            endpoints.MapPost("/Vehicles/GetId", async ([FromBody] InputIdentityViewVehicle inputIdentityViewVehicle, IVehicleService vehicleService) =>
            {
                var vehicle = await vehicleService.GetByIdAsync(inputIdentityViewVehicle);
                if (vehicle == null) return Results.NotFound();
                return Results.Ok(vehicle);
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
            .WithTags("Vehicles");

            endpoints.MapPut("/Vehicles/Update", async ([FromBody] InputIdentityUpdateVehicle inputIdentityUpdateVehicle, IVehicleService vehicleService) =>
            {
                var vehicle = await vehicleService.Update(inputIdentityUpdateVehicle);

                if (vehicle.IsSuccess)
                    return Results.Ok(vehicle.listNotification);

                return Results.BadRequest(vehicle.listNotification);
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
            .WithTags("Vehicles");

            endpoints.MapDelete("/Vehicles/Delete", async ([FromBody] InputIdentityDeleteVehicle inputIdentityDeleteVehicle, IVehicleService vehicleService) =>
            {
                var vehicle = await vehicleService.Delete(inputIdentityDeleteVehicle);
                if (vehicle.IsSuccess)
                    return Results.Ok(vehicle.listNotification);

                return Results.BadRequest(vehicle.listNotification);
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
            .WithTags("Vehicles");
            #endregion
        });
    }
}