using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProtectTheWall
{
    public class TurretController : MonoBehaviour
    {
        public PlayerConfiguration playerConfiguration;

        [SerializeField]
        private float maxRotationDegree;
        [SerializeField]
        private float rotationSpeed;

        private void Start()
        {
            Debug.Log("Bonjour");
            StartCoroutine(nameof(BarrelRotation));
        }

        IEnumerator BarrelRotation()
        {
            Debug.Log("Au revoir");
            float _leftOrRightRotation = 1;
            while (true)
            {
                transform.Rotate(Vector3.forward, _leftOrRightRotation * rotationSpeed * Time.fixedDeltaTime);
                if (transform.rotation.eulerAngles.z > maxRotationDegree && transform.rotation.eulerAngles.z < 360 - maxRotationDegree)
                {
                    _leftOrRightRotation *= -1;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}