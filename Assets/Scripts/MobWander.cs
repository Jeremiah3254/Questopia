using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(CharacterController))]
public class MobWander : MonoBehaviour
{
    public CharacterController controller;
    public LayerMask collideLayer;
    [Range(0,100)] public float speed;
    public float gravity = -9.18f;
    public float jumpHeight = 3f;
    Vector3 velocity;
    int rotateDirection = 1;
    float rotationSpeed = 100f;
    //IEnumerators
    IEnumerator wandering;
    //IEnumerators
    // Boolean Actions
    bool StartWander = true;
    bool moving = false;
    bool jumping = false;
    bool rotating = false;
    // Boolean Actions
    bool isGrounded;
    public Transform groundCheck;
    float groundDistance = 0.4f;
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        groundCheck = gameObject.transform.Find("GroundCheck").GetComponent<Transform>();
        //wandering = startWandering();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, collideLayer);

        if (StartWander == true) {
            StartCoroutine(startWandering());
        }

        if (moving == true) {
            shouldJump();
        }

        if (rotating == true) {
            rotateNow();
        }

        if (isGrounded && jumping == true) {
            jumpNow();
            jumping = false;
        }
        if (moving == true) {
            walkNow();
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator startWandering() {
        int walkTime = Random.Range(3,5);
        int rotateYield = Random.Range(1,4);
        int rotateTime = Random.Range(2,4);

        StartWander = false;
        yield return new WaitForSeconds(walkTime);
        moving = true;
        yield return new WaitForSeconds(walkTime);
        moving = false;
        yield return new WaitForSeconds(rotateYield);
        if (rotateDirection == 1) {
            rotateDirection = 2;
            rotating = true;
            yield return new WaitForSeconds(rotateTime);
            rotating = false;
        } else if (rotateDirection == 2) {
            rotateDirection = 1;
            rotating = true;
            yield return new WaitForSeconds(rotateTime);
            rotating = false;
        }
        StartWander = true;
    }
    public void shouldJump() {
        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),1,collideLayer) && jumping == false && isGrounded) {
            jumping = true;
        } else {
            jumping = false;
        }
    }

    public void jumpNow() {
        Debug.Log("jumping");
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void walkNow() {
        Debug.Log("walking");
        controller.Move(transform.forward * speed * Time.deltaTime);
    }

    public void rotateNow() {
        Debug.Log("rotating");
        if (rotateDirection == 1) {
            transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
        }else if (rotateDirection == 2) {
            transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
        }
    }

}
