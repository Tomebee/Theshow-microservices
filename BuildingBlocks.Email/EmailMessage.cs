using System.Collections.Generic;

namespace BuildingBlocks.Email
{
    public class EmailMessage
    {
        public IList<EmailAddress> ToAddresses { get; set; } = new List<EmailAddress>();
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
