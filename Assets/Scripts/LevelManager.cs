using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float remainingTime = 30f;
    [SerializeField] private int goodEndingThreshold = 10;
    [SerializeField] private int evilEndingThreshold = 3;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI objectCounterText;

    private int numberOfInteractableObjects;
    private GameObject[] interactableObjects;

    private const string InteractableTag = "Interactable";
    private const string GoodEndScene = "EndSceneGood";
    private const string EvilEndScene = "EndSceneEvil";
    private const string MediumEndScene = "EndSceneMedium";

    private void Start() 
    {
        interactableObjects = GameObject.FindGameObjectsWithTag(InteractableTag);
        numberOfInteractableObjects = interactableObjects.Length;
    }

    private void Update()
    {
        UpdateTimer();
        
        int cleanObjects = CountCleanObjects();

        if (cleanObjects == numberOfInteractableObjects)
        {
            LoadGoodEndScene();
            return;
        } 

        if (cleanObjects == 0)
        {
            LoadEvilEndScene();
            return;
        }
    }

    private int CountCleanObjects()
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

        return counter;
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
            DecideEnding();
        }

        timerText.text = Mathf.FloorToInt(remainingTime).ToString();
    }

    private void DecideEnding()
    {
        int cleanObjects = CountCleanObjects();

        if (cleanObjects >= goodEndingThreshold)
        {
            LoadGoodEndScene();
            return;
        }

        if (cleanObjects <= evilEndingThreshold)
        {
            LoadEvilEndScene();
            return;
        }

        LoadMediumEndScene();
    }

    private void LoadGoodEndScene()
    {
        SceneManager.LoadScene(GoodEndScene);
    }

    private void LoadEvilEndScene()
    {
        SceneManager.LoadScene(EvilEndScene);
    }

    private void LoadMediumEndScene()
    {
        SceneManager.LoadScene(MediumEndScene);
    }
}
