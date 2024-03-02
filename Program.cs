using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectef;

var builder = WebApplication.CreateBuilder(args);

//Conexion Para base de datos en memoria
//builder.Services.AddDbContext<TasksContext>(p => p.UseInMemoryDatabase("TasksDB"));
//Conexion para base de datos en Sql Server
builder.Services.AddSqlServer<TasksContext>(builder.Configuration.GetConnectionString("cnTasks"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconnection", async ([FromServices] TasksContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok($"Base de datos en memoria: {dbContext.Database.IsInMemory()}");

});

app.MapGet("/api/tasks", async ([FromServices] TasksContext dbContext) =>
{
    return Results.Ok(dbContext.Tasks.Include(p => p.Category));
    //return Results.Ok(dbContext.Tasks.Include(p => p.Category).Where(p=> p.TaskPriority == projectef.Models.Priority.Low));
});

app.MapPost("/api/tasks", async ([FromServices] TasksContext dbContext, [FromBody] projectef.Models.Task task) =>
{
    task.TaskId = Guid.NewGuid();
    task.CreationDate = DateTime.Now;
    await dbContext.AddAsync(task);
    //await dbContext.Tasks.AddAsync(task);

    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

app.MapPut("/api/tasks/{id}", async ([FromServices] TasksContext dbContext, [FromBody] projectef.Models.Task task, [FromRoute] Guid id) =>
{
    var actualTask = dbContext.Tasks.Find(id);

    if(actualTask != null)
    {
        actualTask.CategoryId = task.CategoryId;
        actualTask.Title = task.Title;
        actualTask.TaskPriority = task.TaskPriority;
        actualTask.Description = task.Description;

        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }
    
    return Results.NotFound();

});

app.MapDelete("/api/tasks/{id}", async ([FromServices] TasksContext dbContext, [FromRoute] Guid id) =>{

    var actualTask = dbContext.Tasks.Find(id);

    if(actualTask != null)
    {
        dbContext.Remove(actualTask);
        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    return Results.NotFound();
});

app.Run();
