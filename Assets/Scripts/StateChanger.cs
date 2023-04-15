using UnityEngine;

public class StateChanger : MonoBehaviour
{
    [SerializeField] public bool isClean;
    [SerializeField] private GameObject cleanObject;
    [SerializeField] private GameObject dirtyObject;

    private MeshRenderer meshRenderer;
    private int originalOutlineLayer;
    private int selectedOutlineLayer;

    private void Awake() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalOutlineLayer = gameObject.layer;
        selectedOutlineLayer = LayerMask.NameToLayer("Selected Outline");
    }

    public void ChangeState()
    {
        Debug.Log("State changed on " + gameObject.name);
        isClean = !isClean;

        if (isClean)
        {
            cleanObject.SetActive(true);
            dirtyObject.SetActive(false);
        }
        else
        {
            dirtyObject.SetActive(true);
            cleanObject.SetActive(false);
        }
    }

    public void ChangeToSelectableOutline()
    {
        gameObject.layer = selectedOutlineLayer;

        foreach (Transform child in gameObject.transform)
        {
                child.gameObject.layer = selectedOutlineLayer;
        }
    }

    public void ChangeToOriginalOutline()
    {
        gameObject.layer = originalOutlineLayer;

        foreach (Transform child in gameObject.transform)
        {
                child.gameObject.layer = originalOutlineLayer;
        }
    }

    public bool IsClean()
    {
        return isClean;
    }

}
