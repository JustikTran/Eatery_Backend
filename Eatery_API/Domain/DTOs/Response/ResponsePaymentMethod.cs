namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponsePaymentMethod
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? MethodCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
