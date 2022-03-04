using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coullsion : MonoBehaviour
{
    public Manger mymanger;
    public GameObject cube;
     public GameObject[] walls;
    //0.north
    //1.south
    //2.east
    //3.wast
    private CharacterController player;
    public Vector3[] wall_pos;
    public Vector3 newVector;
    public float buffer = 2f;

    private void Awake()
    {
        player = GetComponent<CharacterController>();
        wall_pos = new Vector3[4];
        for(var i=0;i<4;i++)
        {
            wall_pos[i] = walls[i].GetComponent<Renderer>().bounds.center;
        }

    }

    private void Start()
    {
        if (mymanger.gametype==1)
        {
            walls[0].GetComponent<MeshCollider>().isTrigger = false;
            walls[1].GetComponent<MeshCollider>().isTrigger = false;
        }
     
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (mymanger.gametype)
        {
            case 0:
                print("Turos");
                Torus(other);
                break;
            case 1:
                print("Ring");
                Ring(other);
                break;
            case 2:
                print("Klein");
                Klein(other);
                break;
        }


       
    }


    private void Torus(Collider other)
    {
        var new_pos = new Vector3(1f, 1f, 1f);
        var player_pos = player.transform.position;
        Instantiate(cube, transform.position, Quaternion.identity);
        player.enabled = false;
        switch (other.name)
        {

            case "Northwall":
                print("we hit in Northwall");
                new_pos = new Vector3(player_pos.x, 1, wall_pos[1].z+buffer);
                break;
            case "Eastwall":
                print("we hit in Eastwall");
                new_pos = new Vector3(wall_pos[3].x + buffer, 1, player_pos.z);
                break;
            case "Westwall":
                print("we hit in Westwall");
                new_pos = new Vector3(wall_pos[2].x - buffer, 1, player_pos.z);
                break;
            case "Southwall":
                print("we hit in Southwall");
                new_pos = new Vector3(player_pos.x, 1, wall_pos[0].z - buffer);
                break;

        }
        new_pos.y = player_pos.y;
        player.transform.position=new_pos;
        Instantiate(cube, transform.position, Quaternion.identity);
        player.enabled = true;
    }
    private void Ring(Collider other)
    {

        var new_pos = new Vector3(1f, 1f, 1f);
        var player_pos = player.transform.position;
        Instantiate(cube, transform.position, Quaternion.identity);
        player.enabled = false;
        switch (other.name)
        {

            case "Northwall":
                print("we hit in Northwall");
                break;
            case "Eastwall":
                print("we hit in Eastwall");
                new_pos = new Vector3(wall_pos[3].x + buffer, 1, player_pos.z);
                break;
            case "Westwall":
                print("we hit in Westwall");
                new_pos = new Vector3(wall_pos[2].x - buffer, 1, player_pos.z);
                break;
            case "Southwall":
                print("we hit in Southwall");
                break;

        }
        new_pos.y = player_pos.y;
        player.transform.position = new_pos;
        Instantiate(cube, transform.position, Quaternion.identity);
        player.enabled = true;

    }
    private void Klein(Collider other)
    {
        GameObject North = GameObject.Find("Northwall");
        GameObject South = GameObject.Find("Southhwall");
        var new_pos = new Vector3(1f, 1f, 1f);
        var player_pos = player.transform.position;
        Instantiate(cube, transform.position, Quaternion.identity);
        player.enabled = false;
        float abs;
        switch (other.name)
        {

            case "Northwall":
                print("we hit in Northwall");
                abs = (wall_pos[0].x - player_pos.x);
                new_pos = new Vector3(wall_pos[3].x + buffer, 1, wall_pos[3].z+abs);
                break;
            case "Eastwall":
                print("we hit in Eastwall");
                abs = (wall_pos[2].z - player_pos.z);
                new_pos = new Vector3(wall_pos[1].x+abs, 1, wall_pos[1].z + buffer);
                break;
            case "Westwall":
                print("we hit in Westwall");
                abs = (wall_pos[3].z - player_pos.z);
                new_pos = new Vector3(wall_pos[0].x +abs, 1, wall_pos[0].z - buffer);
                break;
            case "Southwall":
                print("we hit in Southwall");
                abs =(wall_pos[1].x - player_pos.x);
                new_pos = new Vector3(wall_pos[2].x - buffer, 1, wall_pos[2].z+abs);
                break;

        }
        new_pos.y = player_pos.y;
        player.transform.position = new_pos;
        Instantiate(cube,new_pos, Quaternion.identity);
        player.enabled = true;

    }
}
