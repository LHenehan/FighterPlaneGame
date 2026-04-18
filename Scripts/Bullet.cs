using UnityEngine;

public class Bullet : MonoBehaviour
{

    private GameManager gameManager;

    private float bulletSpeed;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bulletSpeed = 8f;
    }

    void Update()
    {
        //floats need an f by it
        transform.Translate(Vector3.up * Time.deltaTime * bulletSpeed);

        if (transform.position.y > gameManager.verticalScreenLimit * 1.1f)
        {
            Destroy(this.gameObject);
        }
    }
}
