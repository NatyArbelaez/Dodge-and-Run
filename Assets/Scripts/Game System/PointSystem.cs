using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointSystem : MonoBehaviour
{
    public Text counterText; // Reference to the UI Text component.
    private float counter = 0; // Initialize the counter.
    public Text finalScore;

    private void Start()
    {
        counterText = GetComponent<Text>(); // Get a reference to the Text component.
        UpdateCounter(); // Call the method to initially display the counter.
    }

    private void UpdateCounter()
    {
        counterText.text = "Points:  " + counter; // Update the text to display the current counter value.
        finalScore.text = "TERRIFIC!\r\nscore:\r\n" + counter;
    }

    private void FixedUpdate()
    {
        // Increment the counter infinitely.
        counter += Time.deltaTime * 100;


        // Update the counter text.
        UpdateCounter();
    }
}
