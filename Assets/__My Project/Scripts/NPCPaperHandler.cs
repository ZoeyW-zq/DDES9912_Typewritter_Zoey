using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class NPCPaperHandler : MonoBehaviour
{
    [SerializeField]
    MoveToPress move2Press;
    [SerializeField]
    PaperMovingSystem paperMovingSystem;
    public Transform pickUpPosition;
    public Transform pickDownPosition;
    public Transform button;
    public Transform handle;
    public WalkingNPCController walkingNPC;
    [SerializeField]
    Transform paper;
    


    public IEnumerator TakePaper()
    {
        //Move towards paper
        move2Press.targetPositionRightHand.position = pickUpPosition.position;
        while (Vector3.Distance(move2Press.targetPositionRightHand.position,move2Press.RigRightHand.position) > 0.01f) 
            yield return null;
        
        paper = paperMovingSystem.GetComponentInChildren<PaperTakeAway>().transform;
        paper.GetComponent<PaperTakeAway>().TakeAway();

        //PickUpPaper
        paper.SetParent(move2Press.RigRightHand,true);
        paper.localPosition = (new Vector3(0f, 0f, 0.15f));
        paper.localRotation = Quaternion.Euler(90, 0, 0);
        Debug.Log("have reset parent");
        yield return new WaitForSeconds(0.5f);

        //move towards destination
        move2Press.targetPositionRightHand.position = pickDownPosition.position;
        while (Vector3.Distance(move2Press.targetPositionRightHand.position, move2Press.RigRightHand.position) > 0.01f)
            yield return null;

        //deposit paper
        paper.SetParent(null);
        paper.GetComponent<Rigidbody>().isKinematic = false;
        paper.GetComponent<Rigidbody>().useGravity = true;

        //call walkingNPC
        walkingNPC.Walking2PickUp(paper);

        //right hand press button
        move2Press.targetPositionRightHand.position = button.position;
        while (Vector3.Distance(move2Press.targetPositionRightHand.position, move2Press.RigRightHand.position) > 0.01f)
            yield return null;
        move2Press.targetPositionRightHand.position = move2Press.rightHandIdle.position;
        while (Vector3.Distance(move2Press.targetPositionRightHand.position, move2Press.RigRightHand.position) > 0.01f)
            yield return null;

        //restart typing; left hand move towards lever
        move2Press.targetPositionLeftHand.position = handle.position;
        while (Vector3.Distance(move2Press.targetPositionLeftHand.position, move2Press.RigLeftHand.position) > 0.01f)
            yield return null;
        move2Press.targetPositionLeftHand.position = move2Press.leftHandIdle.position;
        while (Vector3.Distance(move2Press.targetPositionLeftHand.position, move2Press.RigLeftHand.position) > 0.01f)
            yield return null;
        StartCoroutine(move2Press.Type());
    }

    
}
