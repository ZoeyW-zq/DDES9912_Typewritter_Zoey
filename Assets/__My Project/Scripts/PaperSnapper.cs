using UnityEngine;

public class PaperSnapper : MonoBehaviour
{
    [SerializeField] HoldableMagnetSnapper snapper;
    [SerializeField][Tooltip("the walkingNPC you want to call to deliver paper")] WalkingNPCController walkingNPC;
    [SerializeField][Tooltip("the destination for walkingNPC to pick up paper")] Transform NPCdestination;


    //Invoke when OnSnap triggered
    public void CallNPC()
    {
        //Check if subject is null or if the Holdable component is already disabled, make sure it won't be triggered twice
        if (snapper.subject == null || !snapper.subject.enabled) return;

        Holdable subject = snapper.subject;
        Transform paper = subject.transform;
        
        paper.GetComponent<Rigidbody>().useGravity = false;
        paper.GetComponent<Rigidbody>().isKinematic = true;
        paper.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        paper.GetComponent<Holdable>().enabled = false;


        walkingNPC.StartPickUp(paper,NPCdestination);
        snapper.SoftReleaseSubject();
    }
}