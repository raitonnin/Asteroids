using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D rb;
    public float maxLifetime = 10.0f;
    public float speed = 500.0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void Project(Vector2 direction)
    {
        rb.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
