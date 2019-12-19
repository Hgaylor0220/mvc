using System.Collections.Generic;

namespace mvc.Models
{
    public class Favorite
    {
        public Favorite()
        {
            this.Rivers = new HashSet<FavoriteRiver>();

        }

        public int FavoriteId { get; set; }
        public string FavoriteCategory { get; set; }
        public virtual ICollection<FavoriteRiver> Rivers { get; set; }

    }
}