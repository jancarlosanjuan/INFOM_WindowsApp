namespace INFOM_FINAL_MP
{
    public class User
    {
        public string Id { get; }
        public string SteamName { get; }

        public int TotalMatches { get; } 
        public int TotalKills { get; }
        public int TotalDeaths { get; }
        public int TotalMvps { get; }
        public float KdRatio { get; }
        public int TotalWins { get; }
        public int TotalLosses { get; }
        public float WinRate { get; }

        public User(string id, string steamName, int totalMatches, int totalKills, int totalDeaths, int totalMvps, float kdRatio, int totalWins, int totalLosses, float winRate)
        {
            Id = id;
            SteamName = steamName;
            TotalMatches = totalMatches;
            TotalKills = totalKills;
            TotalDeaths = totalDeaths;
            TotalMvps = totalMvps;
            KdRatio = kdRatio;
            TotalWins = totalWins;
            TotalLosses = totalLosses;
            WinRate = winRate;
        }
    }
}
