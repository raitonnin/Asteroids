using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;


    public ParticleSystem explosion;


    public float respawnInvulnerability = 3.0f;

        public GameObject gameOverUI;

    public Text scoreText;

    public int score { get; private set; }

    public Text livesText;
    public int lives { get; private set; }

    public float smallAsteroid  =  .75f;
    public float mediumAsteroid =  1.2f;
   
       private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return)) {
            NewGame();
        }
    }

    public void NewGame()
    {
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        for (int i = 0; i < asteroids.Length; i++) {
            Destroy(asteroids[i].gameObject);
        }

        gameOverUI.SetActive(false);

        SetScore(0);
        SetLives(3);
        Respawn();
    }
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();

        if (asteroid.size < smallAsteroid){
            SetScore(score + 100);
        } else if (asteroid.size < mediumAsteroid) {
            SetScore(score + 50);
        } else {
            SetScore(score + 25);
        }
    }

    public void PlayerDied()
    {
        explosion.transform.position = player.transform.position;
        explosion.Play();
        SetLives(lives -1 );

        if (this.lives <= 0)
        {
            GameOver();
        } else {
            Invoke(nameof(Respawn), player.respawnDelay);
        }
    }
    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
    }
        private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }
}
