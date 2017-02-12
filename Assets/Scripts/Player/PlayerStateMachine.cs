using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerStateMachine : PlayerController
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
        [HideInInspector]
        public static string state = "IDLE";

        // create a dictionary to contain all the states and the appropriate action to take
        private readonly Dictionary<PlayerStates, Action> playerStateInvoker = new Dictionary<PlayerStates, Action>();

        // private fields required for initialization of state machine
        private PlayerController playerCont;

        // initialization
        void Start()
        {
            // fetch components from unity for use within initialization
            playerCont = GetComponent<PlayerController>();

            // reset number of jumps to 0
            playerCont.ResetJumps();

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
                state = "WALK";
            }
            else if (Input.GetButton("Jump"))
            {
                SetState(PlayerStates.ENTER_JUMP);
                state = "ENTER_JUMP";
            }
            else if (!bGrounded)
            {
                playerCont.DecreaseJumps();
                SetState(PlayerStates.IN_AIR);
                state = "IN_AIR";
            }
        }
        #endregion

        #region walk
        private void UpdateWALK()
        {
            playerCont.HandleHorizontalInput();
            if (!bGrounded)
            {
                playerCont.DecreaseJumps();
                SetState(PlayerStates.IN_AIR);
                state = "IN_AIR";
            }
            else if (Input.GetButton("Jump"))
            {
                SetState(PlayerStates.ENTER_JUMP);
                state = "ENTER_JUMP";
            }
            else if (Mathf.Abs(Input.GetAxis("Horizontal")) <= 0.05)
            {
                SetState(PlayerStates.IDLE);
                state = "IDLE";
            }
        }
        #endregion


        #region enter jump
        private void UpdateENTER_JUMP()
        {
            if (playerCont.iRemainingJumps != 0)
            {
                playerCont.HandleJumpInput();
                playerCont.HandleHorizontalInput();
                playerCont.DecreaseJumps();
                SetState(PlayerStates.IN_AIR);
                state = "IN_AIR";
            }
        }
        #endregion

        #region in air        
        private void UpdateIN_AIR()
        {
            playerCont.HandleJumpInput();
            playerCont.HandleHorizontalInput();
            if (bGrounded)
            {
                playerCont.ResetJumps();
                if (Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.05)
                {
                    SetState(PlayerStates.WALK);
                    state = "WALK";
                }
                else if (Input.GetButton("Jump"))
                {
                    SetState(PlayerStates.ENTER_JUMP);
                    state = "ENTER_JUMP";
                }
                else
                {
                    SetState(PlayerStates.IDLE);
                    state = "IDLE";
                }
            }
        }
        #endregion

    }
}