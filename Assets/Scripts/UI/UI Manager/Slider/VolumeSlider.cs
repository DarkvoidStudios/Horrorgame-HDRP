using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioSource soundController;

    [Header("Slider settings")]
    public Slider slider;
    public TextMeshProUGUI sliderTextValue;
    public AudioSource changeSound;

    private void Start()
    {
        //Update first frame with current slider
        sliderTextValue.SetText(slider.value.ToString());

        //Eventlistener
        slider.onValueChanged.AddListener(delegate { updateSliderValue(); });
    }

    //Update slider text
    private void updateSliderValue()
    {
        //Changes text
        sliderTextValue.SetText(slider.value.ToString());
        
        //Changes volume
        soundController.volume = slider.value/100;
    }
}
