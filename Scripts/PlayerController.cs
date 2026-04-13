using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int lives;
    public GameManager gameManager;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    private float playerSpeed;
    private float horizontalInput;
    private float vertictalInput;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerSpeed = 6f;
        lives = 3;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

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
        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
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
        if (transform.position.y >= verticalScreenLimit/2)
        {
            transform.position = new Vector3(transform.position.x, verticalScreenLimit / 2, 0);
        }
        if (transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, -verticalScreenLimit, 0);
        }


        transform.Translate(new Vector3(horizontalInput, vertictalInput, 0) * Time.deltaTime * playerSpeed);
    }

    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
}
