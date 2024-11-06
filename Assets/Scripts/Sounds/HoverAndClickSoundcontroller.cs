using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HoverAndClickSoundcontroller : MonoBehaviour
{
    [Header("Hover and click resources")]
    [Range(0f, 1f)]
    public float ClickVolume;
    public AudioClip Hover;
    public AudioClip Click;

    [Header("Use Sound Source in this GameObject")]
    public AudioSource SoundPlayer;
}
