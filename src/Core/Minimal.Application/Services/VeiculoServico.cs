//using Minimal.Application.Entities;
//using Minimal.DTOs;
//using Minimal.Infraestrutura.Db;
//using Minimal.Application.Interfaces;
//using Microsoft.EntitiesFrameworkCore;

//namespace Minimal.Application.Servicos;

//public class VehicleServico : IVehicleServico
//{
//    private readonly DbContexto _contexto;
//    public VehicleServico(DbContexto contexto)
//    {
//        _contexto = contexto;
//    }

//    public void Apagar(Vehicle vehicle)
//    {
//        _contexto.Vehicles.Remove(vehicle);
//        _contexto.SaveChanges();
//    }

//    public void Atualizar(Vehicle vehicle)
//    {
//        _contexto.Vehicles.Update(vehicle);
//        _contexto.SaveChanges();
//    }

//    public Vehicle? BuscaPorId(int id)
//    {
//        return _contexto.Vehicles.Where(v => v.Id == id).FirstOrDefault();
//    }

//    public void Incluir(Vehicle vehicle)
//    {
//        _contexto.Vehicles.Add(vehicle);
//        _contexto.SaveChanges();
//    }

//public List<Vehicle> Todos(int? pagina = 1, string? nome = null, string? marca = null)
//{
//    var query = _contexto.Vehicles.AsQueryable();
//    if (!string.IsNullOrEmpty(nome))
//    {
//        query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome}%"));
//    }

//    int itensPorPagina = 10;

//    if (pagina != null)
//        query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);

//    return query.ToList();
//}
//}