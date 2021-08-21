using Dotne5WebAPITemplate.DAL.Base;
using Dotne5WebAPITemplate.DAL.ErrorModule;
using Dotne5WebAPITemplate.Models.Entities.Base;
using Dotne5WebAPITemplate.Services.Base;

namespace Dotne5WebAPITemplate.Services.ErrorModule
{
    public class ErrorService : BaseService<Error>, IErrorService
    {
        public ErrorService(IErrorRepository repository) : base(repository)
        {
        }
    }
}
