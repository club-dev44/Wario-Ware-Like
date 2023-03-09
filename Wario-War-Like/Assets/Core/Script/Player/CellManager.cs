using TMPro;
using UnityEngine;

namespace Core
{


    public class CellManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public void setScoreAndPlayer(int playerIndex, int playerScore)
        {
            text.SetText("Joueur " + playerIndex + " : " + playerScore);
        }
    }

}