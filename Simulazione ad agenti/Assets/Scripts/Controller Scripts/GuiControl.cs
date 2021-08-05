using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GuiControl : MonoBehaviour
{
    public Button b05f, b1, b2, b3, pause;
    public Text aTot, aCont, aH, aInf;
    private GameControl gameControl;
    
    void Start()
    {
        b05f.onClick.AddListener(() => Time.timeScale = 0.5f);
        b1.onClick.AddListener(() => Time.timeScale = 1);
        b2.onClick.AddListener(() => Time.timeScale = 2);
        b3.onClick.AddListener(() => Time.timeScale = 3);
        pause.onClick.AddListener(() => Time.timeScale = 0);
        gameControl = FindObjectOfType<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        aTot.text = "Agenti totali:" + gameControl.ATot;
        aCont.text = "Agenti Contagiati:" + gameControl.ACont;
        aH.text = "Agenti Sani:" + gameControl.AH;
        aInf.text = "Agenti Infetti:" + gameControl.AInf;
        
    }
}
