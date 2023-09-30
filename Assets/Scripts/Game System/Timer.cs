using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 60f;
    [SerializeField] Text countDownText;
    [HideInInspector] public bool isRunning = true;



    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        if ((isRunning))
        {
            currentTime -= 1 * Time.deltaTime;
            countDownText.text = currentTime.ToString("0");

            if (currentTime <= 3)
            {
                countDownText.color = Color.red;
            }

            if (currentTime <= 0)
            {
                currentTime = 0;
                Time.timeScale = 0;
            }
        }

    }
}
