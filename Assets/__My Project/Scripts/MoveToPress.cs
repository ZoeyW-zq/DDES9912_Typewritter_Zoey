using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MoveToPress : MonoBehaviour
{
    public KeyboardMap keyMap;
    public Transform targetPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveFinger()
    {
        Vector3 keyPosition = keyMap.key2Press[0];
        targetPosition.position = keyPosition;
        Debug.Log(keyPosition);
    }
}
