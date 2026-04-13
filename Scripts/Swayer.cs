using UnityEngine;

public class SwingingEnemy : MonoBehaviour
{

    private int horizontalSpeed = 1;
    private int swapTimer = 0;
    private bool is_goingLeft = false;

    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(horizontalSpeed, -1, 0) * Time.deltaTime * 3f);

        if (swapTimer >= 32 && swapTimer <= 148)
        {
            horizontalSpeed = 2;

            if (swapTimer >= 64 && swapTimer <= 116)
            {
                horizontalSpeed = 3;
            }
            
            if (is_goingLeft)
            {
                horizontalSpeed *= -1;
            }
        }
        else
        {
            horizontalSpeed = 1;

            if (is_goingLeft)
            {
                horizontalSpeed *= -1;
            }
        }

        if (swapTimer > 180)
        {
            swapTimer = 0;
            is_goingLeft = !is_goingLeft;
        }

        swapTimer++;

    }

}
