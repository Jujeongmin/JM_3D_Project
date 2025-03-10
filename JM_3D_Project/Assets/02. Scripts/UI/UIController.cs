using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _bgmSlider, _sfxSlider;

    public void ToggleMusic()
    {
        SoundManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        SoundManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        SoundManager.Instance.MusicVolume(_bgmSlider.value);
    }
    public void SFXVolume()
    {
        SoundManager.Instance.MusicVolume(_sfxSlider.value);
    }
}
