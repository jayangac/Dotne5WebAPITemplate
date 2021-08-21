using AutoMapper;
using Dotne5WebAPITemplate.API.Helpers;
using Dotne5WebAPITemplate.Models.Entities.Base;
using Dotne5WebAPITemplate.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Dotne5WebAPITemplate.API.Helpers.RestResponse;

namespace Dotne5WebAPITemplate.API.Controllers.Base
{
    public class BaseController<T, TDto> : ControllerBase where T : IEntityBase, new() where TDto : class
    {
        protected readonly IBaseService<T> _business;
        private readonly IMapper _mapper;
        public BaseController(IBaseService<T> business, IMapper mapper)
        {
            _business = business;
            _mapper = mapper;
        }



        /// <summary>
        /// Gets list of entities
        /// </summary>
        /// <returns>entities</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public virtual async Task<RestResponse> Get()
        {
            var collection = await this._business.GetAsync();
            return ResponseModel(HttpStatusCode.OK, true, Message.Success.ToString(), _mapper.Map<List<TDto>>(collection.ToList()));
        }

        /// <summary>
        /// create entity
        /// </summary>
        /// <param name="entityDto">entity to create</param>
        /// <returns>entity created</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public virtual async Task<RestResponse> Post([FromBody] TDto entityDto)
        {
            try
            {
                if (entityDto != null)
                {
                    var entity = _mapper.Map<T>(entityDto);
                    var resultRaw = this._business.Create(entity);
                    var isSaved = await this._business.SaveChangesAsync();
                    if (isSaved)
                    {
                        return ResponseModel(HttpStatusCode.OK, true, Message.Success.ToString(), _mapper.Map<TDto>(resultRaw));
                    }
                }
                return ResponseModel(HttpStatusCode.BadRequest, false, Message.Error.ToString(), null);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id">id to entity</param>
        /// <param name="entityDto">entity to update</param>
        /// <returns>status code</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPut("{id}")]
        public virtual async Task<RestResponse> Put(int id, [FromBody] TDto entityDto)
        {
            try
            {
                var entity = _mapper.Map<T>(entityDto);
                this._business.Update(id, entity);
                var isSaved = await this._business.SaveChangesAsync();
                if (isSaved)
                    return ResponseModel(HttpStatusCode.OK, true, Message.Success.ToString(), _mapper.Map<TDto>(entity));

                return ResponseModel(HttpStatusCode.BadRequest, false, Message.Error.ToString(), null);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Remove by id
        /// </summary>
        /// <param name="id">id to remove</param>
        /// <returns>status code</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public virtual async Task<RestResponse> Delete(int id)
        {
            try
            {
                _business.Remove(id);
                var isSaved = await _business.SaveChangesAsync();
                if (isSaved)
                {
                    return ResponseModel(HttpStatusCode.OK, true, Message.Success.ToString(), "Ok");
                }
                return ResponseModel(HttpStatusCode.BadRequest, false, Message.Error.ToString(), null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// return response model
        /// </summary>
        /// <param name="status"></param>
        /// <param name="isSuccess"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual RestResponse ResponseModel(HttpStatusCode status, bool isSuccess, string message, object data)
        {
            return new RestResponse(status, isSuccess, message, data);
        }

    }
}
