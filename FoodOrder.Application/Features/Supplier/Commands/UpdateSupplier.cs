using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoodOrder.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodOrder.Shared.Dtos;
using FoodOrder.Shared.ResponseModel;
using FoodOrder.Shared.CustomException;

namespace FoodOrder.Application.Features.Supplier.Commands
{

    public class UpdateSupplierCommand : IRequest<ServiceResponse<SupplierDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string WebUrl { get; set; }
        public bool IsActive { get; set; }


        public class UpdateSupplierCommandHandle : IRequestHandler<UpdateSupplierCommand, ServiceResponse<SupplierDto>>
        {
            private readonly IMapper _mapper;
            private readonly IGenericRepository<Domain.Entities.Supplier> _supplierRepository;
            public UpdateSupplierCommandHandle(IMapper mapper, IGenericRepository<Domain.Entities.Supplier> supplierRepository)
            {
                _mapper = mapper;
                _supplierRepository = supplierRepository;
            }

            public async Task<ServiceResponse<SupplierDto>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
            {

                var dbSupplier = await _supplierRepository.GetByIdAsync(request.Id);
                if (dbSupplier is null)
                    throw new ApiException("Supplier already exists");

                dbSupplier.IsActive = request.IsActive;
                dbSupplier.Name = request.Name;
                dbSupplier.WebUrl = request.WebUrl;

                //_mapper.Map(request, dbSupplier);
                await _supplierRepository.UpdateAsync(dbSupplier);


                return new ServiceResponse<SupplierDto>() { Value = _mapper.Map<SupplierDto>(dbSupplier) };
            }
        }
    }
}