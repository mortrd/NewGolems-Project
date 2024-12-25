using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timertext;
    [SerializeField] Text[] texts;
    [SerializeField] CastleHealth castle;
    float elapsedtime = 0;
    int min;
    int sec;



    void Update()
    {
        if (!castle.GateDestroyed)
        {
            elapsedtime += Time.deltaTime;
            min = Mathf.FloorToInt(elapsedtime / 60);
            sec = Mathf.FloorToInt(elapsedtime % 60);
            timertext.text = string.Format("{0:00}:{1:00}", min, sec);
        }
        else
        {
            texts[0].text = sec.ToString();
            texts[1].text = min.ToString();
        }
        

    }
}
