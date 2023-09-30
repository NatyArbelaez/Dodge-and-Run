using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    public Text counterText; // Reference to the UI Text component.
    private int counter = 0; // Initialize the counter.

    private void Start()
    {
        counterText = GetComponent<Text>(); // Get a reference to the Text component.
        UpdateCounter(); // Call the method to initially display the counter.
    }

    private void UpdateCounter()
    {
        counterText.text = "Points:  " + counter; // Update the text to display the current counter value.
    }

    private void Update()
    {
        // Increment the counter infinitely.
        counter++;

        // Update the counter text.
        UpdateCounter();
    }
}
