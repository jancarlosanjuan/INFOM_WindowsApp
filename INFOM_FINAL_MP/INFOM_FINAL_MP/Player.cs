namespace INFOM_FINAL_MP
{
    public class Player
    {
        // Steam
        public string Id { get; }
        public string SteamName { get; }

        // Overview
        public int TotalMatches { get; } 
        public int TotalKills { get; }
        public int TotalDeaths { get; }
        public int TotalMvps { get; }
        public float KdRatio { get; }
        public int TotalWins { get; }
        public int TotalLosses { get; }
        public float WinRate { get; }
        public int TotalShots { get; }
        public int TotalHits { get; }
        public float Accuracy { get; }

        // Weapons
        public int TotalKillsFamas { get; }
        public int TotalKillsAk47 { get; }
        public int TotalKillsP90 { get; }
        public int TotalShotsFamas { get; }
        public int TotalShotsAk47 { get; }
        public int TotalShotsP90 { get; }
        public int TotalHitsFamas { get; }
        public int TotalHitsAk47 { get; }
        public int TotalHitsP90 { get; }

        // Maps
        public int TotalRoundsDust2 { get; }
        public int TotalRoundsTrain { get; }
        public int TotalRoundsInferno { get; }
        public int TotalWinsDust2 { get; }
        public int TotalWinsTrain { get; }
        public int TotalWinsInferno { get; }

        // Achievements
        public bool IsAchieved_KILL_WITH_OWN_GUN { get; }
        public bool IsAchieved_RESCUE_ALL_HOSTAGES { get; }
        public bool IsAchieved_KILL_TWO_WITH_ONE_SHOT { get; }

        public Player(string id, string steamName, int totalMatches,int totalKills, int totalDeaths, int totalMvps, float kdRatio, int totalWins, int totalLosses, float winRate, int totalShots, int totalHits, float accuracy)
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
            TotalShots = totalShots;
            TotalHits = totalHits;
            Accuracy = accuracy;
        }

        public Player(string id, string steamName, int totalMatches, int totalKills, int totalDeaths, int totalMvps, float kdRatio, int totalWins, int totalLosses, float winRate, int totalShots, int totalHits, float accuracy, int totalKillsFamas, int totalKillsAk47, int totalKillsP90, int totalShotsFamas, int totalShotsAk47, int totalShotsP90, int totalHitsFamas, int totalHitsAk47, int totalHitsP90, int totalRoundsDust2, int totalRoundsTrain, int totalRoundsInferno, int totalWinsDust2, int totalWinsTrain, int totalWinsInferno, bool isAchievedKillWithOwnGun, bool isAchievedRescueAllHostages, bool isAchievedKillTwoWithOneShot)
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
            TotalShots = totalShots;
            TotalHits = totalHits;
            Accuracy = accuracy;
            TotalKillsFamas = totalKillsFamas;
            TotalKillsAk47 = totalKillsAk47;
            TotalKillsP90 = totalKillsP90;
            TotalShotsFamas = totalShotsFamas;
            TotalShotsAk47 = totalShotsAk47;
            TotalShotsP90 = totalShotsP90;
            TotalHitsFamas = totalHitsFamas;
            TotalHitsAk47 = totalHitsAk47;
            TotalHitsP90 = totalHitsP90;
            TotalRoundsDust2 = totalRoundsDust2;
            TotalRoundsTrain = totalRoundsTrain;
            TotalRoundsInferno = totalRoundsInferno;
            TotalWinsDust2 = totalWinsDust2;
            TotalWinsTrain = totalWinsTrain;
            TotalWinsInferno = totalWinsInferno;
            IsAchieved_KILL_WITH_OWN_GUN = isAchievedKillWithOwnGun;
            IsAchieved_RESCUE_ALL_HOSTAGES = isAchievedRescueAllHostages;
            IsAchieved_KILL_TWO_WITH_ONE_SHOT = isAchievedKillTwoWithOneShot;
        }
    }
}
