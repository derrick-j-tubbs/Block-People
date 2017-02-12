using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class CameraController : MonoBehaviour
    {
        public Camera cameraObject;
        public GameObject player;
        public float smoothSpeed;

        private Transform playerXform;

        // public fields for game objects representing room bounds
        public GameObject maxXYObj;
        public GameObject minXYObj;
        private GameObject testMaxObj; //private feild to see if room bounds have changed
                                       // private vector2's used for the mathematical implimentation of 
        private Vector2 maxXY;
        private Vector2 minXY;

        private Vector3 playerPosition;
        private Vector3 prevPlayerPosition;
        private Vector3 cameraPosition;
        private Vector3 screenSize;


        // Use this for initialization
        void Awake()
        {
            GenerateRoomBounds();

            prevPlayerPosition = player.transform.position;
        }

        void LateUpdate()
        {
            CameraUpdate();
        }

        /**
         * Function to change the roombounds upon entry of a new doorway.
         * Function takes two game objects for min and max, sets a private vector2 to those 
         * values adjusted for half the size of the viewport. To set the external bounds of the 
         */
        void GenerateRoomBounds()
        {
            //using the position of gameobjects maxXY and minXY subtract or add 1/2 the viewport height and width to correct
            //the camera clamp locations
            maxXY = maxXYObj.transform.position + new Vector3(-9, -5, 0);
            minXY = minXYObj.transform.position + new Vector3(9, 5, 0);
            testMaxObj = maxXYObj;
        }

        bool RoomChange()
        {
            return maxXYObj != testMaxObj;
        }

        void CameraUpdate()
        {
            playerPosition = player.transform.position;

            if (playerPosition != prevPlayerPosition)
            {
                cameraPosition = cameraObject.transform.position;

                Vector3 playerPosDiff = playerPosition - prevPlayerPosition;

                cameraPosition += playerPosDiff;

                JumpBounds();
                ClampCamera();
                PositionUpdate();
            }

            prevPlayerPosition = playerPosition;
        }

        void JumpBounds()
        {
            if (cameraObject.transform.position.y < transform.position.y - 2)
            {
                cameraPosition = new Vector3(transform.position.x, transform.position.y - 2, -10f);
            }
            else if (cameraObject.transform.position.y > transform.position.y - 2)
            {
                cameraPosition = new Vector3(transform.position.x, transform.position.y + 2, -10f);
            }
            else
            {
                cameraPosition = new Vector3(transform.position.x, cameraObject.transform.position.y, -10f);
            }
        }

        void ClampCamera()
        {
            //use clamp to constrain the cameras movements to create a limit for where the camera can go
            cameraPosition.x = Mathf.Clamp(cameraPosition.x, minXY.x, maxXY.x);
            cameraPosition.y = Mathf.Clamp(cameraPosition.y, minXY.y, maxXY.y);
        }

        void PositionUpdate()
        {
            //use lerp to smooth the transitions of the cameras movement
            cameraPosition = Vector2.Lerp(cameraObject.transform.position, cameraPosition, smoothSpeed * Time.deltaTime);
            cameraPosition.z = -10f;
            cameraObject.transform.position = cameraPosition;
        }
    }
}