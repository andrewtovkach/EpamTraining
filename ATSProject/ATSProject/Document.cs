using System;

namespace ATSProject
{
    public class Document
    {
        public string Number { get; set; }
        public Client Client { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }

        public Document(string number, Client client, DateTime createDate, string description)
        {
            Number = number;
            Client = client;
            CreatedDate = createDate;
            Description = description;
        }

        public override string ToString()
        {
            return string.Format("Document №{0} {1} - {2} ({3})", Number, Client, CreatedDate, Description);
        }
    }
}
