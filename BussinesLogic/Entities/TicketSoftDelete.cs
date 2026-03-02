using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Entities
{
    public abstract class TicketSoftDelete
    {
        public int Id { get; protected set; }

        public bool IsDeleted { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }
        public int? DeletedById { get; protected set; }

        protected TicketSoftDelete() { }
        public void SoftDelete(int userId)
        {
            if (IsDeleted) return;

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedById = userId;
        }

        public void Restore()
        {
            if (!IsDeleted) return;

            IsDeleted = false;
            DeletedAt = null;
            DeletedById = null;
        }
    }
}
