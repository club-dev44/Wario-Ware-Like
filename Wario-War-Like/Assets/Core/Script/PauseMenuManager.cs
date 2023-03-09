using UnityEngine;

namespace Core
{

    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject uiPauseMenu;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                activeDesactiveUI();
            }
        }

        public void activeDesactiveUI()
        {
            uiPauseMenu.SetActive(!uiPauseMenu.activeSelf);
        }
    }
}
