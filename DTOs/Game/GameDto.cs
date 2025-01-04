namespace DR_Tic_Tac_Toe.DTOs.Game
{
    public record GameDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int Player1Id { get; set; }
        public int? Player2Id { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string Winner { get; set; }
        public int Status { get; set; }
    }
}
