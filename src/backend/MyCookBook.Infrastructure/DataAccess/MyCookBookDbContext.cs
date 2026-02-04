using Microsoft.EntityFrameworkCore;
using MyCookBook.Domain.Entities;

namespace MyCookBook.Infrastructure.DataAccess
{
    public class MyCookBookDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração do DbContext
        public MyCookBookDbContext(DbContextOptions options) : base(options) { }

        // Representa a tabela de usuários no banco de dados. Permite realizar operações CRUD na tabela correspondente
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica todas as configurações de entidade definidas no assembly atual
            // Isso permite separar as configurações de entidade em classes distintas
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyCookBookDbContext).Assembly);
        }
    }
}
