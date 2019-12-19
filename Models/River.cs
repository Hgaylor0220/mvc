
using System.Collections.Generic;

namespace mvc.Models
{
    public class River
    {
        public River()
        {
            this.Favorites = new HashSet<FavoriteRiver>();
        }

        public int RiverId { get; set; }
        public string RiverName { get; set; }
        public string Description { get; set; }
        public string Class { get; set; }
        public string RunLength { get; set; }
        public string PutIn { get; set; }
        public string TakeOut { get; set; }
        public string Location { get; set; }
        public int StationNumber { get; set; }
        public ICollection<FavoriteRiver> Favorites { get; }
        public virtual ApplicationUser User { get; set; }

    }
}