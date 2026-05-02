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

        Holdable paper = snapper.subject;
        
        walkingNPC.Walking2PickUp(paper.transform,NPCdestination);
        snapper.SoftReleaseSubject();
    }
}