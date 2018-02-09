using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Farfetch.APIHandler.API_Toggler.Contract;
using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.DTO;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Farfetch.Models;
using Farfetch.ServiceFactory;
using Farfetch.Toggler.Service;
using Factory = Farfetch.ServiceFactory.Factory;

namespace Farfetch.APIHandler.API_Toggler.Public
{
    public class TogglerApiPublic : ITogglerApi
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
            _togglerService = factory.GetDbService(AvailableServices.Toggler) as TogglerService;
        }

        /// <inheritdoc />
        public FarfetchMessage<IEnumerable<ToggleListDto>> GetAll()
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");
            IEnumerable<Toggle> toggleList = _togglerService.GetAll();
            IEnumerable<ToggleListDto> toggleDtoList = new List<ToggleListDto>();
            if (toggleList != null && toggleList.Count() != 0)
            {
                toggleDtoList = Mapper.Map<IEnumerable<Toggle>, IEnumerable<ToggleListDto>>(toggleList);
                if (toggleDtoList == null) throw new AutoMapperMappingException("Error mapping types");
            }
            return new FarfetchMessage<IEnumerable<ToggleListDto>>
            {
                Result = toggleDtoList
            };

        }

        /// <inheritdoc />
        FarfetchMessage<IEnumerable<ToggleDto>> ICrudApi<ToggleDto>.GetAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public FarfetchMessage<ToggleDto> Get(Guid id)
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");
            Toggle toggle = _togglerService.GetById(id);
            ToggleDto toggleDto = Mapper.Map<Toggle, ToggleDto>(toggle);
            if (toggleDto == null) throw new AutoMapperMappingException("Error mapping types");

            return new FarfetchMessage<ToggleDto>
            {
                Result = toggleDto
            };
        }

        /// <inheritdoc />
        public FarfetchMessage<bool> GetForService(string toggleName, bool toggleValue, string serviceName, string serviceVersion)
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
            return new FarfetchMessage<bool>
            {
                Result = result,
            };
        }

        /// <inheritdoc />
        public FarfetchMessage<ToggleDto> Insert(ToggleDto toggleDto)
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");

            Toggle toggle = Mapper.Map<ToggleDto, Toggle>(toggleDto);
            if (toggle == null) throw new AutoMapperMappingException("Error mapping types");
            _togglerService.Insert(toggle);
            toggle = _togglerService.GetByExpression(x => x.Value == toggleDto.Value && x.Overrides == toggleDto.Overrides && x.Name == toggleDto.Name);
            toggleDto = Mapper.Map<Toggle, ToggleDto>(toggle);
            if (toggle == null) throw new AutoMapperMappingException("Error mapping types");
            return new FarfetchMessage<ToggleDto>
            {
                Result = toggleDto,
            };
        }

        /// <inheritdoc />
        public FarfetchMessage<ToggleDto> Update(ToggleDto toggleDto)
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");

            Toggle toggle = Mapper.Map<ToggleDto, Toggle>(toggleDto);
            if (toggle == null) throw new AutoMapperMappingException("Error mapping types");
            _togglerService.Update(toggle);
            toggle = _togglerService.GetById(toggle.Id);

            toggleDto = Mapper.Map<Toggle, ToggleDto>(toggle);
            if (toggle == null) throw new AutoMapperMappingException("Error mapping types");
            return new FarfetchMessage<ToggleDto>
            {
                Result = toggleDto,
            };
        }

        /// <inheritdoc />
        public FarfetchMessage<bool> Delete(Guid id)
        {
            if (_togglerService == null) throw new NullReferenceException("Toggler Service hasn't been defined");

            _togglerService.Delete(id);
            return new FarfetchMessage<bool>();
        }
    }
}