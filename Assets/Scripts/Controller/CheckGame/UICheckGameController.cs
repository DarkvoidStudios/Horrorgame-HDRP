using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class UICheckGameController : MonoBehaviour
{
    public CheckGameManager checkGame;

    //Animation
    bool hearFadeOutTrigger = false;
    bool isFadeOutTriggerDropped = false;
    bool hearFadeInTrigger = false;
    bool isFadeInTriggerDropped = false;

    //Async loading / Loadingscreen
    private int sceneIndex = 2;
    private bool allowLoadingToNextScene = false; // Decides at the loading screen when the next scene may be loaded
    private bool allowPlayFadeIn = false; // Decides at the loading screen when the next scene may be loaded


    [Header("Animation Settings")]
    [Header("FadeIn Settings")]
    [Tooltip("Important to check when Animations are done")]
    public WaitForAnimationTrigger testFadeIn;
    public Animator fadeInAnimator;
    public GameObject fadeInObject;
    [Header("FadeOut Settings")]
    [Tooltip("Important to check when Animations are done")]
    public WaitForAnimationTrigger testFadeOut;
    public Animator fadeOutAnimator;
    public GameObject fadeOutObject;

    [Header("LoadingText")]
    [Tooltip("The text who displays which step initiates the game")]
    public TextMeshProUGUI loadingText;

    [Header("Version Settings")]
    public TextMeshProUGUI VersionText;

    private void Start()
    {
        initiateLoading();
        printVersion();
        playFadeOutAnim();
    }

    private void initiateLoading()
    {
        Debug.Log("Preloading Initiated");
        //Starts loading next scene 
        StartCoroutine(loadAsyncNextScene(sceneIndex));

    }
    private void Update()
    {
        //Waiting and checking when FadeOutTrigger dropped
        if (hearFadeOutTrigger && !isFadeOutTriggerDropped)
        {
            isFadeOutTriggerDropped = testFadeOut.checkAnimTriggerDropped("FadeOut");
            if (isFadeOutTriggerDropped)
            {
                Debug.Log("FadeOut animation done");
                
                StartCoroutine(checkGame.initiateAllChecks());
                //Allows to load next scene 
                allowPlayFadeIn = true; // At this point, the scene will be switched automatically when the game is finished loading*/
            }
        }

        //Waiting and checking when FadeInTrigger dropped
        if (hearFadeInTrigger && !isFadeInTriggerDropped)
        {
            isFadeInTriggerDropped = testFadeIn.checkAnimTriggerDropped("FadeIn");
            if (isFadeInTriggerDropped)
            {
                Debug.Log("FadeIn animation done");
                //Allows to load next scene 
                allowLoadingToNextScene = true; // At this point, the scene will be switched automatically when the game is finished loading
            }
        }
    }

    //Print game version
    public string printVersion()
    {
        string version = Application.version;
        VersionText.text = version;
        return version;
    }

    private void playFadeOutAnim()
    {
        Debug.Log("Waiting for FadeOut animation");
        //Enable fadeOutObject (Gameobject)
        fadeOutObject.SetActive(true);
        //Starting playing animation
        fadeOutAnimator.Play("Loadingfadeout", 0, 0.0f);
        hearFadeOutTrigger = true;
    }

    private void initiateGameChecks()
    {
        checkGame.initiateAllChecks();
        Debug.Log("All checks successfull");
    }

    private void playFadeInAnim()
    {
        Debug.Log("Waiting for FadeIn animation");
        //Enable fadeInObject (Gameobject)
        fadeInObject.SetActive(true);
        //Starting playing animation
        fadeInAnimator.Play("Loadingfadein", 0, 0.0f);
        hearFadeInTrigger = true;
    }

    private IEnumerator loadAsyncNextScene(int sceneIndex)
    {
        //Start an asynchronous operation to load the main game scene
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        async.allowSceneActivation = false;


        while (async.progress < 0.9f)
        {
            // Report progress etc.
            string progress = "Progress: " + async.progress.ToString();
            Debug.Log(progress);
            yield return null;
        }
        Debug.Log("Preloading complete -> Waiting for the approval of the next scene");
        while (!allowPlayFadeIn)
        {
            //Waiting for allowLoadingToNextScene / Waiting for ok to load next scene
            yield return null;
        }

        playFadeInAnim();
        while (!allowLoadingToNextScene)
        {
            //Waiting for allowLoadingToNextScene / Waiting for ok to load next scene
            yield return null;
        }
        async.allowSceneActivation = true;
    }
}
