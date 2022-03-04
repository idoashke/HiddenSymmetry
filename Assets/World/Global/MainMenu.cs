using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("This will be as a Local position teleportation.")]
    [SerializeField] private Vector3 resetLocation;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject howToPlayPrefab;
    [SerializeField] private new AudioSource audio;
    [SerializeField] private Slider audioVolumeSlider;
    [SerializeField] private Canvas uiCanvas;

    private Camera playerCamera;

    private Canvas canvas;
    private GameObject howToPlayRef;
    private bool howToPlayToggle = false;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        if (howToPlayRef == null)
            howToPlayRef = Instantiate(howToPlayPrefab, transform);
        howToPlayRef.SetActive(false);

        playerCamera = player.GetComponentInChildren<Camera>();
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnableCanvas();
        }
    }


    private void EnableCanvas()
    {
        uiCanvas.enabled = false;
        canvas.enabled = true;
        player.GetComponent<FirstPersonController>().enabled = false;
        playerCamera.GetComponentInChildren<Canvas>().enabled = false;
        Time.timeScale = 0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        //Audio
        audioVolumeSlider.normalizedValue = audio.volume;

    }

    public void DisableCanvas()
    {
        canvas.enabled = false;
        uiCanvas.enabled = true;
        player.GetComponent<FirstPersonController>().enabled = true;
        playerCamera.GetComponentInChildren<Canvas>().enabled = true;
        Time.timeScale = 1f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        //Audio
        audio.volume = audioVolumeSlider.normalizedValue;
    }

    public void QuitGame()
    {
        print("Quitting game works only in build version.");
        DisableCanvas();
        Application.Quit();
    }

    public void Teleport()
    {
        player.transform.localPosition = resetLocation;
        player.transform.rotation = Quaternion.identity;
        DisableCanvas();
    }

    public void HowToPlay()
    {
        if (howToPlayToggle) // deactivate the howto
        {
            howToPlayToggle = false;
        }
        else // show or make the how to
        {
            howToPlayToggle = true;
        }
        howToPlayRef.SetActive(howToPlayToggle);
    }
}
