using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICondition : MonoBehaviour
{
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI endingText;

    public Condition hp;

    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }

    public void SetRestart()
    {        
        restartText.gameObject.SetActive(true);
        CharacterManager.Instance.Player.controller.ToggleCursor();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameEnding()
    {
        endingText.gameObject.SetActive(true);
    }
}
