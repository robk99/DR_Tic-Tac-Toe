namespace DR_Tic_Tac_Toe.DTOs
{
    public class UserDetailsDto
    {
        public string Username { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon{ get; set; }
        public int GamesLost{ get; set; }
        public float WinPercentage => GamesPlayed > 0 ? (float)GamesWon / GamesPlayed * 100 : 0;
    }
}
