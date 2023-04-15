using UnityEngine;

public class StateChanger : MonoBehaviour
{
    [SerializeField] private bool isClean;
    [SerializeField] private Material cleanMaterial;
    [SerializeField] private Material dirtyMaterial;

    private MeshRenderer meshRenderer;

    private void Awake() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeState()
    {
        Debug.Log("State changed on " + gameObject.name);
        isClean = !isClean;

        if (isClean)
        {
            meshRenderer.material = cleanMaterial;
        }
        else
        {
            meshRenderer.material = dirtyMaterial;
        }
    }

    public bool IsClean()
    {
        return isClean;
    }

}
