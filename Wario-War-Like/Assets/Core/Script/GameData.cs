using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string sceneName;
    [TextArea]
    public string consigne;
    [Tooltip("chaque élément du tableau correspond à un nombre de joueur possible pour le jeu")]
    public int[] nbJoueurs;
}
