using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using static CheckGameManager;

public class UIKickScreenController : MonoBehaviour
{
    [Header("General Settings")]
    public string checkGameSceneName = "CheckGame";
    [SerializeField]
    public KeyCode retryHotkey;
    private bool keyPressed = false;

    [Header("Text Settings")]
    public TextMeshProUGUI textKickReason;

    private void Start()
    {
        textKickReason.text = kickReason.reason;
    }
private void Update()
    {
        //Wenn die Skip-Taste gedrückt wird
        if (!keyPressed)
        {
            if (Input.GetKeyDown(retryHotkey))
            {
                keyPressed = true;
                Debug.Log("Space Pressed");
                SceneManager.LoadScene(checkGameSceneName);
            }
        }
    }
}