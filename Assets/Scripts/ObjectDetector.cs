using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] GameObject interactPanel;
    //[SerializeField] TextMeshProUGUI interactText;

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
            interactPanel.SetActive(false);
        }
        else
        {
            interactPanel.SetActive(true);
        }

        if (detectedObjects.Count == 1)
        {
            detectedObjects[0].GetComponent<StateChanger>().ChangeToSelectableOutline();
        }

        if (detectedObjects.Count > 1)
        {
            GameObject closestObject = GetClosestDetectedObject();

            closestObject.GetComponent<StateChanger>().ChangeToSelectableOutline();

            foreach (GameObject detectedObject in detectedObjects)
            {
                if (detectedObject == closestObject) continue;

                detectedObject.GetComponent<StateChanger>().ChangeToOriginalOutline();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            detectedObjects.Add(other.gameObject);

            GameObject closestObject = GetClosestDetectedObject();

            closestObject.GetComponent<StateChanger>().ChangeToSelectableOutline();

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

        // GameObject detectedObject = detectedObjects[0];
        // Debug.Log("Interacting with " + detectedObject.gameObject.name);
        // detectedObject.GetComponent<StateChanger>().ChangeState();

        GameObject closestObject = GetClosestDetectedObject();

        closestObject.GetComponent<StateChanger>().ChangeState();
        transform.parent.gameObject.GetComponent<PlayerController>().PlayInteractSound();
    }

    private GameObject GetClosestDetectedObject()
    {
        float nearestDistance = float.PositiveInfinity;
        GameObject closestObject = null;

        foreach (GameObject detectedObject in detectedObjects)
        {
            float distance = Vector3.Distance(transform.position, detectedObject.transform.position);
            Debug.Log("Distance from " + detectedObject.name + " is " + distance);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                closestObject = detectedObject;
            }
        }

        return closestObject;
    }
}
