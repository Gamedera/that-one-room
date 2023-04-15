using UnityEngine;

public class LocationStateChanger : StateChanger
{
    private Vector3 initialPosition;
    private Rigidbody  rb;

    private void Awake() 
    {
        initialPosition = GetComponent<Transform>().localPosition;
        rb = GetComponent<Rigidbody>();
        Debug.Log("Position of " + gameObject.name + " is " + initialPosition);
    }

    public void Update() {

        if(hasLocationChanged()) {
            isClean = false;
        }
        
    }

    public new void ChangeState()
    {
        Debug.Log("State changed on " + gameObject.name);
        rb.AddForce(new Vector3(100f, 100f, 100f), ForceMode.Impulse);
    }

    private bool hasLocationChanged() {

        float distance = Vector3.Distance(initialPosition, gameObject.transform.localPosition);
        Debug.Log("Distance of " + gameObject.name + " on " + gameObject.transform.localPosition
         + " from intial point " + initialPosition + "is: " + distance);

        return false;
    }

}
