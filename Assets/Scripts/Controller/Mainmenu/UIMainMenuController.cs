using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuController : MonoBehaviour
{
    public PanelManager panelManager;

    [Header("FirstLoadingPanel")]
    public GameObject firstLoadingPanel;

    [Header("Map Background")]
    public string mapName;
    public AsyncSceneLoader sceneLoader;

    [Header("Steam Informationen")]
    public SteamPlayer steamPlayerInfo;

    [Header("Fade Settings")]
    public Animator animator;
    public GameObject fadeBackgroundObject;

    [Header("Steam")]
    public TextMeshProUGUI textSteamName;

    // Start is called before the first frame update
    void Start()
    {
        panelManager = (PanelManager)GameObject.FindObjectOfType(typeof(PanelManager));
        panelManager.LoadInspectorSettings();

        //Loads all needed scenes
        sceneLoader.addScene(mapName);
        sceneLoader.loadAllScenes();

        showFadeIn();
        showPlayerName();
    }
    private void showFadeIn()
    {
        fadeBackgroundObject.SetActive(true);
        animator.Play("Loadingfadeout", 0, 0.0f);
    }

    private void showPlayerName()
    {
        //Get SteamName
        textSteamName.text = steamPlayerInfo.getSteamPlayerNickname();
        Debug.LogError(steamPlayerInfo.getSteamID64());
    }
}
