using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UIIntroController : MonoBehaviour
{
    //Import Animation Trigger
    public WaitForAnimationTrigger AnimationTriggers;
    public PanelManager panelManager;
    public CanvasGroupActivator canvasGroupActivator;

    [Header("General Settings")]
    private bool currentSceneCompleteLoaded = false;

    [Header("Intro Settings")]
    public GameObject developerIntroPanel;

    public bool enableGameIntro = true;
    public GameObject gameIntroPanel;

    [Header("Disclaimer Settings")]
    public bool enableDisclaimer = true;
    public GameObject disclaimerPanel;
    [Tooltip("The time how long the disclaimer is enabled (in seconds)")]
    public int showTimeOfDisclaimer;

    [Header("Loadingscreen Settings")]
    private int sceneIndex = 1;
    private bool allowLoadingNextScene = false;

    [Header("Skip Settings")]
    public GameObject skipPanel;
    private bool isKeyPressed = false;
    [SerializeField]
    public KeyCode hotkey;

    [Header("Fade Settings")]
    public Animator animator;
    public GameObject FadeBackgroundObject;
    private bool readyToAnimate = false;

    private IEnumerator Start()
    {
        canvasGroupActivator = (CanvasGroupActivator)GameObject.FindObjectOfType(typeof(CanvasGroupActivator));
        panelManager = (PanelManager)GameObject.FindObjectOfType(typeof(PanelManager));

        panelManager.DisableAllPanelVisibilitys();
        yield return WaitForCurrentSceneFullyLoaded();

        StartCoroutine(LoadAsynchronously(sceneIndex));
        yield return StartCoroutine(PlayStudioIntro());
        yield return StartCoroutine(PlayGameIntro());
        yield return StartCoroutine(ShowDisclaimer());
        allowLoadingNextScene = true;
    }


    private void Awake()
    {
        currentSceneCompleteLoaded = true;
    }

    /// <summary>
    /// This method waits until the current scene is fully loaded.
    /// This was invented because of a bug where the intro starts playing while the scene is loading (glitched sounds)
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForCurrentSceneFullyLoaded()
    {

        while (currentSceneCompleteLoaded == false)
        {
            yield return null;
        }

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKeyPressed)
        {
            if (Input.GetKeyDown(hotkey))
            {
                isKeyPressed = true;
                VideoPlayer videoPlayer = gameIntroPanel.GetComponentInChildren<VideoPlayer>();
                videoPlayer.Stop();
                gameIntroPanel.SetActive(false);
                Debug.Log("Skipping Intro Pressed");
            }
        }
    }


    public IEnumerator PlayStudioIntro()
    {
        developerIntroPanel.SetActive(true);
        panelManager.EnablePanelVisibility(developerIntroPanel);
        VideoPlayer videoPlayer = developerIntroPanel.GetComponentInChildren<VideoPlayer>();

        videoPlayer.frame = 0;
        videoPlayer.Play();
        Debug.Log("Studio Intro Startet");

        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        Debug.Log("Studio Intro Ended");
        developerIntroPanel.SetActive(false);
        panelManager.DisablePanelVisibility(developerIntroPanel);
        yield return null;
    }

    public IEnumerator PlayGameIntro()
    {
        if (enableGameIntro)
        {
            gameIntroPanel.SetActive(true);
            skipPanel.SetActive(true);

            canvasGroupActivator.enableCanvasGroup(skipPanel.GetComponent<CanvasGroup>());
            VideoPlayer videoPlayer = gameIntroPanel.GetComponentInChildren<VideoPlayer>();

            videoPlayer.Play();
            Debug.Log("Game Intro Startet");

            while (videoPlayer.isPlaying)
            {
                yield return null;
            }

            Debug.Log("Game Intro Ended");
            gameIntroPanel.SetActive(false);
            skipPanel.SetActive(false);
            canvasGroupActivator.disableCanvasGroup(skipPanel.GetComponent<CanvasGroup>());
            yield return null;
        }
        Debug.Log("Game Intro Is Disabled");
        yield return null;
    }

    public IEnumerator StartFadeIn()
    {
        if (readyToAnimate == false)
        {
            readyToAnimate = true;
            FadeBackgroundObject.SetActive(true);
            Debug.Log("Loading complete");
            Debug.Log("Wating for animation end");
            animator.Play("Loadingfadein", 0, 0.0f);
        }
        yield return null;
    }

    public IEnumerator ShowDisclaimer()
    {
        disclaimerPanel.SetActive(true);
        canvasGroupActivator.enableCanvasGroup(disclaimerPanel.GetComponent<CanvasGroup>());

        Debug.Log("Showing Disclaimer");

        yield return new WaitForSeconds(showTimeOfDisclaimer);

        Debug.Log("Suppressing Disclaimer");

        canvasGroupActivator.disableCanvasGroup(disclaimerPanel.GetComponent<CanvasGroup>());
        disclaimerPanel.SetActive(false);
        yield return null;
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        //Start an asynchronous operation to load the main game scene
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
        async.allowSceneActivation = false;

        //While the asynchronous operation to load the new scene is not yet complete, continue 
        //waiting until it's done.
        while (!async.isDone)
        {
            if (async.progress >= 0.9f)
            {
                if (allowLoadingNextScene)
                {
                    async.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
