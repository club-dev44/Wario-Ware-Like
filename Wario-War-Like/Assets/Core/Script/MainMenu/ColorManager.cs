using Codice.Client.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{

    /// <summary>
    /// Manage all the colors of the players and the impossibility to have two players with the same color
    /// </summary>
    public class ColorManager : MonoBehaviour
    {
        private List<Color> possibleColors = new();
        private List<Color> usedColors = new();

        /// <summary>
        /// Get a color from the possible colors and not used
        /// </summary>
        /// <param name="currentColor">The color currently used by the player</param>
        /// <param name="nextOne">If the near color is to be the next or previous one</param>
        /// <returns>The new color for the player</returns>
        private Color GetNearColor(Color currentColor, bool nextOne)
        {
            return RandomColor(); // since there is no set of color yet
            int index = possibleColors.IndexOf(currentColor);
            usedColors.Remove(currentColor);
            index += nextOne ? 1 : -1;
            while (usedColors.Contains(possibleColors[index]))
            {
                index += nextOne ? 1 : -1;
            }
            Color color = possibleColors[index];
            usedColors.Add(color);
            //return color;
        }

        /// <summary>
        /// Gives the next color available
        /// </summary>
        /// <param name="currentColor">The current color of the player</param>
        /// <returns>The new color for the player</returns>
        public Color GetNextColor(Color currentColor)
        {
            return GetNearColor(currentColor, true);
        }

        /// <summary>
        /// Gives the previous color available
        /// </summary>
        /// <param name="currentColor">The current color of the player</param>
        /// <returns>The new color for the player</returns>
        public Color GetPreviousColor(Color currentColor)
        {
            return GetNearColor(currentColor, false);
        }

        /// <summary>
        /// To be removed when the decision of how the set of colors will be done
        /// </summary>
        /// <returns></returns>
        private Color RandomColor()
        {
            Color color;
            do
            {
                color = Random.ColorHSV();
            } while (usedColors.Contains(color));
            return color;
        }
    }
}
