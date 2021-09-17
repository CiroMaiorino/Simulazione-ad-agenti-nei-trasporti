using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderText : MonoBehaviour
{

    public Slider slider;
    void Start()
    {
        GetComponent<Text>().text = slider.value.ToString();
    }

    public void textUpdate(float value)
    {
        GetComponent<Text>().text = slider.value.ToString();
    }
}
