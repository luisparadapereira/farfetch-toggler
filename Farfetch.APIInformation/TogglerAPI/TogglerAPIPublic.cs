using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Farfetch.Models;
using Farfetch.ServiceFactory;
using Farfetch.Toggler.Service;

namespace Farfetch.APIHandler.TogglerAPI
{
    /// <inheritdoc />
    /// <summary>
    /// Method that handles all the calls from the client application and translates them into service language
    /// This class know about two types of models:
    /// - The ones in the client which this class expects to receive and transmit
    /// - The ones in the service which this class will convert from and into client models
    /// </summary>
    public class TogglerApiPublic : ITogglerApi
    {
        /// <summary>
        /// The Toggler service that is defined on the constructor
        /// </summary>
        private readonly TogglerService _togglerService;
        private readonly ApplicationService _applicationService;

        /// <summary>
        /// Default constructor defines the service
        /// </summary>
        public TogglerApiPublic()
        {
            Factory factory = new Factory();
            _togglerService = factory.GetDbService(FactoryService.Toggler) as TogglerService;
            _applicationService = factory.GetDbService(FactoryService.TogglerApplication) as ApplicationService;

        }

        /// <inheritdoc />
        public TogglerMessage<IEnumerable<ToggleListDto>> GetAll()
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");
            IEnumerable<Toggle> toggleList = _togglerService.GetAll();
            IEnumerable<ToggleListDto> toggleDtoList = new List<ToggleListDto>();
            if (toggleList != null && toggleList.Count() != 0)
            {
                toggleDtoList = Mapper.Map<IEnumerable<Toggle>, IEnumerable<ToggleListDto>>(toggleList);
                if (toggleDtoList == null) throw new AutoMapperMappingException("Error mapping types");
            }
            return new TogglerMessage<IEnumerable<ToggleListDto>>
            {
                Result = toggleDtoList
            };

        }

        /// <inheritdoc />
        public TogglerMessage<ToggleDto> Get(Guid id)
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");
            Toggle toggle = _togglerService.GetById(id);
            ToggleDto toggleDto = Mapper.Map<Toggle, ToggleDto>(toggle);
            if (toggleDto == null) throw new AutoMapperMappingException("Error mapping types");

            return new TogglerMessage<ToggleDto>
            {
                Result = toggleDto
            };
        }

        /// <inheritdoc />
        public TogglerMessage<bool> GetForService(string toggleName, bool toggleValue, string serviceName, string serviceVersion)
        {
            if (string.IsNullOrEmpty(toggleName)) throw new ArgumentNullException(nameof(toggleName));
            if (string.IsNullOrEmpty(serviceName)) throw new ArgumentNullException(nameof(serviceName));
            if (string.IsNullOrEmpty(serviceVersion)) throw new ArgumentNullException(nameof(serviceVersion));
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");

            Toggle toggle = new Toggle
            {
                Id = Guid.Empty,
                Name = toggleName,
                Overrides = false,
                Value = toggleValue,
                ServiceList = new List<Service>
                {
                    new Service
                    {
                        Name = serviceName,
                        Version = serviceVersion
                    }
                }
            };

            bool result = _togglerService.ShouldApplicationExecute(toggle);
            return new TogglerMessage<bool>
            {
                Result = result,
            };
        }

        /// <inheritdoc />
        public TogglerMessage<ToggleDto> Insert(ToggleDto toggleDto)
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");

            Toggle toggle = Mapper.Map<ToggleDto, Toggle>(toggleDto);
            if (toggle == null) throw new AutoMapperMappingException("Error mapping types");
            _togglerService.Insert(toggle);
            toggle = _togglerService.GetByExpression(x => x.Value == toggleDto.Value && x.Overrides == toggleDto.Overrides && x.Name == toggleDto.Name);
            toggleDto = Mapper.Map<Toggle, ToggleDto>(toggle);
            if (toggle == null) throw new AutoMapperMappingException("Error mapping types");
            return new TogglerMessage<ToggleDto>
            {
                Result = toggleDto,
            };
        }

        /// <inheritdoc />
        public TogglerMessage<ToggleDto> Update(ToggleDto toggleDto)
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");

            Toggle toggle = Mapper.Map<ToggleDto, Toggle>(toggleDto);
            if (toggle == null) throw new AutoMapperMappingException("Error mapping types");
            _togglerService.Update(toggle);
            toggle = _togglerService.GetById(toggle.Id);

            toggleDto = Mapper.Map<Toggle, ToggleDto>(toggle);
            if (toggle == null) throw new AutoMapperMappingException("Error mapping types");
            return new TogglerMessage<ToggleDto>
            {
                Result = toggleDto,
            };
        }

        /// <inheritdoc />
        public TogglerMessage<bool> Delete(Guid id)
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");

            _togglerService.Delete(id);
            return new TogglerMessage<bool>();
        }

        /// <inheritdoc />
        public TogglerMessage<IEnumerable<ServiceDto>> GetAllServices()
        {
            if (_applicationService == null) throw new NullReferenceException("Application Service hasn't been defined");
            IEnumerable<Models.Service> serviceList = _applicationService.GetAll();
            IEnumerable<ServiceDto> serviceDtoList = new List<ServiceDto>();
            if (serviceList!=null && serviceList.Count() != 0)
            {
                serviceDtoList = Mapper.Map<IEnumerable<Models.Service>, IEnumerable<ServiceDto>>(serviceList);
                if (serviceDtoList == null) throw new AutoMapperMappingException("Error mapping types");
            }

            return new TogglerMessage<IEnumerable<ServiceDto>>
            {
                Result = serviceDtoList
            };
        }

        /// <inheritdoc />
        public TogglerMessage<ServiceDto> GetService(Guid id)
        {
            if (_applicationService == null) throw new NullReferenceException("Application Service hasn't been defined");
            Models.Service service = _applicationService.GetById(id);
            ServiceDto serviceDto = Mapper.Map<Models.Service, ServiceDto>(service);
            if (serviceDto == null) throw new AutoMapperMappingException("Error mapping types");

            return new TogglerMessage<ServiceDto>
            {
                Result = serviceDto
            };
        }

        /// <inheritdoc />
        public TogglerMessage<ServiceDto> InsertService(ServiceDto serviceDto)
        {
            if (_applicationService == null) throw new NullReferenceException("Application Service hasn't been defined");

            Service service = Mapper.Map<ServiceDto, Service>(serviceDto);
            if (service == null) throw new AutoMapperMappingException("Error mapping types");
            _applicationService.Insert(service);
            return new TogglerMessage<ServiceDto>();
        }

        /// <inheritdoc />
        public TogglerMessage<ServiceDto> UpdateService(ServiceDto serviceDto)
        {
            if (_applicationService == null) throw new NullReferenceException("Application Service hasn't been defined");

            Service service = Mapper.Map<ServiceDto, Service>(serviceDto);
            if (service == null) throw new AutoMapperMappingException("Error mapping types");
            _applicationService.Update(service);
            return new TogglerMessage<ServiceDto>();
        }

        /// <inheritdoc />
        public TogglerMessage<bool> DeleteService(Guid id)
        {
            if (_applicationService == null) throw new NullReferenceException("Application Service hasn't been defined");

            _applicationService.Delete(id);
            return new TogglerMessage<bool>();
        }
    }
}