// PlayerController.cs
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerCombat combat;

    private PlayerCommand activeCommand;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        combat = GetComponentInChildren<PlayerCombat>();
        activeCommand = default;
        activeCommand.Type = PlayerCommandType.None;
    }

    public void ReceiveCommand(PlayerCommand command)
    {
        activeCommand = command;

        switch (command.Type)
        {
            case PlayerCommandType.Move:
                combat.StopCombat();
                movement.SetDestination(command.WorldPoint);
                break;

            case PlayerCommandType.Attack:
                // Nicht stoppen. Wir chasen bis in Range.
                combat.SetTarget(command.Target, command.IsHeld);
                break;

            case PlayerCommandType.Interact:
                // Interact-Logik lassen wir erstmal minimal:
                // Move hin, Interact selbst machen wir später sauber.
                combat.StopCombat();
                movement.SetDestination(command.Target.position);
                break;

            default:
                break;
        }
    }

    public void ReleaseHold()
    {
        // Bei MouseUp: nur Hold beenden – laufender Schlag darf zu Ende laufen
        activeCommand.IsHeld = false;
        combat.ReleaseHold();
    }

    void Update()
    {
        if (activeCommand.Type == PlayerCommandType.Attack)
        {
            TickAttackCommand();
        }
    }

    private void TickAttackCommand()
    {
        if (combat == null)
            return;

        if (!combat.HasTarget)
        {
            activeCommand.Type = PlayerCommandType.None;
            return;
        }

        // Chase bis in Range
        if (!combat.IsTargetInRange(transform.position))
        {
            if (!movement.IsMoving())
                movement.SetDestination(combat.TargetPosition);

            return;
        }

        // In Range: stehen bleiben und Attack-Cycle auslösen
        movement.StopMoving();
        combat.TryStartAttack();

        // Wenn kein Hold: Combat löscht Target nach Damage-Event.
        // Hier NICHT löschen, sonst verlierst du wieder den Hit-Frame.
    }
}
