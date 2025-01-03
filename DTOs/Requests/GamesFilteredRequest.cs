using DR_Tic_Tac_Toe.Enums;

namespace DR_Tic_Tac_Toe.DTOs.Requests
{
    public record GamesFilteredRequest
    {
        public int? PlayerId { get; set; }
        public DateTime? BeforeDate { get; set; }
        public DateTime? AfterDate { get; set; }
        public GameStatus? Status { get; set; }
        public int Size { get; set; }
        public int Page { get; set; }
    }
}
