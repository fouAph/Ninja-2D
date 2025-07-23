using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [HideInInspector] public UnityEvent OnTakeDamage;
    public UnityEvent OnDie;

    [SerializeField] int maxHealth = 100;

    private int currentHealth;

    void Start()
    {
        ResetHealth();
        OnDie.AddListener(OnDieEvent);
    }

    [ContextMenu("ResetHealth")]
    private void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnTakeDamage?.Invoke();

        if (currentHealth == 0)
            OnDie.Invoke();
    }

    public void OnDieEvent()
    {
    }
}

public interface IDamageable
{
    void TakeDamage(int damage);
}