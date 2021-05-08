namespace ApiGoBarber.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }

        public UserDTO User { get; set; }
    }
}
