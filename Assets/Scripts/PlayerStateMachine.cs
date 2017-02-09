using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerStateMachine : MonoBehaviour
    {
        // enumerator for player states
        private enum PlayerStates
        {
            IDLE,
            WALK,
            ENTER_JUMP,
            IN_AIR
        }

        // state tracker defaults to idle
        private PlayerStates currentState = PlayerStates.IDLE;

        // create a dictionary to contain all the states and the appropriate action to take
        private readonly Dictionary<PlayerStates, Action> playerStateInvoker = new Dictionary<PlayerStates, Action>();

        // private fields required for initialization of state machine
        private Player playerScript;
        private Rigidbody2D player;
        private LayerMask floorLayer;
        private Collider2D playerCol;

        // fields for the purpose of determining whether the character is grounded
        private float fDistToGround;
        public bool bGrounded;

        // private field for the current jump time
        private float fJumpHoldTime = 0.0f;

        // number of jumps available, useful for double jump or multiple jumps
        private int iRemainingJumps;

        // initialization
        void Start()
        {
            // fetch components from unity for use within initialization
            playerScript = GetComponent<Player>();
            player = GetComponent<Rigidbody2D>();
            playerCol = GetComponent<Collider2D>();

            // get the int val of the named layer and ground distance for IsGrounded()
            //floorLayer = LayerMask.GetMask("Floor");
            //fDistToGround = playerCol.bounds.extents.y;

            // get the value for the players max jumps
            iRemainingJumps = playerScript.GetMaxJumps();
            
            // reset number of jumps to 0
            ResetJumps();          

            // Populate the dictionary
            playerStateInvoker.Add(PlayerStates.IDLE, UpdateIDLE);
            playerStateInvoker.Add(PlayerStates.WALK, UpdateWALK);
            playerStateInvoker.Add(PlayerStates.ENTER_JUMP, UpdateENTER_JUMP);
            playerStateInvoker.Add(PlayerStates.IN_AIR, UpdateIN_AIR);
        }

        void Update()
        {
            // check grounded state
            playerStateInvoker[currentState].Invoke();
            //Debug.Log(currentState);
            //Debug.Log("Gounded: " + bGrounded);
            //Debug.Log(iRemainingJumps);
            //Debug.Log(bJumpRelease);
        }

        private void SetState(PlayerStates newState)
        {
                currentState = newState;
        }

        #region idle
        private void UpdateIDLE()
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.05)
            {
                SetState(PlayerStates.WALK);
            } else if (Input.GetButton("Jump"))
            {
                SetState(PlayerStates.ENTER_JUMP);
            } else if (!bGrounded)
            {
                iRemainingJumps--;
                SetState(PlayerStates.IN_AIR);
            }
        }
        #endregion

        #region walk
        private void UpdateWALK()
        {
            HandleHorizontalInput();
            if (!bGrounded)
            {
                iRemainingJumps--;
                SetState(PlayerStates.IN_AIR);
            }
            else if (Input.GetButton("Jump"))
            {
                SetState(PlayerStates.ENTER_JUMP);
            }
            else if (Mathf.Abs(Input.GetAxis("Horizontal")) <= 0.05)
            {
                SetState(PlayerStates.IDLE);
            }
        }
        #endregion


        #region enter jump
        private void UpdateENTER_JUMP()
        {
            if (iRemainingJumps != 0)
            {
                HandleJumpInput();
                HandleHorizontalInput();
                iRemainingJumps--;
                SetState(PlayerStates.IN_AIR);
            }
        }
        #endregion

        #region in air
        
        private void UpdateIN_AIR()
        {
            HandleJumpInput();
            HandleHorizontalInput();
            if (bGrounded)
            {
                ResetJumps();
                if (Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.05)
                {
                    SetState(PlayerStates.WALK);
                }
                else if (Input.GetButton("Jump"))
                {
                    SetState(PlayerStates.ENTER_JUMP);
                } else
                {
                    SetState(PlayerStates.IDLE);
                }
            }
        }
        #endregion

        #region handlers

        //variables to handle speed changes for running and walking
        [SerializeField] private float fAccelerationSpeed;
        [SerializeField] private float fMaxSpeed;

        private void HandleHorizontalInput()
        {
            var direction = Input.GetAxis("Horizontal");

            var acceleration = new Vector2((fAccelerationSpeed * direction), 0);

            if (Mathf.Abs(direction) >= 0.05)
            {
                player.velocity += acceleration;
            }

            var magnitude = Mathf.Abs(player.velocity.x);

            if (magnitude <= 0.1f)
            {
                player.velocity = new Vector2(0, player.velocity.y);
            }
            else if (magnitude >= fMaxSpeed)
            {
                player.velocity = new Vector2((fMaxSpeed * direction), player.velocity.y);
            }
        }

        private bool bContinueJump = true;
        private bool bJumpRelease = false;

        private void HandleJumpInput()
        {
            if (Input.GetButtonUp("Jump"))
            {
                bJumpRelease = true;
            }
            if (fJumpHoldTime > playerScript.GetMaxJumpTime())
            {
                bContinueJump = false;
            }
            if (bContinueJump && bJumpRelease == false)
            {
                if (Input.GetButton("Jump"))
                {
                    fJumpHoldTime += Time.deltaTime;
                    player.velocity += new Vector2(0, playerScript.GetJumpAcceleration());
                }
            }
        }
        #endregion
       
        #region helpers
        //helpers
        private void ResetJumps()
        {
            iRemainingJumps = playerScript.GetMaxJumps();
            bJumpRelease = false;
            bContinueJump = true;
            fJumpHoldTime = 0.0f;
        }
        #endregion
    }
}