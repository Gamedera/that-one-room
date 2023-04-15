using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int numberOfInteractableObjects;
    [SerializeField] private float remainingTime = 30f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI objectCounterText;

    //private int numberOfCleanObjects;

    private void Update()
    {
        UpdateTimer();
        CountCleanObjects();
    }

    private void CountCleanObjects()
    {
        GameObject[] interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");

        int counter = 0;

        foreach (GameObject interactableObject in interactableObjects)
        {
            if (interactableObject.GetComponent<StateChanger>().IsClean())
            {
                counter++;
            }
        }

        objectCounterText.text = counter + "/" + numberOfInteractableObjects;
    }

    private void UpdateTimer()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }

        if (remainingTime <= 0)
        {
            remainingTime = 0;
        }

        timerText.text = Mathf.FloorToInt(remainingTime).ToString();
    }
}
