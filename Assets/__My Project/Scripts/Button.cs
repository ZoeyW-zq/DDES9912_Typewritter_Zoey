using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] GameObject paperPrefab;
    [SerializeField]
    Transform paperMovingSystem;
    string paperNameClone = "PaperContainer(Clone)";
    string paperName = "PaperContainer";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewPaper()
    {
        if ((paperMovingSystem.Find(paperName))|| (paperMovingSystem.Find(paperNameClone)))
        {
            Debug.Log("There is already paper.");
        }
        else
        {
            paperMovingSystem.GetComponent<PaperMovingSystem>().targetPosition.localPosition = Vector3.zero;
            GameObject paperContainer = Instantiate(paperPrefab, paperMovingSystem.position, paperMovingSystem.rotation, paperMovingSystem);
            paperMovingSystem.GetComponent<PaperMovingSystem>().GetPaper();
        }
           
    }
}
