using Core;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace RedAlert
{
    public class PlayerController : MonoBehaviour
    {
        public LevelsManager levelsManager;
        public PlayerConfiguration playerConfiguration;


        public delegate void diedEventMethod(int index);
        public diedEventMethod diedEvent;

        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float boostUp = 1;
        [SerializeField] private float boostRight = 1;
        [SerializeField] private string tagWall;
        [SerializeField] private string nextLevelTriggerTag;
        [SerializeField] private string inputActionName = "Valider";
        [SerializeField] private Object explosionOnImpact;

        private void FixedUpdate()
        {
            if (playerConfiguration.Input.actions[inputActionName].IsPressed())
            {
                rb.velocity += new Vector2(boostRight, boostUp) * Time.deltaTime;
            }
            transform.rotation = Quaternion.Euler(0, 0, rb.velocity.x * 4);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag.Equals(tagWall))
            {
                Instantiate(explosionOnImpact, transform.position, transform.rotation);
                diedEvent?.Invoke(playerConfiguration.PlayerIndex);
                gameObject.SetActive(false);
            }

            if (col.tag.Equals(nextLevelTriggerTag))
            {
                levelsManager.addLevel();
            }
        }
    }

}