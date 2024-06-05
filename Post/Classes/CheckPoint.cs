namespace Post.Classes
{
    public class CheckPoint
    {
        public long ParcelId { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }

        public CheckPoint(long parcelId, string description, DateTime time)
        {
            ParcelId = parcelId;
            Description = description;
            Time = time;
        }
    }
}