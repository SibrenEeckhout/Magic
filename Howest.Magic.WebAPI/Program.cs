using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.Mappings;
using Howest.MagicCards.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1.1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title= "Api without sorting and details",
        Version= "v1.1",
        Description = "API without sorting and detailed cards."
    });
    c.SwaggerDoc("v1.5", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Api with sorting and details",
        Version = "v1.5",
        Description = "API with sorting and detailed cards."
    });
});

builder.Services.AddApiVersioning(o => {
    o.ReportApiVersions = true;
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 1);
    o.ApiVersionReader = ApiVersionReader.Combine(
                                new HeaderApiVersionReader("api-version"));
});

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
        // note: the specified format code will format the version as "'v'major[.minor][-status]"
        options.GroupNameFormat = "'v'VVV";

        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
        // can also be used to control the format of the API version in route templates
        options.SubstituteApiVersionInUrl = true;
    }
);

builder.Services.AddMemoryCache();

builder.Services.AddDbContext<mtg_v1Context>
    (options => options.UseSqlServer(config.GetConnectionString("mtg")));
builder.Services.AddScoped<ICardRepository, SqlCardsRepository>();
builder.Services.AddScoped<IArtistRepository, SqlArtistRepository>();
builder.Services.AddScoped<ITypeRepository, SqlTypeRepository>();
builder.Services.AddScoped<IRarityRepository,   SqlRarityRepository>();

builder.Services.AddAutoMapper(new System.Type[] { typeof(Howest.MagicCards.Shared.Mappings.CardsProfile) });
builder.Services.AddAutoMapper(new System.Type[] { typeof(Howest.MagicCards.Shared.Mappings.ArtistProfile) });
builder.Services.AddAutoMapper(new System.Type[] { typeof(Howest.MagicCards.Shared.Mappings.TypeProfile) });
builder.Services.AddAutoMapper(new System.Type[] { typeof(Howest.MagicCards.Shared.Mappings.RarityProfile) });
builder.Services.AddAutoMapper(new System.Type[] { typeof(Howest.MagicCards.Shared.Mappings.CardsDetailedProfile) });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1.1/swagger.json", "API without sorting and detailed cards.");
        c.SwaggerEndpoint("/swagger/v1.5/swagger.json", "API with sorting and detailed cards.");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
