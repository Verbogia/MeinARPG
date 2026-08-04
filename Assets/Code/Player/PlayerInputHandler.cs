using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float navSampleRadius = 1f;

    private PlayerController controller;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SendCommand(false);

        if (Input.GetMouseButton(0))
            SendCommand(true);

        if (Input.GetMouseButtonUp(0))
            controller.ReleaseHold();
    }

    void SendCommand(bool held)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int mask = groundLayer | enemyLayer | interactableLayer;

        if (!Physics.Raycast(ray, out RaycastHit hit, 100f, mask))
            return;

        PlayerCommand cmd = BuildCommand(hit, held);
        controller.ReceiveCommand(cmd);
    }

    PlayerCommand BuildCommand(RaycastHit hit, bool held)
    {
        Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            return new PlayerCommand
            {
                Type = PlayerCommandType.Attack,
                Target = enemy.transform,
                IsHeld = held
            };
        }

        Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
        if (interactable != null)
        {
            return new PlayerCommand
            {
                Type = PlayerCommandType.Interact,
                Target = interactable.transform,
                IsHeld = held
            };
        }

        return new PlayerCommand
        {
            Type = PlayerCommandType.Move,
            WorldPoint = hit.point,
            IsHeld = held
        };
    }
}
