using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    private Rigidbody2D rb;
    public bool _thrusting;
    public float _turnDirection;
    public float _thrustSpeed;
    public float _turnSpeed;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    } 
    void Update()
    {
        _thrusting = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            _turnDirection = 1.0f;
        }else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            _turnDirection = -1.0f;
        }else{
            _turnDirection = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }
    void FixedUpdate()
    {
        if (_thrusting)
        {
            rb.AddForce(this.transform.up *this._thrustSpeed);
        }

        if (_turnDirection != 0f)
        {
            //transform.Rotate(Vector3.forward, (_turnDirection * this._turnSpeed));
            rb.AddTorque(_turnDirection * this._turnSpeed);
        }

    }
    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}


