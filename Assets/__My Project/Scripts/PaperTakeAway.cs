using UnityEngine;

public class PaperTakeAway : MonoBehaviour
{
    [SerializeField]
    Transform paperScaler;
    float scaleAmount = 4.5f;
    PaperMovingSystem movingSystem;
    bool hasUpdate = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        paperScaler = transform.Find("PaperScaler");
        movingSystem = GetComponentInParent<PaperMovingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeAway()
    {
        transform.SetParent(null);
        paperScaler.transform.localScale = new Vector3(1, scaleAmount,1);

        transform.GetComponent<Transformer>().enabled = false;
        paperScaler.GetComponent<Transformer>().enabled = false;

        if (!hasUpdate)
        {
            movingSystem.UpdateCollider();
            DisconnectedFromMovingSystem();

            hasUpdate = true;
        }
        
    }

    void DisconnectedFromMovingSystem()
    {
        movingSystem.paperContainer=null;
        movingSystem.paperScaler=null;
    }


}
