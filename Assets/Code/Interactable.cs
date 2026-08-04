using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float baseRadius = 1.5f;

    public virtual float GetInteractionRadius(GameObject player)
    {
        return baseRadius;
    }

    public virtual Vector3 GetInteractPoint(GameObject interactor)
    {
        return transform.position;
    }

    public abstract void Interact(GameObject interactor);
}
