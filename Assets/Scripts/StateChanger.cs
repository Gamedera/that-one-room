using UnityEngine;

public class StateChanger : MonoBehaviour
{
    public bool isClean = true;

    public void ChangeState()
    {
        Debug.Log("State changed on " + gameObject.name);
        isClean = !isClean;
    }
}
