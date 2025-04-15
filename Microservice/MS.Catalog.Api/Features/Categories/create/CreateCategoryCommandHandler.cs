using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MS.Catalog.Api.Repositories;
using MS.Shared;
using System.Net;

namespace MS.Catalog.Api.Features.Categories.create
{
    public class CreateCategoryCommandHandler(AppDbContext context) :
        IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var checkCat = await context.Categories.AnyAsync(x => x.Name == request.name, cancellationToken: cancellationToken);
            if (checkCat) 
            {
                ServiceResult<CreateCategoryResponse>.Error("","",HttpStatusCode.BadRequest);
                
            }

            var cat = new Category
            {
                Name = request.name,
                Id = NewId.NextSequentialGuid()
            };

            await context.Categories.AddAsync(cat,cancellationToken);
            await context.SaveChangesAsync();

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(cat.Id),"<empty>");
        }
    }
}
