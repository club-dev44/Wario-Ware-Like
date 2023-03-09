using System.Collections;
using UnityEngine;

namespace Core
{


    public class CountDownAnimation : MonoBehaviour
    {

        [SerializeField] private GameObject animGameObject;
        [SerializeField] private float animationDuration = 3.0f;

        public void playAnimation()
        {
            StartCoroutine(nameof(animationEnumerator));
        }

        IEnumerator animationEnumerator()
        {
            animGameObject.SetActive(true);
            yield return new WaitForSeconds(animationDuration);
            animGameObject.SetActive(false);
        }


    }
}
