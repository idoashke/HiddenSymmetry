using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class player : MonoBehaviour
{
    public float speed = 0f;
    public float jumpspeed = 0;
    public float gravity = 0f;
    public Camera playercam;
    public float lookspeed = 0f;
    public float xlimit = 0f;


    CharacterController charactercontroller;
    Vector3 movedirection = Vector3.zero;
    Vector2 ratetion = Vector2.zero;
    public bool canmove = true;

    private void Start()
    {
        charactercontroller = GetComponent<CharacterController>();
        ratetion.y = transform.eulerAngles.y;
    }
    private void Update()
    {
       //if (charactercontroller.isGrounded)
       //{
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curspeedx = canmove ? speed * Input.GetAxis("Vertical") : 0;
            float curspeedy = canmove ? speed * Input.GetAxis("Horizontal") : 0;
            movedirection = (forward * curspeedx) + (right * curspeedy);

            if (Input.GetButton("Jump") && canmove)
            {
                movedirection.y = jumpspeed;
                print("Jump");
            }



            movedirection.y -= gravity * Time.deltaTime;
            charactercontroller.Move(movedirection * Time.deltaTime);
            if (canmove)
            {
                ratetion.y += Input.GetAxis("Mouse X") * lookspeed;
                ratetion.x += -Input.GetAxis("Mouse Y") * lookspeed;
                ratetion.x = Mathf.Clamp(ratetion.x, -xlimit, xlimit);
                playercam.transform.localRotation = Quaternion.Euler(ratetion.x, 0, 0);
                transform.eulerAngles = new Vector2(0, ratetion.y);

            }
        //}
    }
}

    
