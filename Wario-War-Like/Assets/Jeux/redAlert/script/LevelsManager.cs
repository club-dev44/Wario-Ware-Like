using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace RedAlert
{
    public class LevelsManager : MonoBehaviour
    {
        [SerializeField] private List<Object> niveauxPrefab = new List<Object>();
        Vector3 positionOfNextLevel = Vector3.zero;
        [SerializeField] private CameraPathFollower cameraPathFollower;

        private void Start()
        {
            addLevel();
            addLevel();
            addLevel();
        }

        public void addLevel()
        {
            Object nextLevel = niveauxPrefab[Random.Range(0, niveauxPrefab.Count)];
            GameObject gameObject =
                (GameObject)Instantiate(nextLevel, positionOfNextLevel, Quaternion.Euler(Vector3.zero));
            gameObject.transform.parent = transform;
            positionOfNextLevel = gameObject.GetComponent<LevelProperty>().endPosition.position;
            foreach (Transform waypoint in gameObject.GetComponent<LevelProperty>().waypoints)
            {
                cameraPathFollower.waypoints.Enqueue(waypoint);
            }
        }
    }
}
