using Unity.VisualScripting;
using UnityEngine;

public class PaperSnapper : MonoBehaviour
{
    [SerializeField] HoldableMagnetSnapper snapper;
    [SerializeField] WalkingNPCController walkingNPC;
    [SerializeField] Transform NPCdestination;

    void Start()
    {

    }

    public void CallNPC()
    {
        if (snapper.subject == null) return;

        Holdable subject = snapper.subject;
        Transform paper = subject.transform;
        paper.SetParent(null);
        paper.GetComponent<Rigidbody>().useGravity = false;
        paper.GetComponent<Rigidbody>().isKinematic = true;
        paper.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        paper.GetComponent<Holdable>().enabled = false;


        walkingNPC.Walking2PickUp(paper,NPCdestination);
        snapper.SoftReleaseSubject();
    }
}