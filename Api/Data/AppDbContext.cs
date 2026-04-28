using Microsoft.EntityFrameworkCore;
using Api.Models;

//Aqui defino a sessão do DB com DbContext(do próprio framework) pra setar a estrutura da pessoa
namespace Api.Data{
    public class AppDbContext : DbContext {
        public DbSet<Person> Persons { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}