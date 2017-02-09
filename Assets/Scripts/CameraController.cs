using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject cameraObject;
    public float smoothSpeed;
    public GameObject maxXY;
    public GameObject minXY;
    private Vector3 specificVector;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FollowPlayer();
        //Debug.Log(specificVector.z);
    }

    void FollowPlayer()
    {
        if (cameraObject.transform.position.y < transform.position.y - 2)
        {
            specificVector = new Vector3(transform.position.x, transform.position.y - 2, -10f);
            ClampCamera();
            cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, specificVector, smoothSpeed * Time.deltaTime);
        }
        else if (cameraObject.transform.position.y > transform.position.y - 2)
        {
            specificVector = new Vector3(transform.position.x, transform.position.y + 2, -10f);
            ClampCamera();
            cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, specificVector, smoothSpeed * Time.deltaTime);
        }
        else
        {
            specificVector = new Vector3(transform.position.x, cameraObject.transform.position.y, -10f);
            ClampCamera();
            cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, specificVector, smoothSpeed * Time.deltaTime);
        }
    }
}
