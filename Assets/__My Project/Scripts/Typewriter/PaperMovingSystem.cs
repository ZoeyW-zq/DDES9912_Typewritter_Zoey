using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Transformation;

public class PaperMovingSystem : MonoBehaviour
{
    public Transform paperContainer;
    public Transform paperScaler;
    public float distance = 0.01f;
    public float scaleAmount = 0.075f;
    Transform paper;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetPaper();
        UpdateCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void UpdateCollider1()
    {
        BoxCollider collider = paperContainer.GetComponent<BoxCollider>();
        Renderer renderer = paper.GetComponent<MeshRenderer>();
        Bounds bounds = renderer.bounds;

        Vector3 center = collider.transform.InverseTransformPoint(bounds.center);
        Vector3 size = collider.transform.InverseTransformPoint(bounds.size);

        collider.center = center;
        collider.size = size;  
    }

    public void UpdateCollider()
    {
        BoxCollider collider = paperContainer.GetComponent<BoxCollider>();
        BoxCollider boxCollider = paper.GetComponent<BoxCollider>();

        Vector3 worldcenter = boxCollider.transform.TransformPoint(boxCollider.center);
        Vector3 localCenter = collider.transform.InverseTransformPoint(worldcenter);
        Vector3 worldSize = Vector3.Scale(boxCollider.size, boxCollider.transform.lossyScale);

        // 4️⃣ 转换到 self 的本地 size
        Vector3 localSize = new Vector3(
            (worldSize.x / collider.transform.lossyScale.x),
            (worldSize.y / collider.transform.lossyScale.y),
            (worldSize.z / collider.transform.lossyScale.z)* 1.1f
        );


        collider.center = localCenter;
        collider.size = localSize;
    }
}
