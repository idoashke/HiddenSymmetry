using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKeyClickScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> keys;
    [SerializeField] private GameHandler handler;
    [SerializeField] private Camera cam;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit rayHit))
            {
                //var name = rayHit.collider.gameObject.name;
                var name = rayHit.transform.name;  ///TRY THIS NEW CODE LINE
                if(CheckName(name))
                    handler.LoadLevel(name);

            }
        }

    }

    private bool CheckName(string name)
    {
        foreach(var item in keys)
        {
            if (item.name == name)
                return true;
        }
        return false;
    }
}
