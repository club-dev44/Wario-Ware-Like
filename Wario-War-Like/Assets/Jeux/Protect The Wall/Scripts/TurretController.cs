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
        private GameObject bulletPrefab;
        [SerializeField]
        private float maxRotationDegree;
        [SerializeField]
        private float normalRotationSpeed, slowedRotationSpeed, slowDuration;
        [SerializeField]
        private float bulletPower;

        private float currentRotationSpeed;

        private void Start()
        {
            StartCoroutine(nameof(BarrelRotation));
            playerConfiguration.Input.actions["SHOOT"].performed += Shoot;
        }

        IEnumerator BarrelRotation()
        {
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
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.TransformDirection(Vector3.up), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(bulletPower * Vector3.up));
            StartCoroutine(nameof(SlowRotation));
        }

        private IEnumerator SlowRotation()
        {
            currentRotationSpeed = slowedRotationSpeed;
            yield return new WaitForSeconds(slowDuration);
            currentRotationSpeed = normalRotationSpeed;
        }
    }
}