using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI timeLabel;

    void Start()
    {

    }
    void Update()
    {
        // Get the minutes and seconds since the level load
        int minutes = (int)(Time.timeSinceLevelLoad / 60);
        int seconds = (int)(Time.timeSinceLevelLoad % 60);

        // Format the time as mm:ss with leading zeros
        timeLabel.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }



}
