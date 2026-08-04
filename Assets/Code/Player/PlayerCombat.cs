// PlayerCombat.cs
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Stats")]
    public float attackRange = 1.5f;
    public float attackSpeed = 1f;
    public float damage = 10f;

    private Transform target;
    private Animator anim;

    private bool holdAttack;
    private bool attackInProgress;
    private float nextAttackTime;

    public bool HasTarget => target != null;
    public Vector3 TargetPosition => target != null ? target.position : transform.position;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetTarget(Transform newTarget, bool hold)
    {
        target = newTarget;
        holdAttack = hold;
    }

    public void ReleaseHold()
    {
        holdAttack = false;
    }

    public void StopCombat()
    {
        target = null;
        holdAttack = false;
        attackInProgress = false;
    }

    public bool IsTargetInRange(Vector3 attackerWorldPos)
    {
        if (target == null) return false;
        return Vector3.Distance(attackerWorldPos, target.position) <= attackRange;
    }

    public void TryStartAttack()
    {
        if (target == null) return;
        if (attackInProgress) return;
        if (Time.time < nextAttackTime) return;

        attackInProgress = true;
        anim.SetTrigger("Attack");
        nextAttackTime = Time.time + 1f / attackSpeed;
    }

    // Animation Event auf dem Attack-Clip (Hit-Frame!)
    public void ExecuteDamageEvent()
    {
        if (target == null)
        {
            attackInProgress = false;
            return;
        }

        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
            enemy.TakeDamage(damage);

        attackInProgress = false;

        // Tap: nach dem Treffer Target freigeben
        if (!holdAttack)
            target = null;
    }
}
