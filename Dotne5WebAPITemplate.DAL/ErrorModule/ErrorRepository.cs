using Dotne5WebAPITemplate.DAL.Base;
using Dotne5WebAPITemplate.Models.Entities.Base;
using Dotne5WebAPITemplate.Models.Entities.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotne5WebAPITemplate.DAL.ErrorModule
{
    public class ErrorRepository : BaseRepository<Error>, IErrorRepository
    {
        public ErrorRepository(Dotne5WebAPITemplateContext context) : base(context)
        {
        }
    }
}
