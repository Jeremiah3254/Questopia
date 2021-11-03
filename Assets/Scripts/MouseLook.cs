using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    public bool firstPerson = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && firstPerson == true) {
            firstPerson = false;
            transform.localPosition = new Vector3(0f,2f,-2f);
        }else if (Input.GetKeyDown(KeyCode.E) && firstPerson == false){
            firstPerson = true;
            transform.localPosition = new Vector3(0f,1.157f,0f);
        }
       float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
       float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
       
       xRotation -= mouseY;
       if (firstPerson == true) {
            xRotation = Mathf.Clamp(xRotation, -90f,90f);
       }else if (firstPerson == false) {
            xRotation = Mathf.Clamp(xRotation, 30f,30f);  
       }

       //if (firstPerson == true) {
       
       //}

       transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
       playerBody.Rotate(Vector3.up * mouseX);
       
        
    }
}
