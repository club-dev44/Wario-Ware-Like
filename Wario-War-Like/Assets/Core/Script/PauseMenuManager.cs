using UnityEngine;

namespace Core
{

    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject uiPauseMenu;

        // Meh faudrait changer ça je pense
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EnableDisableUI();
            }
        }

        public void EnableDisableUI()
        {
            uiPauseMenu.SetActive(!uiPauseMenu.activeSelf);
        }
    }
}
