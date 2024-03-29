using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    private Rigidbody2D rb;
    public bool _thrusting;
    public float _turnDirection;
    public float _thrustSpeed;
    public float _turnSpeed;

     public float respawnDelay = 3f;
    public float respawnInvulnerability = 3f;

    [SerializeField] AudioClip[] _clips;
    [SerializeField] AudioMixerGroup[] mixerGroup; 
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    } 
        private void OnEnable()
    {
        // Turn off collisions for a few seconds after spawning to ensure the
        // player has enough time to safely move away from asteroids
        gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        Invoke(nameof(TurnOnCollisions), respawnInvulnerability);
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
        var clip = _clips[0];
        GetComponent<AudioSource>().PlayOneShot(clip , 1);
    }
        private void TurnOnCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            var clip = _clips[1];
        AudioSource.PlayClipAtPoint(clip, this.transform.position, 1);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }

    }


}


