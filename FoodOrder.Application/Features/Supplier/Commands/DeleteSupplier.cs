using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoodOrder.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.ResponseModel;

namespace FoodOrder.Application.Features.Supplier.Commands
{

    public class DeleteSupplierCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }



        public class DeleteSupplierCommandHandle : IRequestHandler<DeleteSupplierCommand, ServiceResponse<bool>>
        {
            private readonly IMapper _mapper;
            private readonly IGenericRepository<Domain.Entities.Supplier> _supplierRepository;
            public DeleteSupplierCommandHandle(IMapper mapper, IGenericRepository<Domain.Entities.Supplier> supplierRepository)
            {
                _mapper = mapper;
                _supplierRepository = supplierRepository;
            }

            public async Task<ServiceResponse<bool>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
            {


                var dbSupplier = await _supplierRepository.Table.Include(inc => inc.Orders)
                                                          .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
                if (dbSupplier is null)
                    throw new System.Exception("Supplier not found");

                var orderCount = dbSupplier.Orders.Count();
                if (orderCount > 0)
                    throw new Exception($"There are {orderCount} sub order for the order you are trying to delete");

                await _supplierRepository.DeleteAsync(dbSupplier);


                return new ServiceResponse<bool>() { Value = true };
            }
        }
    }
}