using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForceReceiver : MonoBehaviour
{
    private bool isGrounded;

    [SerializeField] private float drag = 0.3f;

    private float verticalVelocity;

    private Player player;

    public Vector2 Movement => impact + Vector2.up * verticalVelocity;
    private Vector2 dampingVelocity;
    private Vector2 impact;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (player.IsGrounded())
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }        
        impact = Vector2.SmoothDamp(impact, Vector2.zero, ref dampingVelocity, drag);
    }

    public void Reset()
    {
        verticalVelocity = 0;
        impact = Vector2.zero;
    }

    public void AddForce(Vector2 force)
    {
        impact += force;
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 vel = rb.velocity;
        vel.y = jumpForce;

        rb.velocity = vel;
    }

}
