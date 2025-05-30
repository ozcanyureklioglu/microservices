using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MS.Catalog.Api;
using MS.Catalog.Api.Features.Categories;
using MS.Catalog.Api.Features.Categories.create;
using MS.Catalog.Api.Features.Courses;
using MS.Catalog.Api.Options;
using MS.Catalog.Api.Repositories;
using MS.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbOptions();
builder.Services.AddDbExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));
builder.Services.AddVersioningExt();

var app = builder.Build();
app.AddSeedDataExt();
app.AddCategoryGroupEndpointExt(app.AddVersionSetExt());
app.AddCourseGroupEndpointExt(app.AddVersionSetExt());


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();