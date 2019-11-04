using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{

    private Text text;

    private Slider slider;


    // Start is called before the first frame update
    void Awake()
    {
        text = transform.Find("Value").GetComponent<Text>();
        slider = GetComponent<Slider>();
    }

    public void UpdateText()
    {
        text.text = slider.value.ToString();
    }

    public void UpdateSliderValue(float value)
    {
        slider.value = value;
    }

    public float GetValue()
    {
        return slider.value;
    }
}
