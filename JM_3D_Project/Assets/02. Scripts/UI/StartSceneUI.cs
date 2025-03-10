using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    public void StartButton()
    {
        SoundManager.Instance.PlaySFX("Button");
        SceneManager.LoadScene("MainScene");
    }
        
    public void ExitButton()
    {
        SoundManager.Instance.PlaySFX("Button");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }    
}
