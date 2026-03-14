using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Entities
{
    public class Audit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Action { get; set; }
        public string TableName { get; set; }
        public string RecordId { get; set; }
        public string? OldValues { get; set; }
        public string NewValues { get; set; }
        public string? ChangedColumns { get; set; }


        protected Audit() { }
        public Audit(DateTime date, int userId,string action, string tableName)
        {
            Date = date;
            UserId = userId;
            Action = action;
            TableName = tableName;
        }


    }
}
