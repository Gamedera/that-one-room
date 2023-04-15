using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] TextMeshProUGUI interactText;

    private List<GameObject> detectedObjects = new List<GameObject>();

    private void OnEnable() 
    {
        playerController.InteractEvent += OnInteract;
    }

    private void OnDisable()
    {
        playerController.InteractEvent -= OnInteract;
    }

    private void Update() 
    {
        if (detectedObjects.Count == 0)
        {
            interactText.gameObject.SetActive(false);
        }
        else
        {
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            detectedObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            detectedObjects.Remove(other.gameObject);
        }
    }

    private void OnInteract()
    {
        if (detectedObjects.Count == 0) return;

        GameObject detectedObject = detectedObjects[0];
        Debug.Log("Interacting with " + detectedObject.gameObject.name);
        detectedObject.GetComponent<StateChanger>().ChangeState();
    }
}
