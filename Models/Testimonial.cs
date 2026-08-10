namespace AyaBeauty.API.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string Quote { get; set; } = string.Empty;
        public string ProfileImageFileName { get; set; } = string.Empty;
        public int Stars { get; set; } = 5;
        public int DisplayOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}