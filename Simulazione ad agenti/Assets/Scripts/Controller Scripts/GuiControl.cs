using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GuiControl : MonoBehaviour
{
    public Button b05f, b1, b2, b3,b5,pause,play;
    public Text aTot, aCont, aH, aInf;
    public GameObject clock;
    public Slider infectionsPercentage,contagionsPercentage;
    private GameControl gameControl;
    
    void Start()
    {
        play.onClick.AddListener(() => Play());
        Time.timeScale = 0f;
        SetPlayUI(false);
         b05f.onClick.AddListener(() => Time.timeScale = 0.5f);
        b1.onClick.AddListener(() => Time.timeScale = 1);
        b2.onClick.AddListener(() => Time.timeScale = 2);
        b3.onClick.AddListener(() => Time.timeScale = 3);
        b5.onClick.AddListener(() => Time.timeScale = 5);
        pause.onClick.AddListener(() => Pause());
        gameControl = FindObjectOfType<GameControl>();
    }

    void setPausedUI(bool active)
    {
        infectionsPercentage.gameObject.SetActive(active);
        contagionsPercentage.gameObject.SetActive(active);
        play.gameObject.SetActive(active);
    }

    private void SetPlayUI(bool active)
    {
        b05f.gameObject.SetActive(active);
        b1.gameObject.SetActive(active);
        b2.gameObject.SetActive(active);
        b3.gameObject.SetActive(active);
        b5.gameObject.SetActive(active);
        pause.gameObject.SetActive(active);
        clock.gameObject.SetActive(active);
        aTot.gameObject.SetActive(active);
        aCont.gameObject.SetActive(active);
        aH.gameObject.SetActive(active);
        aInf.gameObject.SetActive(active);
    }
    void Play()
    {
        setPausedUI(false);
        Time.timeScale = 1f;
        SetPlayUI(true);
       
        
        gameControl.infectionPercentage = (int)infectionsPercentage.value;
        gameControl.ContagiousPercentage = (int)contagionsPercentage.value;

        gameControl.SpawningAtStops();
    }

    void Pause()
    {
        setPausedUI(true);
        Time.timeScale = 0f;
        SetPlayUI(false);

    }
    // Update is called once per frame
    void Update()
    {
        if (aTot.IsActive() && aCont.IsActive() && aH.IsActive() && aInf.IsActive()) { 
            aTot.text = "Agenti totali:" + gameControl.ATot;
            aCont.text = "Agenti Contagiosi:" + gameControl.ACont;
            aH.text = "Agenti Sani:" + gameControl.AH;
            aInf.text = "Agenti Infettati:" + gameControl.AInf;
           }
    }
}
