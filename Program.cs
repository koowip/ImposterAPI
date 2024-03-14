using Microsoft.EntityFrameworkCore;
using ImposterAPI.Models;




var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

builder.Services.AddDbContext<ToDoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(x => x
   .AllowAnyMethod()
   .AllowAnyHeader()
   .SetIsOriginAllowed(origin => true) // allow any origin
   .AllowCredentials()); // allow credentials

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/", () => "Landing page");


app.MapGet("/allToDo", async (ToDoContext db) => await db.ToDoItems.ToListAsync());

app.MapPost("/new", async (ToDoContext db, ToDoItem todo) => {
    db.ToDoItems.Add(todo);
    await db.SaveChangesAsync();
});

app.MapDelete("/del/{id}", async (ToDoContext db, long id) => {
    var todo = await db.ToDoItems.FindAsync(id);
    if (todo != null)
    {
        db.Remove(todo);
        await db.SaveChangesAsync();
    }
});

app.Run();
