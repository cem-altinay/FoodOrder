using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoodOrder.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodOrder.Shared.Dtos;
using FoodOrder.Shared.ResponseModel;

namespace FoodOrder.Application.Features.Supplier.Commands
{

    public class CreateSupplierCommand : IRequest<ServiceResponse<SupplierDto>>
    {
        public string Name { get; set; }
        public string WebUrl { get; set; }



        public class CreateSupplierCommandHandle : IRequestHandler<CreateSupplierCommand, ServiceResponse<SupplierDto>>
        {
            private readonly IMapper _mapper;
            private readonly IGenericRepository<Domain.Entities.Supplier> _supplierRepository;
            public CreateSupplierCommandHandle(IMapper mapper, IGenericRepository<Domain.Entities.Supplier> supplierRepository)
            {
                _mapper = mapper;
                _supplierRepository = supplierRepository;
            }

            public async Task<ServiceResponse<SupplierDto>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
            {
                var supplier = _mapper.Map<Domain.Entities.Supplier>(request);
                if (supplier is null)
                    throw new System.Exception("Supplier not mapping");

                var dbSupplier = await _supplierRepository.TableNoTracking.FirstOrDefaultAsync(r => r.Name == request.Name, cancellationToken);
                if (dbSupplier != null)
                    throw new System.Exception("Supplier already exists");

                supplier.IsActive = true;

                await _supplierRepository.InsertAsync(supplier);


                return new ServiceResponse<SupplierDto>() { Value = _mapper.Map<SupplierDto>(supplier) };
            }
        }
    }
}