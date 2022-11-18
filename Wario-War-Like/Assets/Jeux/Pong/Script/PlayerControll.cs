using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pong
{
    
public class PlayerControll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string keyUp;
    [SerializeField]
    private string keyDown;


    [SerializeField]
    private float speed = 10;


    [SerializeField]
    private float max = 3.5f;

    [SerializeField]
    private float min = -3.5f;

    [SerializeField]
    private Color hitColor;

    [SerializeField]
    private Color baseColor;

    [SerializeField] private GameManagerPong gameManagerPong;

    [SerializeField] private int playerIndex;
    
    
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.color = baseColor;
        gameManagerPong.gameStart += GameManagerPongOngameStart;
    }

    private void GameManagerPongOngameStart() {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keyDown) && transform.position.y > min)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKey(keyUp) && transform.position.y < max)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }



    }

    internal void hit()
    {
        spriteRenderer.color = hitColor;
        goToBaseColor();
        this.gameObject.GetComponent<Animation>().Play();
    }

    IEnumerator goToBaseColor()
    {
        yield return new WaitForSeconds(2);
        spriteRenderer.color = baseColor;
    }
}
}
