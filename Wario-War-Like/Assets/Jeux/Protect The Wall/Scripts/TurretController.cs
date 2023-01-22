using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProtectTheWall
{
    public class TurretController : MonoBehaviour
    {
        public PlayerConfiguration playerConfiguration;

        [SerializeField]
        private GameObject basicBulletPrefab;
        [SerializeField]
        private float maxRotationDegree;
        [SerializeField]
        private float normalRotationSpeed, slowedRotationSpeed, slowDuration;

        private float currentRotationSpeed;
        private Queue<GameObject> magazine;

        private void Start()
        {
            magazine = new Queue<GameObject>();
            currentRotationSpeed = normalRotationSpeed;
            StartCoroutine(nameof(BarrelRotation));
            playerConfiguration.Input.actions["SHOOT"].performed += Shoot;
        }

        IEnumerator BarrelRotation()
        {
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(-maxRotationDegree / 2, maxRotationDegree / 2));
            float _leftOrRightRotation = 1;
            while (true)
            {
                transform.Rotate(Vector3.forward, _leftOrRightRotation * currentRotationSpeed * Time.fixedDeltaTime);
                if (transform.rotation.eulerAngles.z > maxRotationDegree && transform.rotation.eulerAngles.z < 360 - maxRotationDegree)
                {
                    _leftOrRightRotation *= -1;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        private void Shoot(InputAction.CallbackContext ctxt)
        {
            StopCoroutine(nameof(SlowRotation));
            GameObject _bullet = magazine.Count > 0 ? magazine.Dequeue() : basicBulletPrefab;
            Instantiate(_bullet, transform.position + transform.TransformDirection(Vector3.up), Quaternion.Euler(transform.TransformDirection(Vector3.right)));
            StartCoroutine(nameof(SlowRotation));
        }

        private IEnumerator SlowRotation()
        {
            currentRotationSpeed = slowedRotationSpeed;
            yield return new WaitForSeconds(slowDuration);
            currentRotationSpeed = normalRotationSpeed;
        }

        public void AddBulletToMagazine(GameObject bulletPrefab)
        {
            magazine.Enqueue(bulletPrefab);
        }
    }
}