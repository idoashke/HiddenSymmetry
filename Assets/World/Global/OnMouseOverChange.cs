using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOverChange : MonoBehaviour
{
    [Header("Make sure the item has a Renderer and a Collider.")]
    private Renderer rend;
    private Color mat;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        mat = new Color();
        mat = rend.material.color;
    }

    private void OnMouseEnter()
    {
        rend.material.color = Color.red;
    }

    private void OnMouseExit()
    {
        rend.material.color = mat;
    }

}
