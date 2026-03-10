using BussinesLogic.Exceptions;
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

        public int TicketId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public long FileSize { get; set; }

        public string ContentType { get; set; }

        public int UploadedByUserId { get; set; }

        public DateTime UploadedAt { get; set; }

        public Ticket Ticket { get; set; }


        private TicketAttachment() { }

        public TicketAttachment(int ticketId,string fileName,string filePath,long fileSize,string contentType,int user)
        {
            TicketId = ticketId;
            FileName = fileName;
            FilePath = filePath;
            FileSize = fileSize;
            ContentType = contentType;
            UploadedAt = DateTime.UtcNow;
            UploadedByUserId = user;
            Validate();
        }


        private void Validate()
        {
            if (string.IsNullOrEmpty(FileName)) throw new TicketException("Error. File name can not be empty");
            if (string.IsNullOrEmpty(FilePath)) throw new TicketException("Error. File path can not be empty");
            if (string.IsNullOrEmpty(ContentType)) throw new TicketException("Error. Content type can not be empty");
        }

    }
}
