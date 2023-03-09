using Core;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pong
{

    public class PlayerControll : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private float max = 3.5f;
        [SerializeField] private float min = -3.5f;
        [SerializeField] private Color hitColor;
        [SerializeField] private Color baseColor;
        [SerializeField] private GameManagerPong gameManagerPong;
        [SerializeField] private int playerIndex;
        [SerializeField] private Animation animationFadeOnHit;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [CanBeNull] private PlayerInput playerInput;

        public const string UPINPUTACTION = "Up";
        public const string DOWNINPUTACTION = "Down";

        void Start()
        {
            spriteRenderer.color = baseColor;
            gameManagerPong.gameStart += GameManagerPongOngameStart;
        }

        private void GameManagerPongOngameStart()
        {
            playerInput = PlayerConfigurationManager.Instance.PlayerConfigurations[playerIndex].Input;
        }

        // Update is called once per frame
        void Update()
        {
            if (playerInput == null) return;
            if (playerInput.actions[DOWNINPUTACTION].IsPressed() && transform.position.y > min)
            {
                transform.position += Vector3.down * (speed * Time.deltaTime);
            }

            if (playerInput.actions[UPINPUTACTION].IsPressed() && transform.position.y < max)
            {
                transform.position += Vector3.up * (speed * Time.deltaTime);
            }
        }

        internal void hit()
        {
            spriteRenderer.color = hitColor;
            goToBaseColor();
            animationFadeOnHit.Play();
        }

        IEnumerator goToBaseColor()
        {
            yield return new WaitForSeconds(2);
            spriteRenderer.color = baseColor;
        }
    }
}
