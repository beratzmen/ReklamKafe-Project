using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win.Entities.Enum
{
    public enum ProcessStatus
    {
        pending= 1,
        completed=2,
        Confirmation = 3,
        GotoCargo = 4,
        DeliverCargo = 5,
        Deliver = 6
    }
}
