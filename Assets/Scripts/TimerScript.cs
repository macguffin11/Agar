using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    Image timeBar;
    public float maxTime = 5f;
    public float timeLeft;
    public float test;

    private MenuManager menuManager;

    // Start is called before the first frame update
    void Start()
    {
        timeBar = GetComponent<Image>();
        if (timeBar == null)
        {
            Debug.Log("No TimeBar found!");
        }
        menuManager = FindObjectOfType<MenuManager>();
        if (menuManager == null)
        {
            Debug.Log("No MenuManager found!");
        }
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timeBar.fillAmount = timeLeft / maxTime;
            test = timeBar.fillAmount;
        }
        else
        {
            menuManager.GameOverScene();
        }
    }
}
