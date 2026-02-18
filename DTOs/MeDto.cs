
namespace DTOs
{
    public sealed class MeDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Role { get; set; }
    }
}
