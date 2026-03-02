using BussinesLogic.Enums;
using BussinesLogic.Exceptions;

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
        public DateTime AssignedDate { get; set; }
        public int? ResolvedById { get; set; }
        public User? ResolvedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public int? ClosedBy { get; set; }
        public byte[] RowVersion { get; private set; }  
        public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
        public ICollection<TicketHistory> History { get; set; } = new List<TicketHistory>();


        //Constructors
        public Ticket() { }

        public Ticket(string title, string description, TicketPriority priority, int creatorUserId)
        {
            Title = string.Empty;
            Description = string.Empty;
            State = TicketState.Open;
            Priority = priority;
            CreatorUserId = creatorUserId;
            CreationDate = DateTime.Now;
            Validate();
        }

        private void AddHistory(int userId, string action,string oldValue,string newValue)
        {
            TicketHistory th = new TicketHistory(Id,userId,action,oldValue,newValue);
            History.Add(th);
        }
        public void Close(int userId)
        {
            if (State == TicketState.Close) throw new TicketException("Error. A closed ticket cannot be modified");
            if (userId <= 0) throw new UserException("Error. User not valid");
            string oldValue = State.ToString();
            State = TicketState.Close;
            ClosingDate = DateTime.Now;
            ClosedBy = userId;
            AddHistory(userId,"Close",oldValue,State.ToString());
        }
        public void Assigned(int assignedByUserId)
        {
            if (State == TicketState.Close)
                throw new TicketException("Closed ticket cannot be modified.");
            AssignedUserId = assignedByUserId;
            AssignedDate = DateTime.UtcNow;
            string oldValue = State.ToString();
            State = TicketState.Assigned;
            AddHistory(assignedByUserId, "Assigned", oldValue, State.ToString());
        }
        public void InProgress()
        {
            if (State == TicketState.Close)
                throw new TicketException("Closed ticket cannot be modified.");

            if (State != TicketState.Assigned)
                throw new TicketException("Only assigned tickets can start progress.");
            string oldValue = State.ToString();
            State = TicketState.InProcess;
        }

        public void Waiting()
        {
            if (State == TicketState.Close)
                throw new TicketException("Closed ticket cannot be modified.");

            if (State != TicketState.InProcess)
                throw new TicketException("Only tickets in process can be set to waiting.");

            State = TicketState.Waiting;
        }

        public void Resolve(int resolvedByUserId)
        {
            if (State == TicketState.Close)
                throw new TicketException("Closed ticket cannot be modified.");

            if (State != TicketState.InProcess && State != TicketState.Waiting)
                throw new TicketException("Only active tickets can be resolved.");
            ResolvedById = resolvedByUserId;
            string oldValue = State.ToString();
            State = TicketState.Resolved;
            AddHistory(resolvedByUserId, "Resolved", oldValue, State.ToString());
        }
        public void Reopen(int userId)
        {
            if (State != TicketState.Close)
                throw new TicketException("Only closed tickets can be reopened.");
            string oldValue = State.ToString();
            State = TicketState.Open;
            ClosedBy = null;
            ClosingDate = null;
            AddHistory(userId, "Reopen", oldValue, State.ToString());
        }
        public void AddComment(string content,bool isInternal)
        {
            if (State == TicketState.Close) throw new TicketException("Error. Ticket is closed");
            TicketComment comment = new TicketComment(Id,content,isInternal);
            Comments.Add(comment);
        }

        public void ChangePriority(TicketPriority newPriority, int userId)
        {
            if (Priority == newPriority)
                return;
            if (State == TicketState.Close)
                throw new InvalidOperationException("Error. Can not change the priority of a closed ticket");

            string oldValue = Priority.ToString();
            Priority = newPriority;
            AddHistory(userId, "Close", oldValue, Priority.ToString());
        }

        private void Validate()
        {
            if (CreatorUserId <= 0) throw new TicketException("Error. Creator user not valid");
            if (AssignedUserId <= 0) throw new TicketException("Error. Assigned user not valid");
        }
    }
}
