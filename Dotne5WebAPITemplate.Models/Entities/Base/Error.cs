using System;

namespace Dotne5WebAPITemplate.Models.Entities.Base
{
    public class Error : IEntityBase
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime DateCreated { get; set; }
        public int ID { get; set; }
        public bool Status { get; set; }
    }
}
