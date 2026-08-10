namespace AyaBeauty.API.Models
{
    public class HomeContent
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string ImageFileName { get; set; } = string.Empty;
        public string ButtonPrimary { get; set; } = "Book Your Escape";
        public string ButtonSecondary { get; set; } = "Explore Services";
        public bool IsActive { get; set; } = true;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}