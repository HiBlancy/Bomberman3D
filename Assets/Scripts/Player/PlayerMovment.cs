using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovment : MonoBehaviour
{
    [SerializeField] float walkingSpeed = 3f;

    [HideInInspector] public CharacterController characterController;

    [HideInInspector] public Vector3 moveDirection = Vector3.zero;

    [HideInInspector] public bool Moving;
    [HideInInspector] public float vertical;
    [HideInInspector] public float horizontal;

    void Start()
    {
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    void Update()
    {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            vertical = walkingSpeed * Input.GetAxis("Vertical");
            horizontal = walkingSpeed * Input.GetAxis("Horizontal");

            moveDirection = (forward * vertical) + (right * horizontal);

            characterController.Move(moveDirection * Time.deltaTime);
            Moving = horizontal < 0 || vertical < 0 || horizontal > 0 || vertical > 0 ? true : false;
        }
    }