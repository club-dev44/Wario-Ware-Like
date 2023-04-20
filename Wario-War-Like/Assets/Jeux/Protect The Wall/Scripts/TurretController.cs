using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProtectTheWall
{
    public class TurretController : MonoBehaviour
    {
        public PlayerConfiguration playerConfiguration;
        public ProtectTheWallManger gameManager;
        public Transform BulletBag;

        [SerializeField]
        private GameObject barrel;
        [SerializeField]
        private float maxRotationDegree;
        [SerializeField]
        private float normalRotationSpeed, slowedRotationSpeed, slowDuration;
        [SerializeField]
        private AnimationCurve magazineCurve;
        [SerializeField]
        private SpriteRenderer bulletDisplay;
        [SerializeField]
        private TextMeshPro scoreDisplay;
        [SerializeField]
        private List<GameObject> magazine = new();

        private int points;
        public int Points
        {
            get => points;
            set
            {
                points = value;
                scoreDisplay.text = points.ToString();
                if (points >= 100)
                {
                    points = 100;
                    gameManager.FinishGame();
                }
            }
        }

        private float currentRotationSpeed;
        private float currentMagazineAbsciss;
        private int currentMagazineIndex;
        private GameObject CurrentBulletToShoot { get => magazine[currentMagazineIndex]; }

        private void Start()
        {
            barrel.GetComponent<SpriteRenderer>().color = playerConfiguration.PlayerColor;
            Points = 0;
            currentRotationSpeed = normalRotationSpeed;
            StartCoroutine(BarrelRotation());
            playerConfiguration.Input.actions["SHOOT"].performed += Shoot; // To subscribe to the action /!\ do not forget to unsubscribe on destroy !!
            StartCoroutine(ImprovingBullet());
        }

        IEnumerator BarrelRotation()
        {
            barrel.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-maxRotationDegree * .5f, maxRotationDegree * .5f));
            float _leftOrRightRotation = 1;
            while (true)
            {
                if (barrel.transform.rotation.eulerAngles.z > maxRotationDegree && barrel.transform.rotation.eulerAngles.z < maxRotationDegree + 10) // add a threshold to avoid blocking 
                    _leftOrRightRotation = -1;
                else if (barrel.transform.rotation.eulerAngles.z < 360 - maxRotationDegree && barrel.transform.rotation.eulerAngles.z > 360 - (maxRotationDegree + 10))
                    _leftOrRightRotation = 1;

                barrel.transform.Rotate(Vector3.forward, _leftOrRightRotation * currentRotationSpeed * Time.fixedDeltaTime);
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
        }

        private void Shoot(InputAction.CallbackContext ctxt)
        {
            StopCoroutine(nameof(SlowRotation));
            BulletBrain bullet = Instantiate(CurrentBulletToShoot, barrel.transform.position + barrel.transform.TransformDirection(Vector3.up), barrel.transform.rotation, BulletBag).GetComponent<BulletBrain>();
            bullet.PlayerOwner = this;
            currentMagazineAbsciss = 0;
            StartCoroutine(nameof(SlowRotation));
        }

        private IEnumerator SlowRotation()
        {
            currentRotationSpeed = slowedRotationSpeed;
            yield return new WaitForSeconds(slowDuration);
            currentRotationSpeed = normalRotationSpeed;
        }

        private IEnumerator ImprovingBullet()
        {
            currentMagazineAbsciss = 0;
            yield return new WaitForSeconds(3);

            while (true)
            {
                currentMagazineAbsciss += Time.deltaTime;
                if (Mathf.FloorToInt(currentMagazineAbsciss) != currentMagazineIndex)
                {
                    currentMagazineIndex = Mathf.FloorToInt(magazineCurve.Evaluate(currentMagazineAbsciss));
                    bulletDisplay.sprite = CurrentBulletToShoot.GetComponent<SpriteRenderer>().sprite;
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        private void OnDestroy()
        {
            playerConfiguration.Input.actions["SHOOT"].performed -= Shoot;
            playerConfiguration.Input.SwitchCurrentActionMap(playerConfiguration.Input.defaultActionMap);
        }
    }
}