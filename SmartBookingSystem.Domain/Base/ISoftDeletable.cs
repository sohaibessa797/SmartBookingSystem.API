using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Base
{
    public interface ISoftDeletable
    {
         bool IsDeleted { get; set; }
    }
}
