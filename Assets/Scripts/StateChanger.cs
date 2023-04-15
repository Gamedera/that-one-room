using UnityEngine;

public abstract class StateChanger : MonoBehaviour
{
    [SerializeField] public bool isClean;

    private MeshRenderer meshRenderer;
    private int originalOutlineLayer;
    private int selectedOutlineLayer;

    private void Awake() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalOutlineLayer = gameObject.layer;
        selectedOutlineLayer = LayerMask.NameToLayer("Selected Outline");
    }

    public abstract void ChangeState();

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
