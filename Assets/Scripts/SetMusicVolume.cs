using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SetMusicVolume : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;

    private void Start()
    {

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else{
            VolumeSettings();
        }
    }

    public void VolumeSettings()
    {
        float volume = volumeSlider.value;
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        VolumeSettings();
    }

}
