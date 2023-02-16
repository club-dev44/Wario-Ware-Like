using Core;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProtectTheWall
{
    public class TurretController : MonoBehaviour
    {
        public PlayerConfiguration playerConfiguration;
        public ProtectTheWallManger gameManager;

        [SerializeField]
        private GameObject barrel;
        [SerializeField]
        private float maxRotationDegree;
        [SerializeField]
        private float normalRotationSpeed, slowedRotationSpeed, slowDuration;
        [SerializeField]
        private AnimationCurve magazineCurve;
        [SerializeField]
        private List<GameObject> magazine = new();

        private int points;
        public int Points
        {
            get => points;
            set
            {
                points = value;
                if (points >= 100)
                {
                    gameManager.FinishGame();
                }
            }
        }

        private float currentRotationSpeed;
        private float currentMagazineAbsciss;
        private GameObject currentBulletToShoot;

        private void Start()
        {
            currentRotationSpeed = normalRotationSpeed;
            StartCoroutine(BarrelRotation());
            playerConfiguration.Input.actions["SHOOT"].performed += Shoot;
            StartCoroutine(ImprovingBullet());
        }

        IEnumerator BarrelRotation()
        {
            barrel.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-maxRotationDegree * .5f, maxRotationDegree * .5f));
            float _leftOrRightRotation = 1;
            while (true)
            {
                if (barrel.transform.rotation.eulerAngles.z > maxRotationDegree && barrel.transform.rotation.eulerAngles.z < 360 - maxRotationDegree)
                {
                    _leftOrRightRotation *= -1;
                }
                barrel.transform.Rotate(Vector3.forward, _leftOrRightRotation * currentRotationSpeed * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        private void Shoot(InputAction.CallbackContext ctxt)
        {
            StopCoroutine(nameof(SlowRotation));
            BulletBrain bullet = Instantiate(currentBulletToShoot, barrel.transform.position + barrel.transform.TransformDirection(Vector3.up), barrel.transform.rotation).GetComponent<BulletBrain>();
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

            while(true)
            {
                currentMagazineAbsciss += Time.deltaTime;

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}