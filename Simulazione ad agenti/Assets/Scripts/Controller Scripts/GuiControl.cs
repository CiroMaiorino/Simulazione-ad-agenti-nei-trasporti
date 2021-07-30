using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GuiControl : MonoBehaviour
{
    public Button b05f, b1, b2, b3, pause;
    
    void Start()
    {
        b05f.onClick.AddListener(() => Time.timeScale = 0.5f);
        b1.onClick.AddListener(() => Time.timeScale = 1);
        b2.onClick.AddListener(() => Time.timeScale = 2);
        b3.onClick.AddListener(() => Time.timeScale = 3);
        pause.onClick.AddListener(() => Time.timeScale = 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
