using Selivura;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IPunchable
{
    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 direction, float speed)
    {
        Vector2 targetSpeed = direction * speed;
        Vector2 speedDiff = targetSpeed - _rb.velocity;
        _rb.AddForce(speedDiff, ForceMode2D.Force);
    }
    public void Punch(Vector2 direction)
    {
        _rb.AddForce(direction, ForceMode2D.Impulse);
    }
    public void StopMoving()
    {
        _rb.velocity = Vector2.zero;
    }
}
