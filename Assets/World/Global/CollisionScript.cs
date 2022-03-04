using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    //Temp Script, will be replaced with Mouse Click per camera location focus.
    [SerializeField] private GameHandler handler;
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        handler.LoadLevel(other.name);
    }
}
