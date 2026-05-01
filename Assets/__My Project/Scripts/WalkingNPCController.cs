using System.Collections;
using UnityEngine;

public class WalkingNPCController : MonoBehaviour
{
    public Transform targetPosition;
    public Transform leftHandRig;
    public Transform walking2PickUp;
    public Transform walking2DropOff;
    public Transform dropOffPosition;
    private float speed = 0.8f;
    private Transform leftHandOriginalTransform;
    public enum State { Idle, Walking2PickUp, Walking2DropOff}

    State state=State.Idle;

    Transform paper;

    private void Start()
    {
        leftHandOriginalTransform = leftHandRig.transform;
    }
    private void Update()
    {
        leftHandRig.position = Vector3.MoveTowards(leftHandRig.position, targetPosition.position, speed*Time.deltaTime);
    }
    public void Walking2PickUp(Transform paper2PickUp)
    {
        paper = paper2PickUp;
        transform.GetComponent<NMAWalkTowards>().SetDestination(walking2PickUp);
        transform.GetComponent<NMAWalkTowards>().onArrive.AddListener(OnArrive);
        state = State.Walking2PickUp;
    }

    void Walking2DropOff()
    {
        state = State.Walking2DropOff;
        transform.GetComponent<NMAWalkTowards>().SetDestination(walking2DropOff);

    }

    void OnArrive()
    {
        if (state == State.Walking2PickUp) {
            StartCoroutine(PickUp());
        }
        if(state == State.Walking2DropOff)
        {
            StartCoroutine(DropOff());
        }
        
    }

    IEnumerator PickUp()
    {
        targetPosition.position = paper.position;
        while(Vector2.Distance(leftHandRig.position,targetPosition.position)>0.01f)
            yield return null;

        paper.SetParent(leftHandRig);
        paper.GetComponent<Rigidbody>().useGravity = false;
        paper.GetComponent <Rigidbody>().isKinematic = true;
        Debug.Log("get paper!");

        targetPosition.position = leftHandOriginalTransform.position;
        while (Vector2.Distance(leftHandRig.position, targetPosition.position) > 0.01f)
            yield return null;
        Walking2DropOff();
    }

    IEnumerator DropOff()
    {
        targetPosition.position = dropOffPosition.position;
        while (Vector2.Distance(leftHandRig.position, targetPosition.position) > 0.01f)
            yield return null;
        paper.SetParent(null);
        paper.GetComponent<Rigidbody>().useGravity = true;
        paper.GetComponent<Rigidbody>().isKinematic = false;

        targetPosition.position = leftHandOriginalTransform.position;
    }
}
