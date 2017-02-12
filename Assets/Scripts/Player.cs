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
        [SerializeField] protected int maxJumps = 1;
        [SerializeField] protected float initialJumpForce = 5f;
        [SerializeField] protected float jumpAcceleration = 0.5f;
        [SerializeField] protected float maxJumpTime = 1.5f;
        protected bool bGrounded;

        public bool IsGrounded()
        {
            return bGrounded;
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

        [SerializeField] protected float walkAcceleration = 2f;
        [SerializeField] protected float maxWalkSpeed = 6f;

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
