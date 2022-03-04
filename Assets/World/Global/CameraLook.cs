using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private float MouseSensitivity = 100f;
    [SerializeField] private float mouseClampUp = 90f, mouseClampDown = -90f;
    private Transform playerTrans;
    private Transform mouseTrans;
    private float mouseX, mouseY;
    private float rotX;
    // Start is called before the first frame update
    void Start()
    {
        playerTrans = this.transform.parent.transform;
        mouseTrans = this.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, mouseClampDown, mouseClampUp);
        mouseTrans.localRotation = Quaternion.Euler(rotX, 0f, 0f);

        playerTrans.Rotate(Vector3.up * mouseX);
    }
}
