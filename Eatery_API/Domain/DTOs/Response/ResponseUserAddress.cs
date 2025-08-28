namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseUserAddress
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
    }
}
