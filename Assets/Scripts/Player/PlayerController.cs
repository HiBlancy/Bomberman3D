using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("PlayerController")]
    [SerializeField] public Transform Camera;

    [SerializeField] float lookSpeed = 2f;
    [SerializeField] float lookXLimit = 80f;

    float rotationX;
    Camera cam;

    [HideInInspector] public float Lookvertical;
    [HideInInspector] public float Lookhorizontal;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Lookvertical = -Input.GetAxis("Mouse Y");
            Lookhorizontal = Input.GetAxis("Mouse X");

            rotationX += Lookvertical * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            Camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Lookhorizontal * lookSpeed, 0);
        }
    }
}