using UnityEngine;

namespace Core
{
    public class RevenirMenu : MonoBehaviour
    {
        private GameManager gameManager;

        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameManager.Instance;
        }

        public void revenirMenu()
        {
            gameManager.SceneMenu();
        }

    }
}
