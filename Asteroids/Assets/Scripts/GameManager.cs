using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;

    public ParticleSystem explosion;

    public int lives = 3;

    public float respawnTime = 3.0f;

    public float respawnInvulnerability = 3.0f;

    public int score = 0;

    public float smallAsteroid  =  .75f;
    public float mediumAsteroid =  1.2f;
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if (asteroid.size < smallAsteroid){
            this.score += 100;
        } else if (asteroid.size < mediumAsteroid) {
            this.score += 50;
        } else {
            this.score += 25;
        }
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.lives-- ;

        if (this.lives <= 0)
        {
            GameOver();
        } else {
            Invoke(nameof(Respawn), this.respawnTime);
        }
        
    }
    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.gameObject.SetActive(true);
        
        Invoke(nameof(TurnOnCollisions), this.respawnInvulnerability);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        this.lives = 3;
        this.score = 0;

        Invoke(nameof(Respawn), this.respawnTime);
    }
}
