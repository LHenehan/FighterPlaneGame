using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public const float basePlayerSpeed = 6f;

    public GameManager gameManager;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;

    

    private float playerSpeed;
    private float horizontalInput;
    private float vertictalInput;

    public int lives;
    public int weaponType;

    public bool shieldActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        lives = 3;
        weaponType = 1;
        playerSpeed = basePlayerSpeed;
        shieldActive = false;
        
        gameManager.ChangeLivesText(lives);
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
        Movement();
    }

    public void LoseALife()
    {
        if (shieldActive)
        {
            shieldActive = false;
            shieldPrefab.SetActive(false);
        }
        else
        {
            lives--;
            gameManager.ChangeLivesText(lives);
        }

        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.GameOver();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 1:
                    playerSpeed = basePlayerSpeed * 2;
                    gameManager.ManagePowerupText(1);
                    thrusterPrefab.SetActive(true);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    weaponType = 2;
                    gameManager.ManagePowerupText(2);
                    StartCoroutine(WeaponPowerDown());
                    break;
                case 3:
                    weaponType = 3;
                    gameManager.ManagePowerupText(3);
                    StartCoroutine(WeaponPowerDown());
                    break;
                case 4:
                    shieldActive = true;
                    shieldPrefab.SetActive(true);
                    gameManager.ManagePowerupText(4);
                    break;

            }
        }
        else if (whatDidIHit.tag == "Coin")
        {
            gameManager.AddScore(1);
            gameManager.PlaySound(3);
            Destroy(whatDidIHit.gameObject);
        }
        else if (whatDidIHit.tag == "Life")
        {
            if (lives < 3)
            {
                lives++;
                gameManager.ChangeLivesText(lives);
            }
            else
            {
                gameManager.AddScore(1);
            }
            gameManager.PlaySound(4);
            Destroy(whatDidIHit.gameObject);
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5f);
        playerSpeed = basePlayerSpeed;
        thrusterPrefab.SetActive(false);
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(5f);
        weaponType = 1;
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        vertictalInput = Input.GetAxis("Vertical");
        float horizontalScreenLimit = gameManager.horizontalScreenLimit;
        float verticalScreenLimit = gameManager.verticalScreenLimit;

        if(transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        if (transform.position.y >= .5f)
        {
            transform.position = new Vector3(transform.position.x, .5f, 0);
        }
        if (transform.position.y <= -3.3f)
        {
            transform.position = new Vector3(transform.position.x, -3.3f, 0);
        }


        transform.Translate(new Vector3(horizontalInput, vertictalInput, 0) * Time.deltaTime * playerSpeed);
    }

    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (weaponType)
            {
                case 1:
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.Euler(0, 0, 45));
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(0, 0, -45));
                    break;
            }
        }
    }
}
