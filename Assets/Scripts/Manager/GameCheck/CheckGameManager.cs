using Steamworks;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Ping = UnityEngine.Ping;

public class CheckGameManager : MonoBehaviour
{
    public UICheckGameController AccountInfo;

    [Header("General Settings")]
    public string kickScreenSceneName = "KickScreen";

    [Header("Network Settings")]
    public string maintenanceAPI = "https://darkvoidstudios.com/api/horrorgame/maintenance.php";
    public int averagePing = 300;
    public int pingTries = 6;

    [Header("Text Settings")]
    public TextMeshProUGUI outputCurrentStep;

    [Header("Steam Informationen")]
    public SteamPlayer steamPlayerInfo;

    //Error messages
    private string msgVersion = "Wrong Game-Version";
    private string msgBanned = "You are banned";
    private string msgConnection = "No Internet-Connection";
    private string servererrorConnectionError = "Failed to communicate with server";
    private string servererrorDataProcessingError = "Client error corrupt communication between server and client";
    private string servererrorProtocolError = "Failed to communicate with server api";

    public IEnumerator initiateAllChecks()
    {
        yield return StartCoroutine(checkVersion("TEST"));
        yield return StartCoroutine(checkBan());
        yield return StartCoroutine(checkServerStatus());
        yield return StartCoroutine(checkConnection());
        yield return true;
    }

    /// <summary>
    /// Checks via Database if player has coreect game verison
    /// </summary>
    /// <returns>Returns true if the user has the correct game version and false if not</returns>
    public IEnumerator checkVersion(string ver)
    {
        //CHECK GAME VERSION
        // ######## CODE TO CHECK GAME VERSION VIA DATABASE ########
        outputCurrentStep.SetText("Checking Game-Version");
        yield return StartCoroutine(delayGame(1));
        Debug.Log("Checking Game-Version");
        bool version = true;
        if (!version)
        {
            Debug.Log(msgVersion);
            kickPlayer(msgVersion);
            yield return false;
        }
        yield return true;
    }

    /// <summary>
    /// Checks via Database if a player is banned
    /// </summary>
    /// <returns>Returns true if the user was banned from the game and false if not</returns>
    public IEnumerator checkBan()
    {
        Debug.Log("Checking Player Ban");
        outputCurrentStep.SetText("Checking Player Ban");
        StartCoroutine(delayGame(5));
        bool isBanned = false;
        if (isBanned)
        {
            Debug.Log("Player is banned");
            kickPlayer(msgBanned);
            yield return false;
        }
        yield return true;
    }

    public IEnumerator checkServerStatus()
    {
        Debug.Log("Checking Server Status");
        outputCurrentStep.SetText("Checking Server Status");
        yield return StartCoroutine(delayGame(1));
        using (UnityWebRequest webRequest = UnityWebRequest.Get(maintenanceAPI))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = maintenanceAPI.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    kickPlayer(servererrorConnectionError);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    kickPlayer(servererrorDataProcessingError);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    kickPlayer(servererrorProtocolError);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);



                    break;
            }
        }
    }

    /// <summary>
    /// Checks the player connection and the average pings
    /// </summary>
    /// <returns>Returns true if the user was banned from the game and false if not</returns>
    public IEnumerator checkConnection()
    {
        Debug.Log("Checking Players Connection");
        outputCurrentStep.SetText("Checking Players Connection");
        yield return StartCoroutine(delayGame(1));
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //No Connection
            Debug.Log(msgConnection);
            kickPlayer(msgConnection);
            yield return false;
        }

        Debug.Log("Checking Players Average Ping");
        outputCurrentStep.SetText("Checking Player Connection");

        int currentTries = 0;
        IPAddress[] ipaddress = Dns.GetHostAddresses("darkvoidstudios.com");
        Debug.Log("Server IP:" + ipaddress[0].ToString());
        while (currentTries < pingTries)
        {
            currentTries++;
            WaitForSeconds f = new WaitForSeconds(0.05f);
            Ping p = new Ping(ipaddress[0].ToString());

            while (!p.isDone)
            {
                yield return f;
            }

            Debug.Log("Ties: " + currentTries + " Time: " + p.time);
        }
        Debug.Break();
        yield return true;
    }

    //A new static class (not inheriting from monobehaviour) that holds kick informations
    public static class kickReason
    {
        public static string reason { get; set; }
    }

    //Load EndScreen / kicks the player
    public void kickPlayer(string reason)
    {
        kickReason.reason = reason;
        SceneManager.LoadScene(kickScreenSceneName);
    }

    private IEnumerator delayGame(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
