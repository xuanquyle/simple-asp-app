using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DockerComposeApi
{
    public class DockerCommandContext : DbContext
    {
        public DockerCommandContext(DbContextOptions<DockerCommandContext> options)
        : base(options)
        {
        }

        public DbSet<DockerCommand> DockerCommand { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DockerCommand>().ToTable(nameof(DockerCommand));
            base.OnModelCreating(modelBuilder);
        }
    }

    public class DockerCommand
    {
        [Key]
        public int Id { get; set; }
        public required string Command { get; set; }
        public string? Description { get; set; }
        public string? Example { get; set; }
    }

    public class DockerCommandVO
    {
        public required string Command { get; set; }
        public string? Description { get; set; }
        public string? Example { get; set; }
    }
}
