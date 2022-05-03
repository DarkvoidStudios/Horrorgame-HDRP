using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public GameObject ExitGamePanel;

    void Start()
    {
        ExitGamePanel.SetActive(true);
        ExitGamePanel.GetComponent<CanvasGroup>().alpha = 0;
    }
    public void InitiateExitWindow()
    {
        if (ExitGamePanel.GetComponent<CanvasGroup>().alpha > 0)
        {
            ExitGamePanel.GetComponent<CanvasGroup>().alpha = 0;
            ExitGamePanel.GetComponent<CanvasGroup>().interactable = false;
            ExitGamePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            Debug.Log("Unloaded exit window");
        } else
        {
            ExitGamePanel.GetComponent<CanvasGroup>().alpha = 1;
            ExitGamePanel.GetComponent<CanvasGroup>().interactable = true;
            ExitGamePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
            Debug.Log("Loaded exit window");
        }
    }
    public void closeGame()
    {
        Application.Quit();
        Debug.Log("Closed Game");
    }
}
