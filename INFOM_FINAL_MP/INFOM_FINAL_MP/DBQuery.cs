﻿using MySql.Data.MySqlClient;
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
            command = DB.RunQuery("SELECT p.steam_name, COUNT(mp.match_player_id) AS total_matches, @total_kills := (SUM(mp.kills)) AS kills, @total_deaths := (SUM(mp.deaths)) AS deaths, @total_mvps := (SUM(mp.mvps)) AS mvps, FORMAT(@total_kills / @total_deaths, 2) AS kd, @total_wins := (SUM(IF(mp.team = 'A', m.score_a, m.score_b) = 16)) AS wins, @total_loses := (SUM(IF(mp.team = 'A', m.score_a, m.score_b) != 16)) AS loses, FORMAT((@total_wins / (@total_wins + @total_loses) * 100), 2) AS win_rate FROM players p INNER JOIN match_player mp ON p.player_id = mp.player_id INNER JOIN matches m ON mp.match_id = m.match_id WHERE p.player_id = (@player_id) ORDER BY p.player_id;",
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
                float.Parse(playerData["win_rate"].ToString()));
            return player;
        }

        public static bool CreatePlayer(Player player)
        {
            return true; // For now!

            command = DB.RunQuery("",
                new MySqlParameter());

            if (command == null)
                return false;

            return true;
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
    }
}
