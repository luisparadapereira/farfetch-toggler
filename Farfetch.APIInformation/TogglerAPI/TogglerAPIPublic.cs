using System;
using System.Collections.Generic;
using AutoMapper;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Farfetch.Models;
using Farfetch.ServiceFactory;
using Farfetch.Toggler.Service;

namespace Farfetch.APIHandler.TogglerAPI
{
    /// <summary>
    /// Method that handles all the calls from the client application and translates them into service language
    /// This class know about two types of models:
    /// - The ones in the client which this class expects to receive and transmit
    /// - The ones in the service which this class will convert from and into client models
    /// </summary>
    public class TogglerApiPublic: ITogglerApi
    {
        /// <summary>
        /// The Toggler service that is defined on the constructor
        /// </summary>
        private readonly TogglerService _togglerService;

        /// <summary>
        /// Default constructor defines the service
        /// </summary>
        public TogglerApiPublic()
        {
            Factory factory = new Factory();
            _togglerService = factory.GetDbService(FactoryService.Toggler) as TogglerService;
        }

        /// <inheritdoc />
        public TogglerMessage<IEnumerable<ToggleDto>> GetAll()
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");
            IEnumerable<Toggle> toggleList = _togglerService.GetAll();
            IEnumerable<ToggleDto> toggleDtoList = Mapper.Map<IEnumerable<Toggle>, IEnumerable<ToggleDto>>(toggleList);
            if (toggleDtoList == null) throw new AutoMapperMappingException("Error mapping types");

            return new TogglerMessage<IEnumerable<ToggleDto>>
            {
                Result = toggleDtoList,
                Message = "Not Implemented Yet"
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
        public TogglerMessage<bool> Insert(ToggleDto toggleDto)
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");

            Toggle toggle = Mapper.Map<ToggleDto, Toggle>(toggleDto);
            if (toggle == null) throw new AutoMapperMappingException("Error mapping types");
            _togglerService.Insert(toggle);
            return new TogglerMessage<bool>();
        }

        /// <inheritdoc />
        public TogglerMessage<bool> Update(ToggleDto toggleDto)
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");

            Toggle toggle = Mapper.Map<ToggleDto, Toggle>(toggleDto);
            if (toggle == null) throw new AutoMapperMappingException("Error mapping types");
            _togglerService.Update(toggle);
            return new TogglerMessage<bool>();
        }

        /// <inheritdoc />
        public TogglerMessage<bool> Delete(ToggleDto toggleDto)
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");

            Toggle toggle = Mapper.Map<ToggleDto, Toggle>(toggleDto);
            if (toggle == null) throw new AutoMapperMappingException("Error mapping types");
            _togglerService.Delete(toggle.Id);
            return new TogglerMessage<bool>();
        }
    }
}