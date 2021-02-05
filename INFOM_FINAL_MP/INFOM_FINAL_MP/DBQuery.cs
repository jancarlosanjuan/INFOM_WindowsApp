using MySql.Data.MySqlClient;
using System.Data;

namespace INFOM_FINAL_MP
{
    public static class DBQuery
    {
        private static MySqlCommand command = null;
        private static DataTable dataTable;
        private static MySqlDataAdapter dataAdapter;

        public static Player GetPlayerFromId(string playerId)
        {
            command = DB.RunQuery("SELECT p.steam_name, po.total_wins + po.total_loses AS total_matches, po.total_kills AS kills, po.total_deaths AS deaths,po.total_mvps AS mvps,IFNULL(FORMAT(po.total_kills / po.total_deaths, 2), 0) AS 'kd',po.total_wins AS wins,po.total_loses AS loses,IFNULL(FORMAT((po.total_wins / (po.total_wins + po.total_loses) * 100), 2), 0) AS win_rate,po.total_shots AS shots_fired,po.total_hits AS shots_hit,IFNULL(FORMAT(po.total_hits / po.total_shots, 2), 0) AS accuracy FROM players p LEFT JOIN player_overview po ON p.player_id = po.player_id LEFT JOIN player_weapon_stats pws ON p.player_id = pws.player_id WHERE p.player_id LIKE @player_id ORDER BY p.player_id LIMIT 1;",
                new MySqlParameter("@player_id", playerId));

            if (command == null)
                return null;

            // Get query results
            dataTable = new DataTable();
            dataAdapter = new MySqlDataAdapter(command);
            dataAdapter.Fill(dataTable);

            if (dataTable.HasErrors)
                return null;

            DataRow playerData = dataTable.Rows[0];

            if (playerData["steam_name"].ToString() == "")
                return null;

            Player player = new Player(
                playerId,
                playerData["steam_name"].ToString(),
                int.Parse(playerData["total_matches"].ToString()),
                int.Parse(playerData["kills"].ToString()),
                int.Parse(playerData["deaths"].ToString()),
                int.Parse(playerData["mvps"].ToString()),
                float.Parse(playerData["kd"].ToString()),
                int.Parse(playerData["wins"].ToString()),
                int.Parse(playerData["loses"].ToString()),
                float.Parse(playerData["win_rate"].ToString()),
                int.Parse(playerData["shots_fired"].ToString()),
                int.Parse(playerData["shots_hits"].ToString()),
                float.Parse(playerData["accuracy"].ToString()));
            return player;
        }

        public static DataTable GetPlayerDataTableFromId(string playerId)
        {
            command = DB.RunQuery("SELECT p.steam_name, po.total_wins + po.total_loses AS total_matches, po.total_kills AS kills, po.total_deaths AS deaths,po.total_mvps AS mvps,IFNULL(FORMAT(po.total_kills / po.total_deaths, 2), 0) AS 'kd',po.total_wins AS wins,po.total_loses AS loses,IFNULL(FORMAT((po.total_wins / (po.total_wins + po.total_loses) * 100), 2), 0) AS win_rate,po.total_shots AS shots_fired,po.total_hits AS shots_hit,IFNULL(FORMAT(po.total_hits / po.total_shots, 2), 0) AS accuracy FROM players p LEFT JOIN player_overview po ON p.player_id = po.player_id LEFT JOIN player_weapon_stats pws ON p.player_id = pws.player_id WHERE p.player_id LIKE @player_id ORDER BY p.player_id LIMIT 1;",
                new MySqlParameter("@player_id", playerId));

            if (command == null)
                return null;

            // Get query results
            dataTable = new DataTable();
            dataAdapter = new MySqlDataAdapter(command);
            dataAdapter.Fill(dataTable);

            if (dataTable.HasErrors)
                return null;

            return dataTable;
        }

