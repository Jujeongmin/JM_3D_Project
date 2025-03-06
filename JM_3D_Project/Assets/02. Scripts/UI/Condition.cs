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
        // ����ü���� ����ü������
        curValue = startValue;     
    }

    void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    //// ����ü���� ��ȯ�ϴ� �Լ�
    float GetPercentage()
    {
        return curValue / maxValue;
    }

    // ü���� ���ϴ� �Լ�
    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);       
    }

    // ü���� ��� �Լ�
    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);        
    }
}
