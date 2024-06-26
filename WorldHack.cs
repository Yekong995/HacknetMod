using BepInEx;
using HarmonyLib;
using Hacknet;
using Hacknet.Daemons.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pathfinder.Action;
using Pathfinder.Administrator;
using Pathfinder.Util;
using Pathfinder.Daemon;
using Pathfinder.GUI;
using Pathfinder.Mission;
using Pathfinder.Util.XML;
using System;

using WorldToolKit.Command;

namespace WorldHack
{
    [BepInPlugin(ModGUID, ModName, ModVer)]
    public class WorldHack : BepInEx.Hacknet.HacknetPlugin
    {
        public const string ModGUID = "com.worldhack";
        public const string ModName = "Worldhack";
        public const string ModVer = "2.1.0";

        public override bool Load()
        {
            Pathfinder.Command.CommandManager.RegisterCommand("PortHack", CommandManager.PortHackCommand);
            Pathfinder.Command.CommandManager.RegisterCommand("Bypass", CommandManager.BypassCommand);
            Pathfinder.Command.CommandManager.RegisterCommand("Firewall", CommandManager.FirewallCommand);
            Pathfinder.Command.CommandManager.RegisterCommand("InitializeBackdoor", CommandManager.InitializeBackdoor);
            Pathfinder.Command.CommandManager.RegisterCommand("Backdoor", CommandManager.BackdoorCommand);
            Pathfinder.Command.CommandManager.RegisterCommand("TrapKill", CommandManager.TrapKill);
            Pathfinder.Command.CommandManager.RegisterCommand("Crasher", CommandManager.CrashCommand);

            return true;
        }

        public override bool Unload()
        {
            return base.Unload();
        }
    }
}
