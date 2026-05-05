using UnityEngine;
public class PaperMovingSystem : MonoBehaviour
{
    public Transform paperContainer;
    public Transform paperScaler;
    [SerializeField]float distance = 0.01f;
    [SerializeField]float scaleAmount = 0.075f;
    public Transform targetPosition;
    Transform paper;
    

    void Start()
    {
        GetPaper();
        UpdateCollider();
        if(targetPosition==null)
            targetPosition = transform.Find("targetPosition");

    }

    public void KnobTwist()
    {
        if (paperContainer && paperScaler)
        {
            PaperMoveUp();
            PaperYScale();
            UpdateCollider();
        }
        else
        {
            Debug.LogError("There is no paper!! PLEASE put a new paper!");
        }
    }

    void PaperMoveUp()
    {
        Transformer moveUp = paperContainer.GetComponent<Transformer>();
        moveUp.MoveUp(distance);
    }

    void PaperYScale()
    {

        Transformer scaleY = paperScaler.GetComponent<Transformer>();
        scaleY.ScaleYAxis(scaleAmount);
    }

    public void GetPaper()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Transformer>())
            {
                paperContainer = child;
            }
        }

        foreach (Transform child in paperContainer)
        {
            if (child.GetComponent<Transformer>())
            {
                paperScaler = child;
                paper = paperScaler.GetChild(0);
            }
        }
        
    }

    public void UpdateCollider()
    {
        BoxCollider collider = paperContainer.GetComponent<BoxCollider>();
        BoxCollider boxCollider = paper.GetComponent<BoxCollider>();

        Vector3 worldcenter = boxCollider.transform.TransformPoint(boxCollider.center);
        Vector3 localCenter = collider.transform.InverseTransformPoint(worldcenter);
        Vector3 worldSize = Vector3.Scale(boxCollider.size, boxCollider.transform.lossyScale);

        // transfer to local size
        Vector3 localSize = new Vector3(
            (worldSize.x / collider.transform.lossyScale.x),
            (worldSize.y / collider.transform.lossyScale.y),
            (worldSize.z / collider.transform.lossyScale.z)* 1.1f
        );


        collider.center = localCenter;
        collider.size = localSize;
    }
}
