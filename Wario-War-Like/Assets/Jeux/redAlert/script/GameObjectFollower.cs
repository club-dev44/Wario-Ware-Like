using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedAlert
{
    public class GameObjectFollower : MonoBehaviour
    {

        private GameObject objectToFollow;
        private bool objectToFollowAsBeenSet = false;
        public void setObjectToFollow(GameObject objectToFollow) {
            this.objectToFollow = objectToFollow;
            objectToFollowAsBeenSet = true;
        }
        // Update is called once per frame
        void Update() {
            if (objectToFollowAsBeenSet) transform.position = objectToFollow.transform.position;
        }
    
    }
    
}
