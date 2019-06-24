using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private float movementSpeed = 10f;
    [SerializeField]
    private float rapidSpeed = 30f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float fly = 0f;
        fly += Input.GetKey(KeyCode.Space) ? 1f : 0f;
        fly += Input.GetKey(KeyCode.C) ? -1f : 0f;
        Vector3 delta = transform.right * h + transform.up * fly + transform.forward * v;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            delta *= rapidSpeed * Time.deltaTime;
        }
        else
        {
            delta *= movementSpeed * Time.deltaTime;
        }

        /*float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * turnSpeed;
        float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * turnSpeed;*/
        

        transform.position = transform.position + delta;
        //transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
    }
}
