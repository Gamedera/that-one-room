using UnityEngine;

public class MotionStateChanger : StateChanger
{
    [SerializeField] private GameObject cleanObject;
    [SerializeField] private GameObject dirtyObject;

    [SerializeField] private GameObject cleanPosition;
    [SerializeField] private GameObject dirtyPosition;

    private Vector3 cleanTransformPosition;
    private Vector3 dirtyTransformPosition;


    private void Start() 
    {
        cleanTransformPosition = cleanPosition.transform.position;
        dirtyTransformPosition = dirtyPosition.transform.position;


        if (isClean)
        {
            cleanObject.SetActive(true);
            dirtyObject.SetActive(false);
            gameObject.transform.position = cleanTransformPosition;

        }
        else
        {
            dirtyObject.SetActive(true);
            cleanObject.SetActive(false);
            gameObject.transform.position = dirtyTransformPosition;
        }
    }

    public override void ChangeState()
    {
        Debug.Log("State changed on " + gameObject.name);
        isClean = !isClean;

        if (isClean)
        {
            cleanObject.SetActive(true);
            dirtyObject.SetActive(false);
            gameObject.transform.position = cleanTransformPosition;
        }
        else
        {
            dirtyObject.SetActive(true);
            cleanObject.SetActive(false);
            gameObject.transform.position = dirtyTransformPosition;
        }
    }

}
