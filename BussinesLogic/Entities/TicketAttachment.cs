using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Entities
{
    public class TicketAttachment
    {
        public int Id { get; set; }
        public TicketComment TicketComment { get; set; }
        public int TicketCommentId { get; set; }    
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public int UploadedByUserId { get; set; }
        public DateTime UploadedAt { get; set; }


        private TicketAttachment() { }

        public TicketAttachment(int ticketCommentId,string fileName,string filePath,long fileSize,string contentType,int user)
        {
            TicketCommentId = ticketCommentId;
            FileName = fileName;
            FilePath = filePath;
            FileSize = fileSize;
            ContentType = contentType;
            UploadedAt = DateTime.UtcNow;
            UploadedByUserId = user;
        }


    }
}
