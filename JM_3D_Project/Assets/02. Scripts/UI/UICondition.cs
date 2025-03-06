using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition hp;
    
    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
}
