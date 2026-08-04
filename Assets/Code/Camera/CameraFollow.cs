using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // Ziehe hier deinen Spieler (Player) rein

    [Header("Settings")]
    public Vector3 offset;   // Der Abstand zum Spieler
    public float smoothSpeed = 5f; // Wie "weich" die Kamera folgt (Dämpfung)

    void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    // LateUpdate läuft NACHDEM sich der Spieler bewegt hat -> Verhindert Zittern
    void LateUpdate()
    {
        if (target == null) return;

        // Berechne die gewünschte Position (Spieler-Position + Abstand)
        Vector3 desiredPosition = target.position + offset;

        // Bewege die Kamera weich dort hin (Interpolation)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Setze die Position
        transform.position = smoothedPosition;

        // (Optional) Kamera schaut immer auf den Spieler (gut für Zoom-Effekte)
        // transform.LookAt(target);
    }

}
