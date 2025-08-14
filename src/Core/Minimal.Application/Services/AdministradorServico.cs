//using Minimal.Domain.Interfaces;
//using Minimal.DTOs;

//namespace Minimal.Application.Servicos;

//public class AdministratorServico : IAdministratorServico
//{
//    private readonly DbContexto _contexto;
//    public AdministratorServico(DbContexto contexto)
//    {
//        _contexto = contexto;
//    }

//    public Administrator? BuscaPorId(int id)
//    {
//        return _contexto.Administratores.Where(v => v.Id == id).FirstOrDefault();
//    }

//    public Administrator Incluir(Administrator administrator)
//    {
//        _contexto.Administratores.Add(administrator);
//        _contexto.SaveChanges();

//        return administrator;
//    }

//    public Administrator? Login(LoginDTO loginDTO)
//    {
//        var adm = _contexto.Administratores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
//        return adm;
//    }

//    public List<Administrator> Todos(int? pagina)
//    {
//        var query = _contexto.Administratores.AsQueryable();

//        int itensPorPagina = 10;

//        if(pagina != null)
//            query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);

//        return query.ToList();
//    }
//}