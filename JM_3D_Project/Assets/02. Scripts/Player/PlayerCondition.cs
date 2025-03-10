using System;
using UnityEngine;

public interface IDamageable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition uiCondition;    
    public PlayerController playerController;
    private Animator animator;

    Condition hp { get { return uiCondition.hp; } }

    public event Action onTakeDamage;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();     
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(hp.curValue <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        hp.Add(amount);
    }

    public void Die()
    {
        if (!playerController.isDead)
        {
            playerController.isDead = true;
            animator.SetTrigger("IsDead");
            CharacterManager.Instance.Player.condition.uiCondition.SetRestart();
        }
    }

    public void TakePhysicalDamage(int damage)
    {
        hp.Subtract(damage);
        onTakeDamage?.Invoke();
    }

}
