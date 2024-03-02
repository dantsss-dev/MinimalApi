using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using projectef.Models;

namespace projectef;

public class TasksContext: DbContext
{
    //Colecciones que representan las tablas en la base de datos utilizando los modelos
    //de Category y Task Respectivamente
    public DbSet<Category> Categories {get;set;}
    public DbSet<Models.Task> Tasks {get;set;}

    public TasksContext(DbContextOptions<TasksContext> options) :base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Category> categoriesInit = [];
        categoriesInit.Add(new Category() { CategoryId = Guid.Parse("16923bc2-aedc-417d-bbe3-fb1bdd16e06d"), Name = "Actividades pendientes", Weight = 20});
        categoriesInit.Add(new Category() { CategoryId = Guid.Parse("16923bc2-aedc-417d-bbe3-fb1bdd16e002"), Name = "Actividades personales", Weight = 50});
        
        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("Category");
            category.HasKey(p => p.CategoryId);

            category.Property(p => p.Name).IsRequired().HasMaxLength(150);

            category.Property(p => p.Description).IsRequired(false);

            category.Property(p => p.Weight);

            category.HasData(categoriesInit);
        });

        List<Models.Task> tasksInit = [];
        tasksInit.Add(new Models.Task() { TaskId = Guid.Parse("16923bc2-aedc-417d-bbe3-fb1bdd16e010"), CategoryId = Guid.Parse("16923bc2-aedc-417d-bbe3-fb1bdd16e06d"), TaskPriority = Priority.Medium, Title = "Pago de servicios publicos", CreationDate = DateTime.Now });
        tasksInit.Add(new Models.Task() { TaskId = Guid.Parse("16923bc2-aedc-417d-bbe3-fb1bdd16e011"), CategoryId = Guid.Parse("16923bc2-aedc-417d-bbe3-fb1bdd16e002"), TaskPriority = Priority.Low, Title = "Terminar de ver peliculas", CreationDate = DateTime.Now });

        modelBuilder.Entity<Models.Task>(task =>
        {
            task.ToTable("Task");
            task.HasKey(p => p.TaskId);

            task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p => p.CategoryId);

            task.Property(p => p.Title).IsRequired().HasMaxLength(200);
            
            task.Property(p=> p.Description).IsRequired(false);

            task.Property(p => p.TaskPriority);

            task.Property(p => p.CreationDate);

            task.Ignore(p => p.Resume);

            task.HasData(tasksInit);
        });
    }
}