using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject settingMenu;

    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
        
    public void ExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void Active()
    {
        settingMenu.SetActive(!settingMenu.activeSelf);
    }
}
