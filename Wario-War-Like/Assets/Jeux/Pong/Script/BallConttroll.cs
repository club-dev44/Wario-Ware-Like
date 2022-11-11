using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    
public class BallConttroll : MonoBehaviour
{
    [SerializeField]
    private float borderMinX;

    [SerializeField]
    private float borderMaxX;


    [SerializeField]
    private float borderMinY;

    [SerializeField]
    private float borderMaxY;


    [SerializeField]
    private float speed = 10.0f;


    private float speedX;
    private float speedY;


    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;

   

    private Collider2D player1Collider;
    private Collider2D player2Collider;

    private PlayerControll playerControllPlayer1;
    private PlayerControll playerControllPlayer2;


    [SerializeField]
    private ScoreManagerPong scoreManager;

    [SerializeField]
    private GameManagerPong gameManager;

    [SerializeField]
    private GameObject explosion;



    public List<GameObject> icesGameObjects;
    public List<GameObject> fireGameObjects;


    public bool play = true;

    // Start is called before the first frame update
    void Start()
    {
        player1Collider = player1.GetComponent<Collider2D>();
        player2Collider = player2.GetComponent<Collider2D>();
        playerControllPlayer1 = player1.GetComponent<PlayerControll>();
        playerControllPlayer2 = player2.GetComponent<PlayerControll>();

        icesGameObjects = new List<GameObject>();

        speedX = speed;
        speedY = speed;
    }

    private void FixedUpdate()
    {
        if (!play) return;
        setSpeed(speed * (1 + (0.01f * Time.deltaTime)));

        if (transform.position.x < borderMinX)
        {
            gameManager.player1MissBall();
            return;
        }

        if (transform.position.x > borderMaxX)
        {
            gameManager.player2MissBall();
            return;
        }

        if(transform.position.y < borderMinY)
        {
            speedY = Mathf.Abs(speedY);
        }

        if(transform.position.y > borderMaxY)
        {
            speedY = -Mathf.Abs(speedY);
        }



        transform.position += new Vector3(1.0f, 0.0f, 0.0f) * speedX * Time.deltaTime;
        transform.position += new Vector3(0.0f, 1.0f, 0.0f) * speedY * Time.deltaTime;


    }


    public void setSpeed(float nspeed)
    {
        speed = nspeed;
        speedX = (speedX < 0 ? -1 : 1) *speed;
        speedY= (speedY < 0 ? -1 : 1) * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == player1Collider)
        {
            speedX = Mathf.Abs(speedX);
            playerControllPlayer1.hit();
            explosion.SetActive(true);
        }
        if (collision == player2Collider)
        {
            speedX = -Mathf.Abs(speedX);
            playerControllPlayer2.hit();
            explosion.SetActive(true);
        }

        foreach(GameObject ice in icesGameObjects)
        {
            if(ice.GetComponent<Collider2D>() == collision)
            {
                icesGameObjects.Remove(ice);
                Destroy(ice);
                play = false;
                IEnumerator couroutine = freezeFor1sec();
                StartCoroutine(couroutine);
                break;

            }
        }


        foreach (GameObject fire in fireGameObjects)
        {
            if (fire.GetComponent<Collider2D>() == collision)
            {
                fireGameObjects.Remove(fire);
                Destroy(fire);
                setSpeed(speed * 1.1f);
                break;

            }
        }



    }


    IEnumerator freezeFor1sec()
    {
        yield return new WaitForSeconds(0.5f);
        play = true;
        if((int)Random.Range(0, 10) % 2 == 0)
        {
            speedX = -speedX;
        }
        if ((int)Random.Range(0, 10) % 2 == 0)
        {
            speedY = -speedY;
        }
    }
}
}
