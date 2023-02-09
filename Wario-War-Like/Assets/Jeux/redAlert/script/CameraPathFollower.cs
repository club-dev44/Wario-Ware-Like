using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RedAlert
{

    public class CameraPathFollower : MonoBehaviour
    {
        public Queue<Transform> waypoints = new Queue<Transform>();

        private Transform waypointTarget;

        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float bonusSpeed = 1.0f;
        public List<Transform> players = new List<Transform>();
        [SerializeField] private float offset = 2.0f;

        private void Update() {
            if (waypointTarget != null) {
                goToWaypoint();
            } else if (waypoints.Count > 0) {
                waypointTarget = waypoints.Dequeue();
            }
        }


        private void goToWaypoint() {
            Transform player = players[0];
            foreach (Transform transform in players) {
                if (transform.position.y < player.position.y) player = transform;
            }

            if (player.transform.position.y + offset > transform.position.y) {
                speed -= bonusSpeed * Math.Abs(player.transform.position.y + offset - transform.position.y) *
                         Time.deltaTime;
            } else {
                speed += bonusSpeed * Math.Abs(player.transform.position.y + offset - transform.position.y) *
                         Time.deltaTime;
            }

            if (speed < 1) {
                speed = 1;
            }

            transform.position =
                Vector3.MoveTowards(transform.position, waypointTarget.position, speed * Time.deltaTime);
            if (transform.position == waypointTarget.position && waypoints.Count > 0) {
                waypointTarget = waypoints.Dequeue();
            }
        }
    }
}
