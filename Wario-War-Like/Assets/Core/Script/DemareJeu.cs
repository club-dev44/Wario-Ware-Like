using UnityEngine;


namespace Core
{



    public class DemareJeu : MonoBehaviour
    {
        private GameManager gameManager;

        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameManager.Instance;
        }


        public void DemarerJeu()
        {
            gameManager.EndMyGame(new[] { 5, 0 });
        }
    }
}
