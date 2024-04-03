using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverflow_Tags_Api.Data;
using StackOverflow_Tags_Api.Models;
using StackOverflow_Tags_Api.Services;
using System.Linq.Expressions;

namespace StackOverflow_Tags_Api.controlers;

public static class TagEndpoints
{
    public static void MapTagEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Tag").WithTags(nameof(Tag));

        group.MapGet("/", async (StackOverflow_Tags_ApiContext db, [FromServices] ILogger<Program> logger, int page = 0, int pagesize = 10, string? sortBy = "name", bool descending = false) =>
        {
            try
            {
                if (!db.Tag.Any())
                {
                    var tags = await ApiService.GetTags();
                    db.AddRange(tags);
                    db.SaveChanges();
                }
                decimal sum = db.Tag.Sum(tag => tag.Count);
                Expression<Func<Tag, object>> sortExpression = ChooseSortMethod(sortBy);

                var result = descending ?
                (db.Tag.Skip(page * pagesize).Take(pagesize).OrderByDescending(sortExpression)
                .Select(tag => new
                {
                    tag,
                    percent = (decimal)tag.Count * 100 / sum
                })) :
                db.Tag.Skip(page * pagesize).Take(pagesize).OrderBy(sortExpression)
                .Select(tag => new
                {
                    tag,
                    percent = (decimal)tag.Count * 100 / sum
                });
                return await result.ToListAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return [];
            }
        })
        .WithName("GetTagsPage")
        .WithOpenApi();
    }

    private static Expression<Func<Tag, object>> ChooseSortMethod(string? sortBy)
    {
        return sortBy switch
        {
            "name" => tag => tag.Name,
            "count" => tag => tag.Count,
            "has_synonyms" => tag => tag.HasSynonyms,
            "is_moderator_only" => tag => tag.IsModeratorOnly,
            "is_required" => tag => tag.IsRequired,
            "last_activity_date" => tag => tag.LastActivityDate,
            "user_id" => tag => tag.UserId,
            _ => tag => tag.Name
        };
    }
}