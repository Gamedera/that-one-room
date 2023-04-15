using UnityEngine;

public class StateChangerImplementation : StateChanger
{
    [SerializeField] private GameObject cleanObject;
    [SerializeField] private GameObject dirtyObject;

    public override void ChangeState()
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

}
