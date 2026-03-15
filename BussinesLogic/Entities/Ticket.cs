using BussinesLogic.Enums;
namespace BussinesLogic.Entities
{
    public class Ticket:TicketSoftDelete
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description{ get; set; }
        public TicketState State { get; set; }
        public TicketPriority Priority { get; set; }
        public int CreatorUserId { get; set; }
        public User CreatorUser { get; set; }
        public int? AssignedUserId { get; set; } 
        public User? AssignedUser { get; set; }
        public int? ResolvedById { get; set; }
        public User? ResolvedBy { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime ResolvedAt { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public DateTime SlaDueDate { get; set; }
        public bool IsSlaBreached { get; set; }
        public int? ClosedBy { get; set; }
        public byte[] RowVersion { get; private set; }  
        public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
        public ICollection<TicketHistory> History { get; set; } = new List<TicketHistory>();
        public ICollection<TicketAttachment> Attachments { get; set; } = new List<TicketAttachment>();


        //Constructors
        protected Ticket() { }

        public Ticket(string title, string description, TicketPriority priority, int creatorUserId, DateTime slaDueDate)
        {
            Title = string.Empty;
            Description = string.Empty;
            State = TicketState.Open;
            Priority = priority;
            CreatorUserId = creatorUserId;
            CreationDate = DateTime.Now;
            SlaDueDate = slaDueDate;
        }

        private void AddHistory(int userId, string action,string oldValue,string newValue)
        {
            TicketHistory th = new TicketHistory(Id,userId,action,oldValue,newValue);
            History.Add(th);
        }
        public void Close(int userId)
        {
            if (State == TicketState.Close) throw new Exception("A closed ticket cannot be modified");
            if (userId <= 0) throw new Exception("User not valid");
            string oldValue = State.ToString();
            State = TicketState.Close;
            ClosingDate = DateTime.Now;
            ClosedBy = userId;
            AddHistory(userId,"Close",oldValue,State.ToString());
        }
        public void Assigned(int assignedByUserId)
        {
            if (State == TicketState.Close)
                throw new Exception("Closed ticket cannot be modified.");
            AssignedUserId = assignedByUserId;
            AssignedDate = DateTime.UtcNow;
            string oldValue = State.ToString();
            State = TicketState.Assigned;
            AddHistory(assignedByUserId, "Assigned", oldValue, State.ToString());
        }
        public void InProgress()
        {
            if (State == TicketState.Close)
                throw new Exception("Closed ticket cannot be modified.");

            if (State != TicketState.Assigned)
                throw new Exception("Only assigned tickets can start progress.");
            string oldValue = State.ToString();
            State = TicketState.InProcess;
        }

        public void Waiting(string comment)
        {
            if (State == TicketState.Close)
                throw new Exception("Closed ticket cannot be modified.");

            if (State != TicketState.InProcess)
                throw new Exception("Only tickets in process can be set to waiting.");
            Comments.Add(new TicketComment(Id,comment,true));
            State = TicketState.Waiting;
        }

        public void Resolve(int resolvedByUserId)
        {
            if (State == TicketState.Close)
                throw new Exception("Closed ticket cannot be modified.");

            if (State != TicketState.InProcess && State != TicketState.Waiting)
                throw new Exception("Only active tickets can be resolved.");
            ResolvedById = resolvedByUserId;
            string oldValue = State.ToString();
            State = TicketState.Resolved;
            IsSlaBreached = ResolvedAt > SlaDueDate;
            AddHistory(resolvedByUserId, "Resolved", oldValue, State.ToString());
        }
        public void Reopen(int userId)
        {
            if (State != TicketState.Close)
                throw new Exception("Only closed tickets can be reopened.");
            string oldValue = State.ToString();
            State = TicketState.Open;
            ClosedBy = null;
            ClosingDate = null;
            AddHistory(userId, "Reopen", oldValue, State.ToString());
        }
        public void AddComment(string content,bool isInternal)
        {
            if (State == TicketState.Close) throw new Exception("Ticket already closed");
            TicketComment comment = new TicketComment(Id,content,isInternal);
            Comments.Add(comment);
        }

        public void ChangePriority(TicketPriority newPriority, int userId)
        {
            if (Priority == newPriority)
                return;
            if (State == TicketState.Close)
                throw new Exception("Ticket already closed");

            string oldValue = Priority.ToString();
            Priority = newPriority;
            AddHistory(userId, "Close", oldValue, Priority.ToString());
        }

    }
}
