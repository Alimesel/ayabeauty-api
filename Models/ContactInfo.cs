namespace AyaBeauty.API.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string SectionTitle { get; set; } = string.Empty;
        public string SectionDescription { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string Phone1 { get; set; } = string.Empty;
        public string Phone2 { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HoursWeekdays { get; set; } = string.Empty;
        public string HoursWeekdaysTime { get; set; } = string.Empty;
        public string HoursSunday { get; set; } = string.Empty;
        public string InstagramUrl { get; set; } = string.Empty;
        public string FacebookUrl { get; set; } = string.Empty;
        public string WhatsappNumber { get; set; } = string.Empty;
        public string MapLatitude { get; set; } = string.Empty;
        public string MapLongitude { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}