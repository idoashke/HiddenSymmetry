using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject messageBox;
    [SerializeField] private TMP_Text keyScore;
    void Start()
    {
        messageBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        keyScore.text = GameHandler.gatheredKeys + " / " + GameHandler.totalKeys;
    }
}
