using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PaperSnapperVR : MonoBehaviour
{
    XRSocketInteractor socketInteractor;
    [SerializeField][Tooltip("the walkingNPC you want to call to deliver paper")] WalkingNPCController walkingNPC;
    [SerializeField][Tooltip("the destination for walkingNPC to pick up paper")] Transform NPCdestination;
    [SerializeField] Transform paper;


    //Invoke when OnSnap triggered
    public void CallNPC()
    {
        socketInteractor=transform.GetComponentInChildren<XRSocketInteractor>();
        Transform paper = socketInteractor.firstInteractableSelected.transform;
        
        paper.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //paper.GetComponent<Holdable>().enabled = false;


        walkingNPC.StartPickUp(paper, NPCdestination);
    }
}
