using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Steam.Models.SteamCommunity;
using Steam.Models.SteamPlayer;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;

namespace INFOM_FINAL_MP
{
    public static class Steam
    {
        private static SteamWebInterfaceFactory webInterfaceFactory;
        private static SteamUser userInterface;
        private static SteamUserStats userStatsInterface;

        public static void InitializeApi(string devApiKey)
        {
            webInterfaceFactory = new SteamWebInterfaceFactory(devApiKey);
            userInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUser>(new HttpClient());
            userStatsInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUserStats>(new HttpClient());
        }

        public static async Task<PlayerSummaryModel> GetSteamUserStats(string steamId)
        {
            if (ulong.TryParse(steamId, out ulong id))
            {
                return (await userInterface.GetPlayerSummaryAsync(id))?.Data;
            }

            return null;
        }

        public static async Task<UserStatsForGameResultModel> GetSteamCsgoStats(string steamId)
        {
            if (ulong.TryParse(steamId, out ulong id))
            {
                return (await userStatsInterface.GetUserStatsForGameAsync(id, 730))?.Data;
            }

            return null;
        }


        public static async Task<Player> GetPlayer(string steamId)
        {
            var userStats = await GetSteamUserStats(steamId);

            if (userStats == null)
                return null;

            var csgoStats = await GetSteamCsgoStats(steamId);
            var csgoStatsDict = csgoStats.Stats.ToDictionary(x => x.Name, x => x.Value);
            var csgoAchievesDict = csgoStats.Achievements.ToDictionary(x => x.Name, x => x.Achieved);

            int totalMatchesPlayed = (int) csgoStatsDict["total_matches_played"];
            int totalKills = (int)csgoStatsDict["total_kills"];
            int totalDeaths = (int)csgoStatsDict["total_deaths"];
            int totalMvps = (int)csgoStatsDict["total_mvps"];
            float kd = (float) totalKills / (float) totalDeaths;
            int totalWins = (int)csgoStatsDict["total_matches_won"];
            int totalLoses = totalMatchesPlayed - totalWins;
            float winRatio = (float)totalWins / (float)totalMatchesPlayed;
            int totalShots = (int)csgoStatsDict["total_shots_fired"];
            int totalHits = (int)csgoStatsDict["total_shots_hit"];
            float accuracy = (float) totalHits / (float) totalShots;
            int totalKillsFamas = (int)csgoStatsDict["total_kills_famas"];
            int totalKillsAk47 = (int)csgoStatsDict["total_kills_ak47"];
            int totalKillsP90 = (int)csgoStatsDict["total_kills_p90"];
            int totalShotsFamas = (int)csgoStatsDict["total_shots_famas"];
            int totalShotsAk47 = (int)csgoStatsDict["total_shots_ak47"];
            int totalShotsP90 = (int)csgoStatsDict["total_shots_p90"];
            int totalHitsFamas = (int)csgoStatsDict["total_hits_famas"];
            int totalHitsAk47 = (int)csgoStatsDict["total_hits_ak47"];
            int totalHitsP90 = (int)csgoStatsDict["total_hits_p90"];
            int totalRoundsDust2 = (int)csgoStatsDict["total_rounds_map_de_dust2"];
            int totalRoundsTrain = (int)csgoStatsDict["total_rounds_map_de_train"];
            int totalRoundsInferno = (int)csgoStatsDict["total_rounds_map_de_inferno"];
            int totalWinsDust2 = (int)csgoStatsDict["total_wins_map_de_dust2"];
            int totalWinsTrain = (int)csgoStatsDict["total_wins_map_de_train"];
            int totalWinsInferno = (int)csgoStatsDict["total_wins_map_de_inferno"];

            return new Player(
                steamId,
                userStats.Nickname,
                totalMatchesPlayed,
                totalKills,
                totalDeaths,
                totalMvps,
                kd,
                totalWins,
                totalLoses,
                winRatio,
                totalShots,
                totalHits,
                accuracy,
                totalKillsFamas,
                totalKillsAk47,
                totalKillsP90,
                totalShotsFamas,
                totalShotsAk47,
                totalShotsP90,
                totalHitsFamas,
                totalHitsAk47,
                totalHitsP90,
                totalRoundsDust2,
                totalRoundsTrain,
                totalRoundsInferno,
                totalWinsDust2,
                totalWinsTrain,
                totalWinsInferno,
                csgoAchievesDict["KILL_WITH_OWN_GUN"] == 1,
                csgoAchievesDict["RESCUE_ALL_HOSTAGES"] == 1,
                csgoAchievesDict["KILL_TWO_WITH_ONE_SHOT"] == 1);
        }

        public static async void LogAllCsgoStats(string steamId)
        {
            var csgoStats = await GetSteamCsgoStats(steamId);
            var csgoStatsDict = csgoStats.Stats.ToDictionary(x => x.Name, x => x.Value);

            foreach (var stat in csgoStatsDict)
            {
                Console.WriteLine($"{stat.Key}: {stat.Value}");
            }
        }
    }
}
