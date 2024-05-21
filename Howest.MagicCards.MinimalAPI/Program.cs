using Howest.MagicCards.MinimalAPI.Model;
using Howest.MagicCards.MinimalAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DeckRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Magic The Gathering");

app.MapPost("/deck", async (Deck deck, [FromServices] DeckRepository deckRepository) =>
{
    deckRepository.Add(deck);
    return "Succes";
});

app.MapGet("/deck", async ([FromServices] DeckRepository deckRepository) =>
{
    return deckRepository.GetAll();
});

app.MapPut("/deck/{id:int}", (int id, [FromServices] DeckRepository deckRepository) =>
{
    deckRepository.updateAmount(id);
    return "success";
});

app.MapDelete("/deck", ([FromServices] DeckRepository deckRepository) =>
{
    deckRepository.RemoveAll();
    return "success";
});

app.MapPut("/deck/{id:int}/{fieldName}/{newValue}", (int id, string fieldName, String newValue, [FromServices] DeckRepository deckRepository) =>
{
    deckRepository.UpdateField(id, fieldName, newValue);
    return "success";
});


app.Run();
