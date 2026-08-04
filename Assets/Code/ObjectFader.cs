using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    private Renderer renderer;
    private Color originalColor;
    private float targetOpacity = 1.0f;
    private float smoothTime = 10f;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    void Update()
    {
        // Sanfter Übergang der Transparenz
        Color currentColor = renderer.material.color;
        float newAlpha = Mathf.Lerp(currentColor.a, targetOpacity, Time.deltaTime * smoothTime);
        renderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
    }

    public void DoFade(float opacity) => targetOpacity = opacity;
    public void ResetFade() => targetOpacity = originalColor.a;
}
