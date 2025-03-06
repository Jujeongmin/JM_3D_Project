using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    [Header("UI")]
    public float curValue;
    public float startValue;
    public float maxValue;
    public Image uiBar;

    void Start()
    {
        // 현재체력을 시작체력으로
        curValue = startValue;     
    }

    void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    //// 현재체력을 반환하는 함수
    float GetPercentage()
    {
        return curValue / maxValue;
    }

    // 체력을 더하는 함수
    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);       
    }

    // 체력을 깎는 함수
    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);        
    }
}
