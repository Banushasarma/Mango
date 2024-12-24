namespace Mango.Service.AuthAPI.Models.Dto
{
    public class RegistrationRequestDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
