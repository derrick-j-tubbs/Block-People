using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerCollisions : MonoBehaviour
    {
        private Player playerScript;
        public LayerMask floorLayer;

        void Start()
        {
            floorLayer = LayerMask.NameToLayer("Floor");
            playerScript = GetComponent<Player>();
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (gameObject.tag == "Player" && coll.gameObject.layer == floorLayer)
            {
                playerScript.bGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D coll)
        {
            if (gameObject.tag == "Player" && coll.gameObject.layer == floorLayer)
            {
                playerScript.bGrounded = false;
            }
        }
    }
}
