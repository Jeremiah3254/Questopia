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
    Coroutine wandering;
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

    //map size
    int xBorder = 100;
    int zBorder = 100;
    //map size

    //Bound Management
    public GameObject mapInfo;
    bool boundManagementOccurring = false;
    int outOfBounds = 5;
    //Bound Management
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        groundCheck = gameObject.transform.Find("GroundCheck").GetComponent<Transform>();
        xBorder = GameObject.Find("Terrain").GetComponent<MapGeneration>().bottomBorder;
        zBorder = GameObject.Find("Terrain").GetComponent<MapGeneration>().CurrentRow+1;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, collideLayer);

        if (boundManagementOccurring == true) {
            if (moveUp() == false) {
                if (moveDown() == false) {
                    if (moveRight() == false) {
                        if (moveLeft() == false) {
                            boundManagementOccurring = false;
                            wandering = StartCoroutine(startWandering());
                        }
                    }
                }
            }
        }

        if (StartWander == true) {
            wandering = StartCoroutine(startWandering());
        }
        
        //StopCoroutine(routine);
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

        checkBounds();
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
        if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y-0.15f,transform.position.z),transform.TransformDirection(Vector3.forward),1,collideLayer) && jumping == false && isGrounded) {
            jumping = true;
        } else {
            jumping = false;
        }
    }

    public void jumpNow() {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void walkNow() {
        controller.Move(transform.forward * speed * Time.deltaTime);
    }

    public void rotateNow() {
        if (rotateDirection == 1) {
            transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
        }else if (rotateDirection == 2) {
            transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
        }
    }

    public void checkBounds() {
        //Debug.Log("bounds Entered");
        if (boundManagementOccurring == false) {
            if ((transform.position.x < outOfBounds)) {
                //Debug.Log("bound 1");
                boundManagementOccurring = true;
                StartWander = false;
                StopCoroutine(wandering);

            }else if (transform.position.x > xBorder - outOfBounds) {
                //Debug.Log("bound 2"+ (transform.position)+"  "+xBorder+"-"+outOfBounds);
                boundManagementOccurring = true;
                StartWander = false;
                StopCoroutine(wandering);

            }else if (transform.position.z > -outOfBounds) {
                //Debug.Log("bound 3");
                boundManagementOccurring = true;
                StartWander = false;
                StopCoroutine(wandering);

            }else if (transform.position.z < -zBorder + outOfBounds) {
                //Debug.Log("bound 4"+ (transform.position.z)+"  "+zBorder+"+"+outOfBounds);
                boundManagementOccurring = true;
                StartWander = false;
                StopCoroutine(wandering);
                
            }
        }
    }

    public bool rotateToAngle(Vector3 desiredRotation) {
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, desiredRotation, Time.deltaTime);
        if ((transform.rotation.eulerAngles.y >= desiredRotation.y && transform.rotation.eulerAngles.y <= (desiredRotation.y+2f)) || (transform.rotation.eulerAngles.y <= desiredRotation.y && transform.rotation.eulerAngles.y >= (desiredRotation.y-2f))){
            //Debug.Log("true");
            return true;
        }
        //Debug.Log("false"+transform.rotation.eulerAngles.y+" "+desiredRotation.y);
        return false;
    }
    public bool moveUp() {   
        if (transform.position.x < outOfBounds + 3) {
            if (rotateToAngle(new Vector3(0,90,0)) == true) {
                shouldJump();
                controller.Move(transform.forward * speed * Time.deltaTime);
            }
        } else {
            return false;
        }
        return true;
    }

    public bool moveDown() {   
        if (transform.position.x > xBorder - (outOfBounds+3)) {
            if (rotateToAngle(new Vector3(0,270,0))) {
                shouldJump();
                controller.Move(transform.forward * speed * Time.deltaTime);
            }
        } else {
            return false;
        }
        return true;
    }

    public bool moveRight() {   
        if (transform.position.z > -(outOfBounds+3)) {
            if (rotateToAngle(new Vector3(0,180,0))) {
                shouldJump();
                controller.Move(transform.forward * speed * Time.deltaTime);
            }
        } else {
            return false;
        }
        return true;
    }

    public bool moveLeft() {   
        if (transform.position.z < -zBorder + (outOfBounds+3)) {
            if (rotateToAngle(new Vector3(0,0,0))) {
                shouldJump();
                controller.Move(transform.forward * speed * Time.deltaTime);
            }
        } else {
            return false;
        }
        return true;
    }

}
