using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Pong
{


    public class iceGenerator : MonoBehaviour
    {
        [SerializeField]
        private Object iceprefab;

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

        [SerializeField] private GameManagerPong gameManagerPong;

        private void Awake()
        {
            gameManagerPong.gameStart += () =>
            {
                IEnumerator enumerator = generateIceEvery5sec();
                StartCoroutine(enumerator);
            };
        }




        IEnumerator generateIceEvery5sec()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(4.0f, 6.0f));

                float x = Random.Range(xMin, xMax);
                float y = Random.Range(yMin, yMax);

                GameObject iceGameObject = (GameObject)Instantiate(iceprefab, new Vector3(x, y, 0), Quaternion.identity);
                ballConttroll.icesGameObjects.Add(iceGameObject);

            }
        }
    }
}
