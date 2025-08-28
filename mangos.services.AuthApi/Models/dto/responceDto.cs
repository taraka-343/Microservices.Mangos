namespace mangos.services.AuthApi.Models.dto
{
    public class responceDto
    {
        public object? result { get; set; }
        public bool isSuceed { get; set; } = true;
        public string message { get; set; } = "";
    }
}
