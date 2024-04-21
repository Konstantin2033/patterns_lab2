using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Classes
{
    public class CheckPoint
    {
        private long parcelId;
        private string description;
        private DateTime time;

        public CheckPoint(long parcelId, string description, DateTime time)
        {
            this.parcelId = parcelId;
            this.description = description;
            this.time = time;
        }

        public long ParcelId { get => parcelId; set => parcelId = value; }
        public string Description { get => description; set => description = value; }
        public DateTime Time { get => time; set => time = value; }
    }
}
