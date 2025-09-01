using Microsoft.EntityFrameworkCore;

public class StudentDbContext : DbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }
    public DbSet<Student> Students => Set<Student>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Student>().HasKey(s => s.Rn);
        mb.Entity<Student>().Property(s => s.Name).HasMaxLength(100).IsRequired();
        mb.Entity<Student>().Property(s => s.Batch).HasMaxLength(20).IsRequired();
    }
}

