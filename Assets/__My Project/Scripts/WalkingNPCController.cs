using System.Collections;
using UnityEngine;

public class WalkingNPCController : MonoBehaviour
{
    public Transform targetPosition;
    public Transform leftHandRig;
    public Transform walking2PickUp;
    public Transform walking2DropOff;
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

    public void Walking2DropOff()
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
            DropOff();
        }
        
    }

    IEnumerator PickUp()
    {
        Transform originalPosition = transform;
        targetPosition.position = paper.position;
        while(Vector2.Distance(leftHandRig.position,targetPosition.position)>0.01f)
            yield return null;

        paper.SetParent(leftHandRig);
        Debug.Log("get paper!");
        Walking2DropOff();
    }

    public void DropOff()
    {
        paper.SetParent(null);
        targetPosition.position = leftHandOriginalTransform.position;
    }
}
