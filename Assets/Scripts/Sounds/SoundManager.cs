using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class SoundManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [Header("RESOURCES")]
    //Importing sound settings from the controller
    public HoverAndClickSoundcontroller SoundController;

    [Header("SETTINGS")]
    public bool enableHoverSound = true;
    public bool enableClickSound = true;

    //Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (enableHoverSound == true)
        {
            SoundController.SoundPlayer.PlayOneShot(SoundController.Hover, SoundController.ClickVolume);
        }
    }

    //Click
    public void OnPointerClick(PointerEventData eventData)
    {
        if (enableClickSound == true)
        {
            SoundController.SoundPlayer.PlayOneShot(SoundController.Click, SoundController.ClickVolume);
        }
    }
}