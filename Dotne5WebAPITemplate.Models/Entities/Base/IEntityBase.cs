using System;
using System.Collections.Generic;
using System.Text;

namespace Dotne5WebAPITemplate.Models.Entities.Base
{
    public interface IEntityBase
    {
        int ID { get; set; }
        bool Status { get; set; }
    }
}
