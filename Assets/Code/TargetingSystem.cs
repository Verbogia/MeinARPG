using UnityEngine;
using System;

public class TargetingSystem : MonoBehaviour
{
    private Interactable currentTarget;

    // Event, wenn sich das Target ändert (Highlight/UI)
    public event Action<Interactable> OnTargetChanged;

    // Öffentlich lesbar
    public Interactable CurrentTarget => currentTarget;

    public void SetTarget(Interactable newTarget)
    {
        if (currentTarget != newTarget)
        {
            currentTarget = newTarget;
            OnTargetChanged?.Invoke(currentTarget);
        }
    }

    public void ClearTarget()
    {
        if (currentTarget != null)
        {
            currentTarget = null;
            OnTargetChanged?.Invoke(null);
        }
    }
}
