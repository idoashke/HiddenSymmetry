using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 12f;//, gravity = 9.8f;
    private CharacterController player;
    private float x, z;
    private Vector3 moveTo;
    
    private void Awake()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
       
        moveTo = transform.right * x + transform.forward * z;
        player.Move(moveTo * speed * Time.deltaTime);
    }
}
