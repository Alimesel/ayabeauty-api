namespace AyaBeauty.API.Models
{
    public class AboutContent
    {
        public int Id { get; set; }
        public string SectionTitle { get; set; } = string.Empty;
        public string PhilosophyTitle { get; set; } = string.Empty;
        public string Paragraph1 { get; set; } = string.Empty;
        public string Paragraph2 { get; set; } = string.Empty;
        public string Paragraph3 { get; set; } = string.Empty;
        public string ImageFileName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}