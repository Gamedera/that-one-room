using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : StateChanger
{

    // the door from this object
    [SerializeField] public GameObject door;
    // the door copied and rotated/moved to be the opened door
    [SerializeField] public GameObject doorOpen;
    // this will be a copy of the original door so that we have some numbers to work with.
    private GameObject doorClosed;
   
    // this is the movement rate (if movemnt is applied to the door)
    [SerializeField] public float moveSpeed = 3;
    // this is the rotation rate (if rotation is applied to the door)
    [SerializeField] public float rotationSpeed = 90;

    // Start is called before the first frame update
    void Start()
    {
        // copy the door to keep its position
        doorClosed = Instantiate(door, door.transform.position, door.transform.rotation);
        // hide both the open and closed door
        doorClosed.SetActive(false);
        doorOpen.SetActive(false);
        
    }

    // Update is called once per frame
    void FixedUpdate(){
        // every frame, move the door towards the Open/Closed door
        var target = isClean ? doorClosed : doorOpen;
        // these actually do the moving/rotating
        door.transform.position = Vector3.MoveTowards(door.transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        door.transform.rotation = Quaternion.RotateTowards(door.transform.rotation, target.transform.rotation, rotationSpeed * Time.deltaTime);
    }

    new public void ChangeState()
    {
        Debug.Log("State changed on " + gameObject.name);
        isClean = !isClean;
    }
}
