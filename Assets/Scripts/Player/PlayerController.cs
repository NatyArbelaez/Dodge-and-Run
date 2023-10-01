using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public GameObject char1;
    public GameObject char2;
    public GameObject char3;

    private CharacterController charController;
    private Animator playerAnimations;

    public float movement_Speed = 3f;
    public float gravity = 9.8f;
    public float rotation_Speed = 0.15f;
    public float rotateDegreesPerSecond = 180f;

    Vector3 moveDirection;

    void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerAnimations = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        AnimateWalk();
    }

    void Move()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");
        float rotacionHorizontal = Input.GetAxisRaw("Horizontal");
        float rotacionVertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical) * movement_Speed;
        charController.Move(moveDirection * movement_Speed * Time.deltaTime);
        switch (rotacionVertical)
        {
            case 0:
                break;
            case 1:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case -1:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
        }

        switch (rotacionHorizontal)
        {
            case 0:
                break;
            case 1:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case -1:
                transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
        }
    }
    void AnimateWalk()
    {
        if (charController.velocity.sqrMagnitude != 0f)
        {
            playerAnimations.SetBool("Walk", true);
        }
        else
        {
            playerAnimations.SetBool("Walk", false);
        }
    }
}
