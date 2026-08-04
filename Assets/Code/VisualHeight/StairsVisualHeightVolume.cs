using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class StairsVisualHeightVolume : MonoBehaviour
{
    public enum Axis { X, Z }

    [Header("Stairs Shape")]
    public Axis alongAxis = Axis.Z;   // Richtung des Anstiegs (X oder Z)
    public float length = 4f;         // Länge der Treppe in Welt-Einheiten
    public float topHeight = 1.0f;    // visuelle Höhe am oberen Ende
    public bool invert = false;       // umdrehen (hoch->runter)

    void Awake()
    {
        var box = GetComponent<BoxCollider>();
        box.isTrigger = true;
    }

    public float EvaluateHeight(Vector3 worldPos)
    {
        Vector3 local = transform.InverseTransformPoint(worldPos);

        float t = alongAxis == Axis.X ? local.x : local.z;

        float half = Mathf.Max(0.0001f, length * 0.5f);
        float u = Mathf.InverseLerp(-half, half, t);

        if (invert) u = 1f - u;

        return Mathf.Lerp(0f, topHeight, u);
    }
}