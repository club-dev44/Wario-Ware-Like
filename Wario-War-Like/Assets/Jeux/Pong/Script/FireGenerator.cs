using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    

public class FireGenerator : MonoBehaviour
{
    [SerializeField]
    private Object firePrefab;

    [SerializeField]
    private BallConttroll ballConttroll;

    [SerializeField]
    private float xMin = -5;

    [SerializeField]
    private float xMax = 5;

    [SerializeField]
    private float yMin = -5;

    [SerializeField]
    private float yMax = 5;

    public void Start()
    {
        IEnumerator enumerator = generateIceEvery5sec();
        StartCoroutine(enumerator);
    }



    IEnumerator generateIceEvery5sec()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4.0f, 6.0f));

            float x = Random.Range(xMin, xMax);
            float y = Random.Range(yMin, yMax);

            GameObject iceGameObject = (GameObject)Instantiate(firePrefab, new Vector3(x, y, 0), Quaternion.identity);
            ballConttroll.fireGameObjects.Add(iceGameObject);

        }
    }
}
}
