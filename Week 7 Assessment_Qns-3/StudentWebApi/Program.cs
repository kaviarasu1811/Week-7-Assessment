using System.Linq;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

var students = new List<Student>();

app.MapGet("/students", () => Results.Ok(students));

app.MapGet("/students/{rn:int}", (int rn) =>
{
    var s = students.FirstOrDefault(x => x.Rn == rn);
    return s is null ? Results.NotFound() : Results.Ok(s);
});

app.MapPost("/students", (Student s) =>
{
    if (students.Any(x => x.Rn == s.Rn)) return Results.Conflict("RN exists");
    students.Add(s);
    return Results.Created($"/students/{s.Rn}", s);
});

app.MapPut("/students/{rn:int}", (int rn, Student updated) =>
{
    var idx = students.FindIndex(x => x.Rn == rn);
    if (idx == -1) return Results.NotFound();
    students[idx] = new Student(rn, updated.Name, updated.Batch, updated.Marks);
    return Results.Ok(students[idx]);
});

app.MapDelete("/students/{rn:int}", (int rn) =>
{
    var s = students.FirstOrDefault(x => x.Rn == rn);
    if (s is null) return Results.NotFound();
    students.Remove(s);
    return Results.NoContent();
});

app.Run();

record Student(int Rn, string Name, string Batch, int Marks);
