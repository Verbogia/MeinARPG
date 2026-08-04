using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private NavMeshPath path;
    private int currentWaypointIndex = 0;
    private bool isMoving = false;

    [Header("Settings")]
    public float moveSpeed = 5f;
    public Animator anim;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        path = new NavMeshPath();
    }

    void Update()
    {
        if (isMoving) FollowPath();
        if (anim != null)
            anim.SetFloat("Speed", isMoving ? moveSpeed : 0f);
    }

    public void SetDestination(Vector3 dest)
    {
        if (!NavMesh.CalculatePath(transform.position, dest, NavMesh.AllAreas, path)) return;
        if (path.status == NavMeshPathStatus.PathInvalid) return;

        currentWaypointIndex = 1;
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
        currentWaypointIndex = 0;
        path.ClearCorners();
        if (anim != null) anim.SetFloat("Speed", 0f);
    }

    void FollowPath()
    {
        if (path == null || path.corners.Length <= currentWaypointIndex)
        {
            isMoving = false;
            return;
        }

        Vector3 target = path.corners[currentWaypointIndex];
        Vector3 dir = target - transform.position;
        dir.y = 0;

        if (dir.sqrMagnitude > 0.04f)
        {
            controller.Move(dir.normalized * moveSpeed * Time.deltaTime);
            if (dir != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 15f);

            if (anim != null) anim.SetFloat("Speed", moveSpeed);
        }
        else
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= path.corners.Length) isMoving = false;
        }
    }

    // ================= Hilfsmethoden =================

    public Vector3 GetMoveDirection()
    {
        if (path == null || currentWaypointIndex >= path.corners.Length)
            return Vector3.zero;

        Vector3 dir = path.corners[currentWaypointIndex] - transform.position;
        dir.y = 0;
        return dir.normalized;
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
