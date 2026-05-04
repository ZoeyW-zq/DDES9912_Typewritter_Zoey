using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WalkingNPCController : MonoBehaviour
{
    [SerializeField] Transform targetPosition;
    [SerializeField] Transform leftHandRig;
    [SerializeField][Tooltip("where to drop off paper")] Transform dropOffDestination;
    [SerializeField][Tooltip("position for hand to release")] Transform releasePaperPosition;
    [SerializeField][Tooltip("transform that paper will attach to")] Transform holdingTip;
    [SerializeField][Tooltip("speed of hand movement")] float speed = 1.5f;
    Rig rig;
    Transform paper;

    enum State { Idle, Walking2PickUp, Walking2DropOff }
    State state = State.Idle;

    // Papers waiting to be picked up from either the NPC typist or the player
    Queue<Transform> paperQueue = new Queue<Transform>();
    Queue<Transform> destinationQueue = new Queue<Transform>();

    private void Start()
    {
        rig = leftHandRig.GetComponent<Rig>();
        rig.weight = 0f;

    }

    private void Update()
    {
        leftHandRig.position = Vector3.MoveTowards(leftHandRig.position, targetPosition.position, speed * Time.deltaTime);
    }

    // Called by both NPCPaperHandler and PaperSnapper
    public void StartPickUp(Transform paper2PickUp, Transform destination)
    {
        if (state == State.Idle)
        {
            Walk2PickUp(paper2PickUp, destination); 
        }
        else
        {
            if (!paperQueue.Contains(paper2PickUp))
            {
                paperQueue.Enqueue(paper2PickUp);
                destinationQueue.Enqueue(destination); 
            }
        }
    }

    void Walk2PickUp(Transform paper2PickUp, Transform destination)
    {
        paper = paper2PickUp;
        
        transform.GetComponent<NMAWalkTowards>().SetDestination(destination);
        state = State.Walking2PickUp;
    }

    void Walk2DropOff()
    {
        state = State.Walking2DropOff;
        transform.GetComponent<NMAWalkTowards>().SetDestination(dropOffDestination);
    }

    //Invoke when onArrive triggered
    public void CheckState()
    {
        if (state == State.Walking2PickUp)
            StartCoroutine(PickUp());

        if (state == State.Walking2DropOff)
            StartCoroutine(DropOff());
    }

    IEnumerator PickUp()
    {
        rig.weight = 1;
        targetPosition.position = paper.position +Vector3.up*0.1f;
        while (Vector2.Distance(leftHandRig.position, targetPosition.position) > 0.01f)
            yield return null;

        paper.SetParent(holdingTip);
        paper.GetComponent<Rigidbody>().useGravity = false;
        paper.GetComponent<Rigidbody>().isKinematic = true;

        rig.weight = 0;
        Walk2DropOff();
    }

    IEnumerator DropOff()
    {
        rig.weight = 1;
        targetPosition.position = releasePaperPosition.position;
        while (Vector2.Distance(leftHandRig.position, targetPosition.position) > 0.01f)
            yield return null;

        paper.SetParent(null);
        paper.GetComponent<Rigidbody>().useGravity = true;
        paper.GetComponent<Rigidbody>().isKinematic = false;
        paper = null;
        rig.weight = 0;

        // Return to Idle, then immediately process the next queued paper
        state = State.Idle;
        if (paperQueue.Count > 0)
            Walk2PickUp(paperQueue.Dequeue(),destinationQueue.Dequeue());

       
    }
}
