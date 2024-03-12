using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    private Player player;

    Rigidbody rB;

    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        rB = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        // Get targetMovingSpeed.
        float targetMovingSpeed = player.moveSpeed;
        if (speedOverrides.Count > 0)
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rB.velocity = transform.rotation * new Vector3(targetVelocity.x, rB.velocity.y, targetVelocity.y);
    }
}