using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StudentDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using var scope = app.Services.CreateScope();
scope.ServiceProvider.GetRequiredService<StudentDbContext>().Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/students", async (StudentDbContext db) => await db.Students.ToListAsync());
app.MapGet("/students/{rn:int}", async (int rn, StudentDbContext db) =>
    await db.Students.FindAsync(rn) is { } s ? Results.Ok(s) : Results.NotFound());
app.MapPost("/students", async (Student s, StudentDbContext db) =>
{
    db.Students.Add(s);
    await db.SaveChangesAsync();
    return Results.Created($"/students/{s.Rn}", s);
});
app.MapPut("/students/{rn:int}", async (int rn, Student updated, StudentDbContext db) =>
{
    var s = await db.Students.FindAsync(rn);
    if (s is null) return Results.NotFound();
    s.Name = updated.Name; s.Batch = updated.Batch; s.Marks = updated.Marks;
    await db.SaveChangesAsync();
    return Results.Ok(s);
});
app.MapDelete("/students/{rn:int}", async (int rn, StudentDbContext db) =>
{
    var s = await db.Students.FindAsync(rn);
    if (s is null) return Results.NotFound();
    db.Students.Remove(s);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
