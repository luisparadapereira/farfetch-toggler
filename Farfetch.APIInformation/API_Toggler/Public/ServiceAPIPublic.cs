using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Farfetch.APIHandler.API_Toggler.Contract;
using Farfetch.APIHandler.Common.DTO;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Farfetch.Models;
using Farfetch.ServiceFactory;
using Farfetch.Toggler.Service;
using Factory = Farfetch.ServiceFactory.Factory;

namespace Farfetch.APIHandler.API_Toggler.Public
{
    public class ServiceApiPublic : IServiceApi
    {
        /// <summary>
        /// The Application service that is defined on the constructor
        /// </summary>
        private readonly ApplicationService _applicationService;

        /// <summary>
        /// Default constructor defines the service
        /// </summary>
        public ServiceApiPublic(string settingsFilePath)
        {
            Factory factory = new Factory(settingsFilePath);
            _applicationService = factory.GetDbService(AvailableServices.TogglerApplication) as ApplicationService;

        }

        /// <inheritdoc />
        public FarfetchMessage<IEnumerable<ServiceDto>> GetAll()
        {
            if (_applicationService == null) throw new NullReferenceException("Application Service hasn't been defined");
            IEnumerable<Models.Service> serviceList = _applicationService.GetAll();
            IEnumerable<ServiceDto> serviceDtoList = new List<ServiceDto>();
            if (serviceList!=null && serviceList.Count() != 0)
            {
                serviceDtoList = Mapper.Map<IEnumerable<Models.Service>, IEnumerable<ServiceDto>>(serviceList);
                if (serviceDtoList == null) throw new AutoMapperMappingException("Error mapping types");
            }

            return new FarfetchMessage<IEnumerable<ServiceDto>>
            {
                Result = serviceDtoList
            };
        }

        /// <inheritdoc />
        public FarfetchMessage<ServiceDto> Get(Guid id)
        {
            if (_applicationService == null) throw new NullReferenceException("Application Service hasn't been defined");
            Service service = _applicationService.GetById(id);
            if (service == null)
            {
                return new FarfetchMessage<ServiceDto>
                {
                    Result = null
                };
            }
            ServiceDto serviceDto = Mapper.Map<Models.Service, ServiceDto>(service);
            if (serviceDto == null) throw new AutoMapperMappingException("Error mapping types");

            return new FarfetchMessage<ServiceDto>
            {
                Result = serviceDto
            };
        }

        /// <inheritdoc />
        public FarfetchMessage<ServiceDto> Insert(ServiceDto serviceDto)
        {
            if (_applicationService == null) throw new NullReferenceException("Application Service hasn't been defined");

            Service service = Mapper.Map<ServiceDto, Service>(serviceDto);
            if (service == null) throw new AutoMapperMappingException("Error mapping types");
            _applicationService.Insert(service);

            service = _applicationService.GetByExpression(x => x.Name == serviceDto.Name && x.Version == serviceDto.Version && x.ApiKey == serviceDto.ApiKey);

            serviceDto = Mapper.Map<Service, ServiceDto>(service);
            if (service == null) throw new AutoMapperMappingException("Error mapping types");
            return new FarfetchMessage<ServiceDto>
            {
                Result = serviceDto,
            };
        }

        /// <inheritdoc />
        public FarfetchMessage<ServiceDto> Update(ServiceDto serviceDto)
        {
            if (_applicationService == null) throw new NullReferenceException("Application Service hasn't been defined");

            Service service = Mapper.Map<ServiceDto, Service>(serviceDto);
            if (service == null) throw new AutoMapperMappingException("Error mapping types");
            _applicationService.Update(service);

            service = _applicationService.GetByExpression(x => x.Name == serviceDto.Name && x.Version == serviceDto.Version && x.ApiKey == serviceDto.ApiKey);

            serviceDto = Mapper.Map<Service, ServiceDto>(service);
            if (service == null) throw new AutoMapperMappingException("Error mapping types");
            return new FarfetchMessage<ServiceDto>
            {
                Result = serviceDto,
            };
        }

        /// <inheritdoc />
        public FarfetchMessage<bool> Delete(Guid id)
        {
            if (_applicationService == null) throw new NullReferenceException("Application Service hasn't been defined");

            try
            {
                _applicationService.Delete(id);
                return new FarfetchMessage<bool>
                {
                    Result = true
                };
            }
            catch (Exception)
            {
                return new FarfetchMessage<bool>
                {
                    Result = false
                };
            }
        }
    }
}