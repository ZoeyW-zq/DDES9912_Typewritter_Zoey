using UnityEngine;

public class PaperTakeAway : MonoBehaviour
{
    Transform paperScaler;
    float scaleAmount = 4.5f;
    PaperMovingSystem movingSystem;
    bool hasUpdate = false;

    void Start()
    {
        paperScaler = transform.Find("PaperScaler");
        movingSystem = GetComponentInParent<PaperMovingSystem>();
    }


    public void TakeAway()
    {
        transform.SetParent(null);
        paperScaler.transform.localScale = new Vector3(1, scaleAmount,1);//Standardized paper size

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
