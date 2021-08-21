using AutoMapper;
using Dotne5WebAPITemplate.API.Controllers.Base;
using Dotne5WebAPITemplate.Models.DTO;
using Dotne5WebAPITemplate.Models.Entities.Base;
using Dotne5WebAPITemplate.Services.ErrorModule;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Dotne5WebAPITemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : BaseController<Error, ErrorDTO>
    {
        private readonly IErrorService _ErrorService;
        private readonly IMapper _mapper;
        public ErrorController(IErrorService service, IMapper mapper) : base(service, mapper)
        {
            _ErrorService = service;
            _mapper = mapper;
        }

    }
}
