using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
    private Slider slider;
    private TMP_Text textBox;
    

    private void Start()
    {
        slider = GetComponent<Slider>();
        textBox = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        var val = slider.normalizedValue * 100;
        textBox.text = val.ToString();
    }
}
