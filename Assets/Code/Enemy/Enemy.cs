using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName = "Zombie";
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    private Animator animator;
    private Collider myCollider;
    private bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
        myCollider = GetComponent<Collider>();
    }

    public float GetHealthPercent()
    {
        return Mathf.Clamp01(currentHealth / maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth > 0) animator?.SetTrigger("Hit");
        else Die();
    }

    void Die()
    {
        isDead = true;
        animator?.SetTrigger("Die");
        if (myCollider != null) myCollider.enabled = false;
        Destroy(gameObject, 5f);
    }
}
