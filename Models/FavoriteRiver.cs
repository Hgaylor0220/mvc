namespace mvc.Models
{
    public class FavoriteRiver
    {
        public int FavoriteRiverId { get; set; }
        public int RiverId { get; set; }
        public int FavoriteId { get; set; }
        public River River { get; set; }
        public Favorite Favorite { get; set; }

    }
}