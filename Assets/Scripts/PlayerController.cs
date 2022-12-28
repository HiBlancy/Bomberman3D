//by EvolveGames
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("PlayerController")]
    [SerializeField] public Transform Camera;
    [SerializeField] float walkingSpeed = 3f;
    public float CroughSpeed = 1f;
    [SerializeField] float jumpSpeed = 6f;
    [SerializeField] float lookSpeed = 2f;
    [SerializeField] float lookXLimit = 80f;

    [Header("Advance")]
    [SerializeField] float CroughHeight = 1f;
    [SerializeField] float gravity = 20f;

    [Header("Input")]
    [SerializeField] KeyCode CroughKey = KeyCode.LeftControl;

    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;
    bool isCrough = false;
    float InstallCroughHeight;
    float rotationX = 0;
    [HideInInspector] public bool isRunning = false;
    Vector3 InstallCameraMovement;
    float InstallFOV;
    Camera cam;
    [HideInInspector] public bool Moving;
    [HideInInspector] public float vertical;
    [HideInInspector] public float horizontal;
    [HideInInspector] public float Lookvertical;
    [HideInInspector] public float Lookhorizontal;

    float installGravity;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InstallCroughHeight = characterController.height;
        InstallCameraMovement = Camera.localPosition;
        InstallFOV = cam.fieldOfView;
        installGravity = gravity;
    }
    void Update()
    {
        RaycastHit CroughCheck;

        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        vertical = walkingSpeed * Input.GetAxis("Vertical");
        horizontal = walkingSpeed * Input.GetAxis("Horizontal");

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * vertical) + (right * horizontal);

        if (Input.GetButton("Jump") && characterController.isGrounded)
            moveDirection.y = jumpSpeed;

        else
            moveDirection.y = movementDirectionY;

        characterController.Move(moveDirection * Time.deltaTime);
        Moving = horizontal < 0 || vertical < 0 || horizontal > 0 || vertical > 0 ? true : false;

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Lookvertical = -Input.GetAxis("Mouse Y");
            Lookhorizontal = Input.GetAxis("Mouse X");

            rotationX += Lookvertical * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            Camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Lookhorizontal * lookSpeed, 0);
        }

        if (Input.GetKey(CroughKey))
        {
             isCrough = true;
             float Height = Mathf.Lerp(characterController.height, CroughHeight, 5 * Time.deltaTime);
             characterController.height = Height;
             walkingSpeed = Mathf.Lerp(walkingSpeed, CroughSpeed, 6 * Time.deltaTime);

        }
        else if (!Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.up), out CroughCheck, 0.8f, 1))
        {
            if (characterController.height != InstallCroughHeight)
            {
                    isCrough = false;
                    float Height = Mathf.Lerp(characterController.height, InstallCroughHeight, 6 * Time.deltaTime);
                    characterController.height = Height;
                    walkingSpeed = Mathf.Lerp(walkingSpeed, walkingSpeed, 4 * Time.deltaTime);
            }
        }
    }

}