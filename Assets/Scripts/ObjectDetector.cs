using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] TextMeshProUGUI interactText;

    private List<GameObject> detectedObjects = new List<GameObject>();

    private int selectedOutlineLayer;
    private int outlineLayer;

    private void OnEnable() 
    {
        playerController.InteractEvent += OnInteract;
    }

    private void OnDisable()
    {
        playerController.InteractEvent -= OnInteract;
    }

    private void Awake() 
    {
        selectedOutlineLayer = LayerMask.NameToLayer("Selected Outline");
        outlineLayer = LayerMask.NameToLayer("Outline");
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

            other.gameObject.GetComponent<StateChanger>().ChangeToSelectableOutline();

            // other.gameObject.layer = selectedOutlineLayer;

            // foreach (Transform child in other.gameObject.transform)
            // {
            //      child.gameObject.layer = selectedOutlineLayer;
            // }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            detectedObjects.Remove(other.gameObject);

            other.gameObject.GetComponent<StateChanger>().ChangeToOriginalOutline();

            // other.gameObject.layer = outlineLayer;

            // foreach (Transform child in other.gameObject.transform)
            // {
            //      child.gameObject.layer = outlineLayer;
            // }
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
