using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerJump : MonoBehaviour
{
    [HideInInspector] public CharacterController characterController;
   // float InstallCroughHeight;
    [SerializeField] float jumpSpeed = 6f;
    [SerializeField] float gravity = 20f;
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    //    InstallCroughHeight = characterController.height;
    }

    void Update()
    {
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        float movementDirectionY = moveDirection.y;

        if (Input.GetKeyDown (KeyCode.Space) && characterController.isGrounded) //No detecta el characterController
        {
            moveDirection.y = jumpSpeed;
            Debug.Log("Espacio presionado");
        }
           
        else
            moveDirection.y = movementDirectionY;
    }
}