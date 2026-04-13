using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cloudPrefab;

    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;

    public float horizontalScreenLimit;
    public float verticalScreenLimit;

    //public TextMeshProUGUI livesText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        horizontalScreenLimit = 10f;
        verticalScreenLimit = 5.5f;

        CreateSky();

        //Function name as string, delay, interval
        InvokeRepeating("CreateEnemyOne", 1f, 2f);
        InvokeRepeating("CreateEnemyTwo", 2f, 4f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenLimit, horizontalScreenLimit), Random.Range(-verticalScreenLimit, verticalScreenLimit), 0f), Quaternion.identity);
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

    public void ChangeLivesText(int currentLives)
    {
        //livesText = "lives " + curentLives;
    }
}
