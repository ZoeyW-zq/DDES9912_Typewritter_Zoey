using UnityEditor.Animations;
using UnityEngine;

public class ResetTypewriter : MonoBehaviour
{
    public Transform characterContainer;
    public Transform paperTargetPositioin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void Reset()
    {
        foreach (Transform child in characterContainer)
        {
            Destroy(child.gameObject);
        }

        paperTargetPositioin.localPosition = Vector3.zero;
    }
}