        public static bool CreatePlayer(Player player)
        {
            string insertPlayerQuery = "INSERT INTO players(player_id, steam_name) VALUES (@player_id, @steam_name);";
            string insertPlayerOverviewQuery = "INSERT INTO player_overview VALUES (@player_id, @total_kills, @total_deaths, @total_mvps, @total_wins, @total_loses, @total_shots, @total_hits);";
            string insertWeaponQuery = "SELECT @weapon_id:=weapons.weapon_id FROM weapons WHERE @weapon_name = weapons.weapon_name; INSERT INTO player_weapon_stats(weapon_id, player_id, kills, shots_fired, shots_hit) VALUES (@weapon_id, @player_id, @kills, @shots_fired, @shots_hit);";
            string insertMapQuery = "SELECT @map_id:=maps.map_id FROM maps WHERE @map_name = maps.map_name; INSERT INTO player_map_stats(map_id, player_id, rounds, wins) VALUES (@map_id, @player_id, @rounds, @wins);";
            string insertAchievementQuery = "SELECT @achievement_id:=achievements.achievement_id FROM achievements WHERE @achievement_name = achievements.achievement_name; INSERT INTO player_achievement_stats(achievement_id, player_id, is_achieved) VALUES(@achievement_id, @player_id, @is_achieved);";

            // Insert player
            if (DB.RunQuery(insertPlayerQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@steam_name", player.SteamName)) == null)
                return false;

            // Insert player overview
            if (DB.RunQuery(insertPlayerOverviewQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@total_kills", player.TotalKills),
                new MySqlParameter("@total_deaths", player.TotalDeaths),
                new MySqlParameter("@total_mvps", player.TotalMvps),
                new MySqlParameter("@total_wins", player.TotalWins),
                new MySqlParameter("@total_loses", player.TotalLosses),
                new MySqlParameter("@total_shots", player.TotalShots),
                new MySqlParameter("@total_hits", player.TotalHits),
                new MySqlParameter("@steam_name", player.SteamName)) == null)
                return false;

            // Insert Ak47 stats
            if (DB.RunQuery(insertWeaponQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@weapon_name", "Ak47"),
                new MySqlParameter("@kills", player.TotalKillsAk47),
                new MySqlParameter("@shots_fired", player.TotalShotsAk47),
                new MySqlParameter("@shots_hit", player.TotalHitsAk47)) == null)
                return false;
            
            // Insert Famas stats
            if (DB.RunQuery(insertWeaponQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@weapon_name", "Famas"),
                new MySqlParameter("@kills", player.TotalKillsFamas),
                new MySqlParameter("@shots_fired", player.TotalShotsFamas),
                new MySqlParameter("@shots_hit", player.TotalHitsFamas)) == null)
                return false;
            
            // Insert P90 stats
            if (DB.RunQuery(insertWeaponQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@weapon_name", "P90"),
                new MySqlParameter("@kills", player.TotalKillsP90),
                new MySqlParameter("@shots_fired", player.TotalShotsP90),
                new MySqlParameter("@shots_hit", player.TotalHitsP90)) == null)
                return false;

            // Insert Dust2 stats
            if (DB.RunQuery(insertMapQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@map_name", "Dust2"),
                new MySqlParameter("@rounds", player.TotalRoundsDust2),
                new MySqlParameter("@wins", player.TotalWinsDust2)) == null)
                return false;

            // Insert Train stats
            if (DB.RunQuery(insertMapQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@map_name", "Train"),
                new MySqlParameter("@rounds", player.TotalRoundsTrain),
                new MySqlParameter("@wins", player.TotalWinsTrain)) == null)
                return false;

            // Insert Inferno stats
            if (DB.RunQuery(insertMapQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@map_name", "Inferno"),
                new MySqlParameter("@rounds", player.TotalRoundsInferno),
                new MySqlParameter("@wins", player.TotalWinsInferno)) == null)
                return false;

            // Insert KILL_WITH_OWN_GUN achievement
            if (DB.RunQuery(insertAchievementQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@achievement_name", "KILL_WITH_OWN_GUN"),
                new MySqlParameter("@is_achieved", player.IsAchieved_KILL_WITH_OWN_GUN)) == null)
                return false;

            // Insert RESCUE_ALL_HOSTAGES achievement
            if (DB.RunQuery(insertAchievementQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@achievement_name", "RESCUE_ALL_HOSTAGES"),
                new MySqlParameter("@is_achieved", player.IsAchieved_RESCUE_ALL_HOSTAGES)) == null)
                return false;

            // Insert KILL_TWO_WITH_ONE_SHOT achievement
            if (DB.RunQuery(insertAchievementQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@achievement_name", "KILL_TWO_WITH_ONE_SHOT"),
                new MySqlParameter("@is_achieved", player.IsAchieved_KILL_TWO_WITH_ONE_SHOT)) == null)
                return false;

            return true;
        }

        public static bool UpdatePlayer(Player player)
        {
            string overviewQuery = "UPDATE player_overview SET total_kills=@total_kills, total_deaths=@total_deaths, total_mvps=@total_mvps, total_wins=@total_wins, total_loses=@total_loses, total_shots=@total_shots, total_hits=@total_hits WHERE player_id=@player_id AND weapon_id=@weapon_id;";
            string weaponQuery = "SELECT @weapon_id := weapons.weapon_id FROM weapons WHERE @weapon_name = weapons.weapon_name; UPDATE player_weapon_stats SET kills = @kills WHERE player_id = @player_id AND weapon_id = @weapon_id; UPDATE player_weapon_stats SET shots_fired = @shots_fired WHERE player_id = @player_id AND weapon_id = @weapon_id; UPDATE player_weapon_stats SET shots_hit = @shots_hit WHERE player_id = @player_id AND weapon_id = @weapon_id;";
            string mapQuery = "SELECT @map_id:=maps.map_id FROM maps WHERE @map_name = maps.map_name; UPDATE player_map_stats SET rounds = @rounds WHERE player_id = @player_id AND map_id = @map_id; UPDATE player_map_stats SET wins = @wins WHERE player_id = @player_id AND map_id = @map_id; ";
            string achievementQuery = "SELECT @achievement_id:=achievements.achievement_id FROM achievements WHERE @achievement_name = achievements.achievement_name; UPDATE player_achievement_stats SET is_achieved = @is_achieved WHERE player_id = @player_id AND achievement_id = @achievement_id; ";

            if (DB.RunQuery(overviewQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@total_kills", player.TotalKills),
                new MySqlParameter("@total_deaths", player.TotalDeaths),
                new MySqlParameter("@total_mvps", player.TotalMvps),
                new MySqlParameter("@total_wins", player.TotalWins),
                new MySqlParameter("@total_loses", player.TotalLosses),
                new MySqlParameter("@total_shots", player.TotalShots),
                new MySqlParameter("@total_hits", player.TotalHits),
                new MySqlParameter("@steam_name", player.SteamName)) == null)
                return false;

            // Update Ak47 stats
            if (DB.RunQuery(weaponQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@weapon_name", "Ak47"),
                new MySqlParameter("@kills", player.TotalKillsAk47),
                new MySqlParameter("@shots_fired", player.TotalShotsAk47),
                new MySqlParameter("@shots_hit", player.TotalHitsAk47)) == null)
                return false;

            // Update Famas stats
            if (DB.RunQuery(weaponQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@weapon_name", "Famas"),
                new MySqlParameter("@kills", player.TotalKillsFamas),
                new MySqlParameter("@shots_fired", player.TotalShotsFamas),
                new MySqlParameter("@shots_hit", player.TotalHitsFamas)) == null)
                return false;

            // Update P90 stats
            if (DB.RunQuery(weaponQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@weapon_name", "P90"),
                new MySqlParameter("@kills", player.TotalKillsP90),
                new MySqlParameter("@shots_fired", player.TotalShotsP90),
                new MySqlParameter("@shots_hit", player.TotalHitsP90)) == null)
                return false;

            // Update Dust2 stats
            if (DB.RunQuery(mapQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@map_name", "Dust2"),
                new MySqlParameter("@rounds", player.TotalRoundsDust2),
                new MySqlParameter("@wins", player.TotalWinsDust2)) == null)
                return false;

            // Update Train stats
            if (DB.RunQuery(mapQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@map_name", "Train"),
                new MySqlParameter("@rounds", player.TotalRoundsTrain),
                new MySqlParameter("@wins", player.TotalWinsTrain)) == null)
                return false;

            // Update Inferno stats
            if (DB.RunQuery(mapQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@map_name", "Inferno"),
                new MySqlParameter("@rounds", player.TotalRoundsInferno),
                new MySqlParameter("@wins", player.TotalWinsInferno)) == null)
                return false;

            // Update KILL_WITH_OWN_GUN achievement
            if (DB.RunQuery(achievementQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@achievement_name", "KILL_WITH_OWN_GUN"),
                new MySqlParameter("@is_achieved", player.IsAchieved_KILL_WITH_OWN_GUN)) == null)
                return false;

            // Update RESCUE_ALL_HOSTAGES achievement
            if (DB.RunQuery(achievementQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@achievement_name", "RESCUE_ALL_HOSTAGES"),
                new MySqlParameter("@is_achieved", player.IsAchieved_RESCUE_ALL_HOSTAGES)) == null)
                return false;

            // Update KILL_TWO_WITH_ONE_SHOT achievement
            if (DB.RunQuery(achievementQuery,
                new MySqlParameter("@player_id", player.Id),
                new MySqlParameter("@achievement_name", "KILL_TWO_WITH_ONE_SHOT"),
                new MySqlParameter("@is_achieved", player.IsAchieved_KILL_TWO_WITH_ONE_SHOT)) == null)
                return false;

            return true;
        }

        public static bool DeletePlayer(string playerId)
        {
            if (DB.RunQuery("DELETE FROM match_player WHERE @player_id = player_id;DELETE FROM player_achievement_stats WHERE @player_id = player_id;DELETE FROM player_map_stats WHERE @player_id = player_id;DELETE FROM player_weapon_stats WHERE @player_id = player_id;DELETE FROM players WHERE @player_id = player_id; ",
                new MySqlParameter("@player_id", playerId)) == null)
                return false;

            return true;
        }

        public static ClientUser getClientUser(string username)
        {
            command = DB.RunQuery("select * from users where username = @username limit 1;",
                new MySqlParameter("@username", username));
            ClientUser clientuser = null;
            if(command != null)
            {
                dataTable = new DataTable();
                dataAdapter = new MySqlDataAdapter(command);
                dataAdapter.Fill(dataTable);

                foreach(DataRow row in dataTable.Rows)
                {
                    string currentUsername = row["username"].ToString();//change this to username
                    string currentPassword = row["password"].ToString();//change this later
                    string currentisadmin = row["is_admin"].ToString();//change this later
                    clientuser = new ClientUser(currentUsername, currentPassword, currentisadmin);
                }
            }
            return clientuser;
        }

        public static DataTable getUserMatches(string username)
        {


            command = DB.RunQuery("select players.steam_name, team, kills, deaths, assists, score, mvps from match_player left join players on players.player_id = match_player.player_id and players.steam_name = @username;", new MySqlParameter("@username", username));

            ClientUser clientuser = null;
            if (command != null)
            {
                dataTable = new DataTable();
                dataAdapter = new MySqlDataAdapter(command);
                dataAdapter.Fill(dataTable);

                /*foreach (DataRow row in dataTable.Rows)
                {
                    string currentUsername = row["username"].ToString();//change this to username
                    string currentPassword = row["password"].ToString();//change this later
                    string currentisadmin = row["is_admin"].ToString();//change this later
                    clientuser = new ClientUser(currentUsername, currentPassword, currentisadmin);
                }*/
            }
            return dataTable;

        }


        public static DataTable GetPlayerFromId2(string playerId)
        {
            //string query = "select * from match_player left join players on players.player_id = match_player.player_id where players.steamName = (@steamName) limit 1";
            command = DB.RunQuery("SELECT * FROM players WHERE player_id = (@player_id);",
                new MySqlParameter("@player_id", playerId));

            if (command != null)
            {
                dataTable = new DataTable();
                dataAdapter = new MySqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public static DataTable GetPlayerFromName2(string steamName)
        {
            command = DB.RunQuery("SELECT * FROM players WHERE steam_name = (@steam_name) LIMIT 1;",
                new MySqlParameter("@steam_name", steamName));

            if (command != null)
            {
                dataTable = new DataTable();
                dataAdapter = new MySqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public static bool InsertPlayerMatch(string matchId, string playerId, string team, int matchKills, int matchDeaths, int matchAssists, int score, int mvps)
        {
            if (DB.RunQuery("INSERT INTO match_player(match_id, player_id, team, kills, deaths, assists, score, mvps) VALUES (@match_id, @player_id, @team, @match_kills, @match_deaths, @match_assists, @score, @mvps);",
                new MySqlParameter("@match_id", matchId),
                new MySqlParameter("@player_id", playerId),
                new MySqlParameter("@team", team),
                new MySqlParameter("@match_kills", matchKills),
                new MySqlParameter("@match_deaths", matchDeaths),
                new MySqlParameter("@match_assists", matchAssists),
                new MySqlParameter("@score", score),
                new MySqlParameter("@mvps", mvps)) == null)
                return false;

            return true;
        }
    }
}
