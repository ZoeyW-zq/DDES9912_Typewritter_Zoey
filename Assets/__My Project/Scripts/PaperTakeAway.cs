using UnityEngine;

public class PaperTakeAway : MonoBehaviour
{
    [SerializeField]
    Transform paperScaler;
    float scaleAmount = 4.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        paperScaler = transform.Find("PaperScaler");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeAway()
    {
        transform.SetParent(null);
        paperScaler.transform.localScale = new Vector3(1, scaleAmount,1);
    }
}
