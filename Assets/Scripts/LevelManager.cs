using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float remainingTime = 30f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI objectCounterText;

    private int numberOfInteractableObjects;
    private GameObject[] interactableObjects;

    private void Start() 
    {
        interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
        numberOfInteractableObjects = interactableObjects.Length;
    }

    private void Update()
    {
        UpdateTimer();
        CountCleanObjects();
    }

    private void CountCleanObjects()
    {
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
            LoadEndScene();
        }

        timerText.text = Mathf.FloorToInt(remainingTime).ToString();
    }

    private void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
