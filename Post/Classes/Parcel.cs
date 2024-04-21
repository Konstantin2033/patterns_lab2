using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Classes
{
    public class Parcel
    {
        private long id;
        private long userId;
        private string number;
        private string name;
        private string description;
        private string recipient;
        private DateTime sendingTime;
        private DateTime? receivedTime;
        private List<CheckPoint>? checkPoints;
        private bool isDelivered;

        public Parcel(long id, long userId, string number, string name, string description, string recipient,
            DateTime sendingTime, DateTime? receivedTime, List<CheckPoint>? checkPoints, bool isDelivered)
        {
            this.id = id;
            this.userId = userId;
            this.number = number;
            this.name = name;
            this.description = description;
            this.recipient = recipient;
            this.sendingTime = sendingTime;
            this.receivedTime = receivedTime;
            this.checkPoints = checkPoints;
            this.isDelivered = isDelivered;
        }

        public long Id { get => id; set => id = value; }
        public long UserId { get => userId; set => userId = value; }
        public string Number { get => number; set => number = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Recipient { get => recipient; set => recipient = value; }
        public DateTime SendingTime { get => sendingTime; set => sendingTime = value; }
        public DateTime? ReceivedTime { get => receivedTime; set => receivedTime = value; }
        public List<CheckPoint>? CheckPoints { get => checkPoints; set => checkPoints = value; }
        public bool IsDelivered { get => isDelivered; set => isDelivered = value; }
    }
}
