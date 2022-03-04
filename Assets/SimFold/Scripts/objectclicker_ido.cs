using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectclicker_ido : FirstPersonController
{
    public Manger mymanger;
    public Camera camerapy;
    private void Awake()
    {
        //camerapy = GetComponentInChildren<Camera>();
        //camerapy = FirstPersonController < player;>;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = camerapy.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit rayHit))
            {
                //var name = rayHit.collider.gameObject.name;
                var name = rayHit.transform.name;  ///TRY THIS NEW CODE LINE
                mymanger.CheckAnswer(name);
                
            }
        }

    }

}
