using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedAlert
{
    public class LevelProperty : MonoBehaviour
    {
        [SerializeField] public Transform endPosition;
        [SerializeField] public List<Transform> waypoints;
    }
}
