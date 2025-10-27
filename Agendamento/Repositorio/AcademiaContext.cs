using Agendamento.Models;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Repositorio
{
    public class AcademiaContext : DbContext
    {
        public string DbPath { get; }

        public AcademiaContext(DbContextOptions<AcademiaContext> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "academia.db");
        }

        // Configuração para SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite($"Data Source={DbPath}");
            }
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Agendamentos> Agendamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para evitar agendamentos duplicados
            modelBuilder.Entity<Agendamentos>()
                .HasIndex(a => new { a.AlunoId, a.AulaId, a.DataAgendamento })
                .IsUnique();

            // Configuração do enum Plano como string
            modelBuilder.Entity<Aluno>()
                .Property(a => a.Plano)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Configurações de string length
            modelBuilder.Entity<Aluno>()
                .Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Aula>()
                .Property(a => a.TipoAula)
                .HasMaxLength(100)
                .IsRequired();

            // SQLite não suporta precisão para DateTime, então usamos o padrão
            modelBuilder.Entity<Aula>()
                .Property(a => a.DataHora)
                .IsRequired();

            modelBuilder.Entity<Agendamentos>()
                .Property(a => a.DataAgendamento)
                .IsRequired();
               
        }
    }
}