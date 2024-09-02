namespace WebApiFormatter.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? SeriaNo { get; set; }
        public int Age { get; set; }

        public decimal? Score { get; set; }
    }
}
