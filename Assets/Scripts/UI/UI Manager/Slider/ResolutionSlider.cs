using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ResolutionSlider : MonoBehaviour
{
    [Header("Graphic Controller / Where the logic happens")]
    public GraphicController graphicController;

    [Header("Slider settings")]
    public Slider slider;
    public TextMeshProUGUI sliderTextValue;
    public AudioSource sliderSoundController;

    private void Start()
    {
        //Autoimport Script
        graphicController = (GraphicController)GameObject.FindObjectOfType(typeof(GraphicController));

        //Update first frame with current slider
        sliderTextValue.SetText(slider.value.ToString());

        //Eventlistener
        slider.onValueChanged.AddListener(delegate { updateSliderValue(); });
    }

    //Update slider text
    private void updateSliderValue()
    {
        sliderTextValue.SetText(slider.value.ToString());
        graphicController.setFieldOfView(slider.value);
        //sliderSoundController.Play();
    }
}
