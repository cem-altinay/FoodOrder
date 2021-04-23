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
using Shared.Dtos;
using Shared.ResponseModel;

namespace FoodOrder.Application.Features.Supplier.Queries
{
    public class GetSuppliers : IRequest<ServiceResponse<List<SupplierDto>>>
    {
        public class Handler : IRequestHandler<GetSuppliers, ServiceResponse<List<SupplierDto>>>
        {
            private readonly IGenericRepository<Domain.Entities.Supplier> _supplierRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Domain.Entities.Supplier> supplierRepository, IMapper mapper)
            {
                _supplierRepository = supplierRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<List<SupplierDto>>> Handle(GetSuppliers request, CancellationToken cancellationToken)
            {
                try
                {
                    var suppliers = await _supplierRepository.TableNoTracking.Where(r => r.IsActive)
                                                               .ProjectTo<SupplierDto>(_mapper.ConfigurationProvider)
                                                               .OrderBy(ord => ord.CreateDate)
                                                               .ToListAsync(cancellationToken);
                    return new ServiceResponse<List<SupplierDto>>()
                    {
                        Value = suppliers,
                    };
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}