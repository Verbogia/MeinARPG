using UnityEngine;

public class VisualHeightDriver : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform visualRoot;

    [Header("Update")]
    [SerializeField] private float positionThreshold = 0.05f;
    [SerializeField] private float smooth = 20f;

    private StairsVisualHeightVolume activeVolume;
    private Vector3 lastSamplePos;
    private float currentY;

    void Awake()
    {
        if (visualRoot == null)
            Debug.LogError($"{nameof(VisualHeightDriver)}: visualRoot ist nicht gesetzt auf {name}.");

        lastSamplePos = transform.position;
    }

    void Update()
    {
        if (visualRoot == null) return;

        float targetY = 0f;

        if (activeVolume != null)
        {
            Vector3 p = transform.position;
            if ((p - lastSamplePos).sqrMagnitude >= positionThreshold * positionThreshold)
            {
                targetY = activeVolume.EvaluateHeight(p);
                lastSamplePos = p;
            }
            else
            {
                targetY = currentY;
            }
        }

        if (smooth > 0f)
            currentY = Mathf.Lerp(currentY, targetY, 1f - Mathf.Exp(-smooth * Time.deltaTime));
        else
            currentY = targetY;

        Vector3 lp = visualRoot.localPosition;
        lp.y = currentY;
        visualRoot.localPosition = lp;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StairsVisualHeightVolume vol))
            activeVolume = vol;
    }

    void OnTriggerExit(Collider other)
    {
        if (activeVolume != null && other.gameObject == activeVolume.gameObject)
            activeVolume = null;
    }
}