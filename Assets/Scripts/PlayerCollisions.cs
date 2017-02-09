using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerCollisions : MonoBehaviour
    {
        private PlayerStateMachine psmScript;
        public LayerMask floorLayer;

        void Start()
        {
            floorLayer = LayerMask.NameToLayer("Floor");
            psmScript = GetComponent<PlayerStateMachine>();
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (gameObject.tag == "Player" && coll.gameObject.layer == floorLayer)
            {
                psmScript.bGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D coll)
        {
            if (gameObject.tag == "Player" && coll.gameObject.layer == floorLayer)
            {
                psmScript.bGrounded = false;
            }
        }
    }
}
