using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] GameObject DebugPanel;
    [SerializeField] TMP_Text TimerText, WinTimerText;
    [SerializeField] GameObject WinTimeGO;

    Scenario scenario;

    private bool isActiveDebug;

    private float timer;
    private string winTime;
    private string hours, minutes, seconds;
    private float h, m, s;
    private bool isWin = false;
    
    void Start()
    {
        scenario = new Scenario();
    }

    void Update()
    {
        TimerText.text = Timer();
        if (scenario.IsWin && !isWin) SetWinTime();
    }

    private string Timer()
    {
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= 1) { s++; timer = 0; }
            if (s >= 60) { m++; s = 0; }
            if (m >= 60) { s++; m = 0; }

            if (s < 10) { seconds = "0" + s; } else { seconds = s.ToString(); }
            if (m < 10) { minutes = "0" + m; } else { minutes = m.ToString(); }
            if (h < 10) { hours = "0" + h; } else { hours = h.ToString(); }

            if (m < 1 && h < 1) { return "00:" + seconds; }
            if (h < 1) { return minutes + ":" + seconds; }
            else { return hours + ":" + minutes + ":" + seconds; }
        }
    }

    public void ShowHideDebug()
    {
        isActiveDebug = !isActiveDebug;
        DebugPanel.SetActive(isActiveDebug);
    }

    public void SetWinTime()
    {
        isWin = true;
        winTime = Timer();
        WinTimerText.text = winTime;
        WinTimeGO.SetActive(true);
    }
}
