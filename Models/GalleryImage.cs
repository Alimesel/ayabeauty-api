namespace AyaBeauty.API.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }
        public string Src { get; set; } = string.Empty;
        public string Alt { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string CategoryLabel { get; set; } = string.Empty;
        public bool Tall { get; set; } = false;
        public int DisplayOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}