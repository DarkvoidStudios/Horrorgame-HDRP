using Steamworks;
using System;
using UnityEngine;

    public class SteamPlayer : MonoBehaviour
    {
        //Get Steam Player Nickname
        public string getSteamPlayerNickname()
        {
            if (!SteamManager.Initialized) { return null; }
            return SteamFriends.GetPersonaName();
        }

        //Get SteamID64 | Can converted to String
        public CSteamID getSteamID64()
        {
            if (!SteamManager.Initialized) { return new CSteamID(); }
            return SteamUser.GetSteamID();
        }

        //Get HardwareID from player
        public string getHWID()
        {
            return SystemInfo.deviceUniqueIdentifier;
        }
}
