using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoodOrder.Application.Interfaces.Repositories;
using FoodOrder.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodOrder.Shared.Dtos;
using FoodOrder.Shared.ResponseModel;

namespace FoodOrder.Application.Features.Supplier.Queries
{
    public class GetSupplierById : IRequest<ServiceResponse<SupplierDto>>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetSupplierById, ServiceResponse<SupplierDto>>
        {
            private readonly IGenericRepository<Domain.Entities.Supplier> _supplierRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Domain.Entities.Supplier> supplierRepository, IMapper mapper)
            {
                _supplierRepository = supplierRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<SupplierDto>> Handle(GetSupplierById request, CancellationToken cancellationToken)
            {
              
                    var supplier = await _supplierRepository.GetByIdAsync(request.Id);

                    if (supplier is null)
                        throw new System.Exception("Supplier not faund");

                    var suppliersDto = _mapper.Map<SupplierDto>(supplier);
                    return new ServiceResponse<SupplierDto>()
                    {
                        Value = suppliersDto,
                    };
              
            }
        }
    }
}