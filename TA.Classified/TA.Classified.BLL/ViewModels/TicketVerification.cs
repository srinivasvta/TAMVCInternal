using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Classified.BLL.ViewModels
{
    class TicketVerification
    {
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
        public Nullable<bool> IsExpired { get; set; }
        public Nullable<bool> IsUsed { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
