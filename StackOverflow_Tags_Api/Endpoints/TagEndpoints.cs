using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StackOverflow_Tags_Api.Data;
using StackOverflow_Tags_Api.Models;
using StackOverflow_Tags_Api.Services;

namespace StackOverflow_Tags_Api.controlers;

public static class TagEndpoints
{
    public static void MapTagEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Tag").WithTags(nameof(Tag));

        group.MapGet("/", async (StackOverflow_Tags_ApiContext db) =>
        {
            if (!db.Tag.Any())
            {
                var tags = await ApiService.GetTags();
                db.AddRange(tags);
                db.SaveChanges();
            }
            return await db.Tag.ToListAsync();
        })
        .WithName("GetAllTags")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Tag>, NotFound>> (int id, StackOverflow_Tags_ApiContext db) =>
        {
            return await db.Tag.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Tag model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetTagById")
        .WithOpenApi();
    }
}