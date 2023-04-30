﻿using ClientSubnautica.MultiplayerManager.SendData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientSubnautica.MultiplayerManager
{
    class ConnectToServer
    {
        //Connect to server
        public static TcpClient Start(string ip, string nickname = null)
        {
            string[] ipArray = ip.Split(':');
            string playername;
            playername = nickname;
            if(nickname == null)
                playername = $"Player{MainPatcher.id}";
            try
            {
                MainPatcher.configFile["nickname"] = playername;
                var newjson = JsonConvert.SerializeObject(MainPatcher.configFile, Formatting.Indented);
                string conffilePath = Path.Combine(MainPatcher.modFolder, "player.json");
                File.WriteAllText(conffilePath, newjson);
            }
            catch (Exception e)
            {
                ErrorMessage.AddError(e.Message);
                throw new Exception(e.Message, e);
            }

            IPAddress ipDest = IPAddress.Parse(ipArray[0]);
            int port = int.Parse(ipArray[1]);
            TcpClient client = new TcpClient();

            client.Connect(ipDest, port);
            // Send the ID of the player to the server. [HAVE IMPERATELY TO BE THE FIRST REQUEST]
            SendMyID.start(client);
            // Check if there are other players, if yes, each player will be generated
            GetAllPlayers.start(client);
            return client;
        }
    }
}
