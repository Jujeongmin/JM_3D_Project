using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public enum EBgm
    {
        BGMTitle,
        BGMGame
    }

    public enum ESfx
    {
        SFXButton,
        SFXJump,
        SFXWalk,
        SFXCoin,
        SFXInventory
    }

    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] sfxs;

    [SerializeField] AudioSource audioBgm;
    [SerializeField] AudioSource audioSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBgm(EBgm.BGMGame);
    }

    public void PlayBgm(EBgm bgmIdx)
    {
        audioBgm.clip = bgms[(int) bgmIdx];
        audioBgm.Play();
    }

    public void StopBgm()
    {
        audioBgm.Stop();
    }

    public void PlaySfx(ESfx esfxIdx)
    {
        audioSfx.PlayOneShot(sfxs[(int) esfxIdx]);
    }
}
