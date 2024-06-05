
namespace Post.Classes
{
    public class Parcel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Recipient { get; set; }
        public DateTime SendingTime { get; set; }
        public DateTime? ReceivedTime { get; set; }
        public List<CheckPoint>? CheckPoints { get; set; }
        public bool IsDelivered { get; set; }

        public Parcel(long id, long userId, string number, string name, string description, string recipient,
            DateTime sendingTime, DateTime? receivedTime, List<CheckPoint>? checkPoints, bool isDelivered)
        {
            Id = id;
            UserId = userId;
            Number = number;
            Name = name;
            Description = description;
            Recipient = recipient;
            SendingTime = sendingTime;
            ReceivedTime = receivedTime;
            CheckPoints = checkPoints;
            IsDelivered = isDelivered;
        }
    }
}