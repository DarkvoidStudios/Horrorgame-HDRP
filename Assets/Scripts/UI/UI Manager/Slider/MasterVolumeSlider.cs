using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSlider : MonoBehaviour
{
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
        sliderTextValue.SetText(slider.value.ToString());
        //AudioListener is volume for all || Max Volume 1
        AudioListener.volume = slider.value / 100;
    }
}
