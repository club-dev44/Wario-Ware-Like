using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelProperty : MonoBehaviour
{
    [SerializeField] public Transform startPosition;
    [SerializeField] public Transform endPosition;
    [SerializeField] public List<Transform> waypoints;
    
}
