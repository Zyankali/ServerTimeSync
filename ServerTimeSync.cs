using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Oxide.Game.Rust.Libraries;


namespace Oxide.Plugins
{
    [Info("ServerTimeSync", "zyankali", "0.3.2")]
    [Description("Sync Server core time too ingame time")]
    public class ServerTimeSync : CovalencePlugin
    {

        //Out kommented test and debugging part - Will be later removed!
        /*[Command("timeset")]
        private void TestCommand(IPlayer player, string command, string[] args)
        {

            //server.Command("env.time", args);


            // args gives out System.String[] how to acces it´s data? No need, use server.Time instead!
            server.Command("env.time", args);

            Puts("Used float time was: " + server.Time);
            
        }*/

        void OnServerInitialized(string[] args)
        {

            int progressset = progressset = 1;

            server.Command("env.progresstime ", progressset);

        }

        void Unloaded(string[] args)
        {
            int progressset = progressset = 1;

            server.Command("env.progresstime ", progressset);
        }

        // When a player joins the server switches from ingame time to Local Server core time and stops ingame time sight progress.
        void OnPlayerConnected(Network.Message packet, string[] args)
        {

            int progressset = progressset = 0;
            
            server.Command("env.progresstime ", progressset);

            //Run timer avery realtime 60 seconds repeatitly, turned out going lower isn´t neccessary
            timer.Every(60f, () =>
            {

                DateTime currentLocalTime = DateTime.Now;

                float hour = currentLocalTime.Hour;
                float minute = currentLocalTime.Minute;
                float second = currentLocalTime.Second;

                float a = minute / 60;
                float b = second / 60;

                b = b * 0.01F;

                float d = (a + b);

                float e = hour + d;

                string s = e.ToString(CultureInfo.InvariantCulture);

                server.Command("env.time ", e);

                Puts("New syncted realtime: env.time " + e);

            });

            Puts("OnPlayerConnected works!");
        }

        private void Init(string[] args)
        {

            

        }

    }

}

