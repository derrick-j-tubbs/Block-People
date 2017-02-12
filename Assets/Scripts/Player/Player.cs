using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class Player : MonoBehaviour
    {
        public int maxJumps = 1;
        public float initialJumpForce = 5f;
        public float jumpAcceleration = 0.5f;
        public float maxJumpTime = .5f;
        public float walkAcceleration = 2f;
        public float maxWalkSpeed = 6f;

        public bool bGrounded;

        #region jumping
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
