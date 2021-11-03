using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    const float animationSmoothTime =.1f;
    CharacterController controller;
    float speed;
    public float speedPercents;

    //NavMeshAgent agent;
    Animator animator; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = GetComponent<PlayerMovement>().speed;
        //agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = GetComponent<PlayerMovement>().speed;
        //float overallSpeed = controller.velocity.magnitude;
        speedPercents = speed / 20f;
        animator.SetFloat("speedPercent", speedPercents, animationSmoothTime,Time.deltaTime);
    }
}
