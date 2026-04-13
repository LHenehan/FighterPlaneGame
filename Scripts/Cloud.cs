using UnityEngine;
public class Cloud : MonoBehaviour
{

    private float speed;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        transform.localScale = transform.localScale * Random.Range(0.1f, 0.6f);
        transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Random.Range(0.1f, 0.7f));
        speed = Random.Range(3f, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 3f);
        if (transform.position.y < -gameManager.verticalScreenLimit)
        {
            transform.position = new Vector3(Random.Range(-gameManager.horizontalScreenLimit, gameManager.horizontalScreenLimit), gameManager.verticalScreenLimit * 1.2f, 0f);
        }
    }
}