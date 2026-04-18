using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Transformation;

public class PaperMovingSystem : MonoBehaviour
{
    [SerializeField]
    Transform paperContainer;
    [SerializeField]
    Transform paperScaler;
    [SerializeField]
    Transform paper;
    public float distance = 0.01f;
    public float scaleAmount = 0.075f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        }
        else
        {
            GetPaper();
            PaperMoveUp();
            PaperYScale();
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

    void GetPaper()
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

    void UpdateCollider()
    {
        BoxCollider collider = paperContainer.GetComponent<BoxCollider>();
        MeshRenderer renderer = paper.GetComponent<MeshRenderer>();
        collider.size = renderer.bounds.size;
    }
}
