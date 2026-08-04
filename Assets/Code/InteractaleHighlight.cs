using UnityEngine;

public class InteractableHighlight : MonoBehaviour
{
    private Renderer[] renderers;
    public float glowIntensity = 0.7f;
    private bool isHighlighted = false;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void ToggleHighlight(bool on)
    {
        if (isHighlighted == on) return;
        isHighlighted = on;

        foreach (var r in renderers)
        {
            if (on)
            {
                Color highlightColor = Color.white * glowIntensity;
                r.material.EnableKeyword("_EMISSION");
                r.material.SetColor("_EmissionColor", highlightColor);
            }
            else
            {
                r.material.SetColor("_EmissionColor", Color.black);
                r.material.DisableKeyword("_EMISSION");
            }
        }
    }
}
