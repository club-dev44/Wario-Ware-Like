using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtectTheWall
{
    public class GenerateGround : MonoBehaviour
    {
        [SerializeField]
        private int cellWidth, cellHeight;
        [SerializeField]
        private GameObject quadPrefab;

        private void Start()
        {
            GenGround();
        }

        private void GenGround()
        {
            for (int i = 0; i < cellHeight; i++)
            {
                for (int j = 0; j < cellWidth; j++)
                {
                    float[] possibleRotations = { 0, 90, 180, 270 };
                    Instantiate(quadPrefab, transform.position + new Vector3(i, j, 0), Quaternion.Euler(0, 0, possibleRotations[Random.Range(0, 4)]), transform);
                }
            }
        }
    }
}