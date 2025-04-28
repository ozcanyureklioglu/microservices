﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MS.Catalog.Api.Features.Courses.Dtos;
using MS.Catalog.Api.Repositories;
using MS.Shared;

namespace MS.Catalog.Api.Features.Courses.GetByUserId
{
    public record GetCourseByUserIdQuery(Guid Id) : IRequestByServiceResult<List<CourseDto>>;


    public class GetCourseByIdQueryHandler(AppDbContext context, IMapper mapper)
        : IRequestHandler<GetCourseByUserIdQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetCourseByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var courses = await context.Courses.Where(x => x.UserId == request.Id)
                .ToListAsync(cancellationToken: cancellationToken);

            var categories = await context.Categories.ToListAsync(cancellationToken: cancellationToken);


            foreach (var course in courses)
            {
                course.Category = categories.First(x => x.Id == course.CategoryId);
            }

            var coursesAsDto = mapper.Map<List<CourseDto>>(courses);
            return ServiceResult<List<CourseDto>>.SuccessAsOk(coursesAsDto);
        }
    }

    public static class GetCourseByUserIdEndpoint
    {
        public static RouteGroupBuilder GetByUserIdCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{userId:guid}",
                    async (IMediator mediator, Guid userId) =>
                        (await mediator.Send(new GetCourseByUserIdQuery(userId))).ToResult())
                .WithName("GetByUserIdCourses");

            return group;
        }
    }
}
