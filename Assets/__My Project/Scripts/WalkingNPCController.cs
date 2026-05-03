using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WalkingNPCController : MonoBehaviour
{
    public Transform targetPosition;
    public Transform leftHandRig;
    public Transform walking2DropOff;
    public Transform dropOffPosition;

    public Rig rig;
    public Transform holdingTip;
    private float speed = 1.5f;
    private Vector3 leftHandOriginalPosition;
    public enum State { Idle, Walking2PickUp, Walking2DropOff }

    State state = State.Idle;
    [SerializeField]Transform paper;

    // Papers waiting to be picked up from either the NPC typist or the player
    Queue<Transform> paperQueue = new Queue<Transform>();
    Queue<Transform> destinationQueue = new Queue<Transform>();

    private void Start()
    {
        leftHandOriginalPosition = leftHandRig.localPosition;
        rig = leftHandRig.GetComponent<Rig>();
        rig.weight = 0.2f;

    }

    private void Update()
    {
        leftHandRig.position = Vector3.MoveTowards(leftHandRig.position, targetPosition.position, speed * Time.deltaTime);
    }

    // Called by both NPCPaperHandler and PaperDropZone
    public void Walking2PickUp(Transform paper2PickUp, Transform destination)
    {
        Debug.Log(transform.name + "is called");
        if (state == State.Idle)
            StartPickUp(paper2PickUp,destination);
        else
        {
            if (!paperQueue.Contains(paper2PickUp))
            {
                paperQueue.Enqueue(paper2PickUp);
                destinationQueue.Enqueue(destination);
            }
                
        }
            
    }

    void StartPickUp(Transform paper2PickUp, Transform destination)
    {
        paper = paper2PickUp;
        
        transform.GetComponent<NMAWalkTowards>().SetDestination(destination);
        transform.GetComponent<NMAWalkTowards>().onArrive.AddListener(OnArrive);
        state = State.Walking2PickUp;
    }

    void Walking2DropOff()
    {
        state = State.Walking2DropOff;
        transform.GetComponent<NMAWalkTowards>().SetDestination(walking2DropOff);
        transform.GetComponent<NMAWalkTowards>().onArrive.AddListener(OnArrive);
    }

    void OnArrive()
    {
        rig.weight = 1;
        transform.GetComponent<NMAWalkTowards>().onArrive.RemoveListener(OnArrive);
        Debug.Log("on arrived");
        if (state == State.Walking2PickUp)
            StartCoroutine(PickUp());

        if (state == State.Walking2DropOff)
            StartCoroutine(DropOff());
    }

    IEnumerator PickUp()
    {
        //rig.weight = 1;
        targetPosition.position = paper.position +Vector3.up*0.1f;
        while (Vector2.Distance(leftHandRig.position, targetPosition.position) > 0.01f)
            yield return null;

        paper.SetParent(holdingTip);
        paper.GetComponent<Rigidbody>().useGravity = false;
        paper.GetComponent<Rigidbody>().isKinematic = true;

        rig.weight = 0;
        Walking2DropOff();
    }

    IEnumerator DropOff()
    {
        //rig.weight = 1;
        targetPosition.position = dropOffPosition.position;
        while (Vector2.Distance(leftHandRig.position, targetPosition.position) > 0.01f)
            yield return null;

        paper.SetParent(null);
        paper.GetComponent<Rigidbody>().useGravity = true;
        paper.GetComponent<Rigidbody>().isKinematic = false;
        paper = null;
        //targetPosition.localPosition = leftHandOriginalPosition;
        rig.weight = 0;

        // Return to Idle, then immediately process the next queued paper if any
        state = State.Idle;
        if (paperQueue.Count > 0)
            StartPickUp(paperQueue.Dequeue(),destinationQueue.Dequeue());

       
    }
}
