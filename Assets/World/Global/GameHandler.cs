using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * How to use:
 * 
    [SerializeField] private GameHandler handler;
    private void OnTriggerEnter(Collider other) // Doesnt need to be a collider, just whatever interaction.
    {
        print(other.name);
        handler.LoadLevel(other.name);
    }
 */


public class GameHandler : MonoBehaviour
{
    public static string worldSceneName = "MainScene";
    public static int gatheredKeys = 0;
    [SerializeField] private GameObject[] interactableObjects; // Just nice to see.
    [SerializeField] private string[] sceneNames;
    [SerializeField] private Canvas gameUiCanvas;
    [SerializeField] private TMP_Text keyAmount;
    [SerializeField] private GameObject endGameObject;
    [SerializeField] private AudioClip winSong;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 endGamePos;
    public static string currentGame = "";
    public static int currentDataForGame = 0;
    private bool movedToEndGame = false;
    public const int totalKeys = 9;
    public static bool[] gotKeys;

    private void Awake()
    {
        if(gotKeys == null) // init the keys Check
        {
            gotKeys = new bool[totalKeys];
            for (int i = 0; i < totalKeys; i++)
                gotKeys[i] = false;
        }
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            gatheredKeys++;
        }
    }

    int delayCounter = 0;

    private void FixedUpdate()
    {
        if (gatheredKeys >= totalKeys && !movedToEndGame)
        {
            print("We gathered all the keys.");
            movedToEndGame = true;
            StartCoroutine(GameWinning());
        }
        delayCounter++;

        if(delayCounter > 35)
        {
            keyAmount.text = gatheredKeys.ToString() + " / " + totalKeys.ToString();
            DisableKeys();
            delayCounter = 0;
        }
    }

    private void DisableKeys()
    {
        for(int i = 0; i < totalKeys; i++)
        {
            interactableObjects[i].SetActive(!gotKeys[i]);
        }
    }

    private IEnumerator GameWinning()
    {
        yield return new WaitForSeconds(3f);
        endGameObject.SetActive(true);
        var music = GetComponent<AudioSource>();
        music.clip = winSong;
        music.loop = true;
        music.volume = 0.5f;
        music.Play();
        player.transform.localPosition = endGamePos;
    }

    public void LoadLevel(string obj)
    {
        //per the obj name we go to the scene
        string[] splitArray = obj.Split(char.Parse("_"));
        var sceneIndex = int.Parse(splitArray[0]);
        currentDataForGame = int.Parse(splitArray[1]);
        currentGame = obj;
        SceneManager.LoadScene(sceneNames[sceneIndex]);
    }
}