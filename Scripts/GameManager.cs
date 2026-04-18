using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject cloudPrefab;
    public GameObject gameOverMenu;
    public GameObject powerUpPrefab;
    public GameObject audioPlayer;

    public AudioClip powerUpSound;
    public AudioClip powerDownSound;


    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerUpText;

    public float horizontalScreenLimit;
    public float verticalScreenLimit;

    public int score;
    public int cloudMove;

    private bool gameOver;

    void Start()
    {
        horizontalScreenLimit = 10f;
        verticalScreenLimit = 5.5f;

        score = 0;
        cloudMove = 1;

        gameOver = false;

        AddScore(0);
        powerUpText.text = "No power ups yet!";

        Instantiate(playerPrefab, transform.position, Quaternion.identity);

        //Function name as string, delay, interval
        InvokeRepeating("CreateEnemyOne", 1f, 2f);
        InvokeRepeating("CreateEnemyTwo", 2f, 4f);

        CreateSky();

        StartCoroutine(SpawnPowerup());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenLimit, horizontalScreenLimit) * .9f, verticalScreenLimit, 0f), Quaternion.identity);
    }

    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-horizontalScreenLimit, horizontalScreenLimit) * .8f, verticalScreenLimit, 0f), Quaternion.identity);
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenLimit, horizontalScreenLimit), Random.Range(-verticalScreenLimit, verticalScreenLimit), 0f), Quaternion.identity);
        }
    }

    void CreatePowerup()
    {
        //Instantiate(powerUpPrefab, new Vector3(Random.Range(-horizontalScreenLimit * .8f, horizontalScreenLimit * .8f), Random.Range(-verticalScreenLimit * .8f, verticalScreenLimit * .8f), 0f), Quaternion.identity);
        Instantiate(powerUpPrefab, new Vector3(Random.Range(-horizontalScreenLimit * .8f, horizontalScreenLimit * .8f), Random.Range(-3.3f, 0.5f), 0f), Quaternion.identity);
    }

    IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(3, 5);
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }

    public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerUpText.text = "Speed!";
                break;
            case 2:
                powerUpText.text = "Double Weapon!";
                break;
            case 3:
                powerUpText.text = "Triple Weapon!";
                break;
            case 4:
                powerUpText.text = "Shield!";
                break;
            default:
                powerUpText.text = "No power ups yet!";
                break;
        }
    }

    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerUpSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerDownSound);
                break;
        }
    }

    public void AddScore(int earnedScore)
    {
        score += earnedScore;
        scoreText.text = "Score: " + score;
    }

    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "lives " + currentLives;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        gameOver = true;
    }

}
