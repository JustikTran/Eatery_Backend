namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseUser
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Language { get; set; }
        public string? AccountId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
