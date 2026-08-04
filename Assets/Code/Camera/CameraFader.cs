using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFader : MonoBehaviour
{
    public Transform player;          // Ziehe deinen Player hier rein
    public LayerMask wallLayer;      // Layer "Environment" ausw‰hlen
    public float fadedOpacity = 0.3f; // Wie durchsichtig soll es werden?

    private List<ObjectFader> objectsBlockingView = new List<ObjectFader>();

    void Update()
    {
        if (player == null) return;

        // Richtung von Kamera zum Spieler berechnen
        Vector3 direction = player.position - transform.position;
        float distance = Vector3.Distance(transform.position, player.position);

        // Raycast schieﬂen
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, wallLayer);

        // Aktuelle Blocker sammeln
        List<ObjectFader> currentHits = new List<ObjectFader>();

        foreach (var hit in hits)
        {
            ObjectFader fader = hit.collider.GetComponent<ObjectFader>();
            if (fader != null)
            {
                fader.DoFade(fadedOpacity);
                currentHits.Add(fader);
                if (!objectsBlockingView.Contains(fader)) objectsBlockingView.Add(fader);
            }
        }

        // Blocker, die nicht mehr im Weg sind, wieder einblenden
        for (int i = objectsBlockingView.Count - 1; i >= 0; i--)
        {
            if (!currentHits.Contains(objectsBlockingView[i]))
            {
                objectsBlockingView[i].ResetFade();
                objectsBlockingView.RemoveAt(i);
            }
        }
    }
}
