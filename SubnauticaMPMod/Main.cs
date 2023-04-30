﻿namespace SubnauticaMPMod
{
    [BepInPlugin(mod_guid, mod_name, version_string)]
    public class SubnauticaMPPlugin : BaseUnityPlugin
    {
        private const string mod_guid = "com.sbzm_project.sbzmultiplayer";
        private const string mod_name = "SBZ Multiplayer Mod";
        private const string version_string = "3.0.0_a1";

        private static readonly Harmony harmony = new Harmony(mod_guid);

        public static ManualLogSource? logger;

        private void Awake()
        {
            harmony.PatchAll();
            Logger.LogInfo(mod_name + " " + version_string + " loaded.");
            logger = Logger;
        }
    }
}