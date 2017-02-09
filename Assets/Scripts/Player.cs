using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Player : MonoBehaviour
    {
        #region jumping
        [SerializeField] private int maxJumps = 1;
        [SerializeField] private float initialJumpForce = 5f;
        [SerializeField] private float jumpAcceleration = 0.5f;
        [SerializeField] private float maxJumpTime = 1.5f;
        private bool bGrounded;
        public bool IsGrounded()
        {
            return bGrounded;
        }

        public void SetGrounded(bool grounded)
        {
            bGrounded = grounded;
        }

        public int GetMaxJumps()
        {
            return maxJumps;
        }

        public float GetInitialJumpForce()
        {
            return initialJumpForce;
        }

        public float GetJumpAcceleration()
        {
            return jumpAcceleration;
        }

        public float GetMaxJumpTime()
        {
            return maxJumpTime;
        }
        #endregion
        
        #region walking

        [SerializeField] private float walkAcceleration = 2f;
        [SerializeField] private float maxWalkSpeed = 6f;

        public float GetWalkAcceleration()
        {
            return walkAcceleration;
        }

        public float GetMaxWalkSpeed()
        {
            return maxWalkSpeed;
        }
        #endregion
    }
}
