using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traget : MonoBehaviour
{
    private Renderer renderer;
    public Material met;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    //private void OnMouseOver()
    private void OnMouseEnter()
    {
        renderer.material.color = Color.red;
    }
    private void OnMouseExit()
    {
        renderer.material = met;
    }
}
