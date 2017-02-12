using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class PlayerController : Player
    {

        // number of jumps available, useful for double jump or multiple jumps
        public int iRemainingJumps;
        private Rigidbody2D player;

        // private field for the current jump time
        private float fJumpHoldTime = 0.0f;
        private bool bContinueJump = true;
        private bool bJumpRelease = false;

        void Awake()
        {
            player = GetComponent<Rigidbody2D>();
        }

        public void HandleHorizontalInput()
        {
            var direction = Input.GetAxis("Horizontal");

            var acceleration = new Vector2((walkAcceleration * direction), 0);

            if (Mathf.Abs(direction) >= 0.05)
            {
                player.velocity += acceleration;
            }

            var magnitude = Mathf.Abs(player.velocity.x);

            if (magnitude <= 0.1f)
            {
                player.velocity = new Vector2(0, player.velocity.y);
            }
            else if (magnitude >= maxWalkSpeed)
            {
                player.velocity = new Vector2((maxWalkSpeed * direction), player.velocity.y);
            }
        }

        public void HandleJumpInput()
        {
            if (bJumpRelease == true || PlayerStateMachine.state == "IN_AIR")
            {
                JumpReleased();
                if (fJumpHoldTime > GetMaxJumpTime())
                {
                    bContinueJump = false;
                }
                if (bContinueJump && bJumpRelease == false)
                {
                    if (Input.GetButton("Jump"))
                    {
                        fJumpHoldTime += Time.deltaTime;
                        if (fJumpHoldTime < 0.1f)
                        {
                            player.velocity += new Vector2(0, initialJumpForce);
                        }
                        else
                        {
                            player.velocity += new Vector2(0, GetJumpAcceleration());
                        }
                    }
                }
            }
        }

        public void ResetJumps()
        {
            iRemainingJumps = GetMaxJumps();
            bJumpRelease = false;
            bContinueJump = true;
            fJumpHoldTime = 0.0f;
        }

        public void DecreaseJumps()
        {
            iRemainingJumps--;
        }

        public void JumpReleased()
        {
            if (Input.GetButtonUp("Jump"))
            {
                bJumpRelease = true;
            }
        }
    }
}
