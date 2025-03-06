using System;
using UnityEngine;

public interface IDamageable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition uiCondition;

    Condition hp { get { return uiCondition.hp; } }

    public event Action onTakeDamage;

    public void Heal(float amount)
    {
        hp.Add(amount);
    }

    public void Die()
    {
        Debug.Log("»ç¸Á");
    }

    public void TakePhysicalDamage(int damage)
    {
        hp.Subtract(damage);
        onTakeDamage?.Invoke();
    }

}
