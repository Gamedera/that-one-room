using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

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
        //Debug.Log(detectedObjects.Count);
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
