using Microsoft.EntityFrameworkCore;

namespace ImposterAPI.Models;

public class ToDoContext : DbContext
{
    public ToDoContext(DbContextOptions<ToDoContext> options) 
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoItem>().Property(t => t.Id).ValueGeneratedOnAdd();
    }

    public DbSet<ToDoItem> ToDoItems { get; set; } = null!;

}