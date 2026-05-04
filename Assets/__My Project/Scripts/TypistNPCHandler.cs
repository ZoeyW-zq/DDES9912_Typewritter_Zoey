using System.Collections;
using UnityEngine;

public class TypistNPCHandler : MonoBehaviour
{
    [SerializeField] MoveToPress move2Press;
    [SerializeField] PaperMovingSystem paperMovingSystem;
    [SerializeField][Tooltip("where typist reaches paper")] Transform pickUpPosition;
    [SerializeField][Tooltip("where typist place paper")] Transform pickDownPosition;
    [SerializeField][Tooltip("the lever that typist uses to add new paper")] Transform button;
    [SerializeField][Tooltip("the lever that typist uses to reset typewritter")] Transform lever;
    [SerializeField][Tooltip("the walkingNPC you want to call to deliver paper")] WalkingNPCController walkingNPC;
    [SerializeField][Tooltip("the destination for walkingNPC to pick up paper")] Transform NPCdestination;
    Transform paper;
    


    public IEnumerator TakePaper()
    {
        //Right hand reaches paper
        move2Press.targetPositionRightHand.position = pickUpPosition.position;
        yield return StartCoroutine(WaitUntilReach(move2Press.targetPositionRightHand, move2Press.RigRightHand));
        
        
        paper = paperMovingSystem.GetComponentInChildren<PaperTakeAway>().transform;
        paper.GetComponent<PaperTakeAway>().TakeAway();
        paper.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        //PickUpPaper
        paper.SetParent(move2Press.RigRightHand);
        paper.localPosition = (new Vector3(0f, 0f, 0.15f));
        paper.localRotation = Quaternion.Euler(90, 0, 0);

        //move towards target position
        move2Press.targetPositionRightHand.position = pickDownPosition.position;
        yield return StartCoroutine(WaitUntilReach(move2Press.targetPositionRightHand, move2Press.RigRightHand));

        //release paper
        paper.SetParent(null);
        paper.GetComponent<Rigidbody>().isKinematic = false;
        paper.GetComponent<Rigidbody>().useGravity = true;

        //right hand press button to add new paper
        move2Press.targetPositionRightHand.position = button.position; 
        yield return StartCoroutine(WaitUntilReach(move2Press.targetPositionRightHand, move2Press.RigRightHand));
        move2Press.targetPositionRightHand.position = move2Press.rightHandIdle.position;
        yield return StartCoroutine(WaitUntilReach(move2Press.targetPositionRightHand, move2Press.RigRightHand));

        //callNPC to deliver
        walkingNPC.StartPickUp(paper,NPCdestination);

        //restart typing; left hand move towards lever
        move2Press.targetPositionLeftHand.position = lever.position;
        yield return StartCoroutine(WaitUntilReach(move2Press.targetPositionLeftHand, move2Press.RigLeftHand));
        move2Press.targetPositionLeftHand.position = move2Press.leftHandIdle.position;
        yield return StartCoroutine(WaitUntilReach(move2Press.targetPositionLeftHand, move2Press.RigLeftHand));

        StartCoroutine(move2Press.Type());
    }

    IEnumerator WaitUntilReach(Transform currentTransform, Transform targetTransform)
    {
        while(Vector3.Distance(currentTransform.position, targetTransform.position) > 0.01f)
            yield return null;
    }

    
}
