using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MoveToPress : MonoBehaviour
{
    public KeyboardMap keyMap;
    public Transform targetPosition;
    public KeyPressMoving press;

    [SerializeField] float pressDistance = 0.1f;
    [SerializeField] float speed = 0.5f;

   

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,targetPosition.position, speed*Time.deltaTime);

    }
     public IEnumerator MoveFinger()
    {
        for (int i = 0; i < keyMap.key2Press.Length; i++)
        {
            Vector3 keyPosition = keyMap.key2Press[i];
            Vector3 pressPosition = keyPosition + Vector3.up * 0.1f;
            targetPosition.position = pressPosition;
            Debug.Log(targetPosition.position);
            yield return new WaitForSeconds(1);
            Press();
            yield return new WaitForSeconds(0.5f);
            Lift();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Press()
    {
        targetPosition.position += Vector3.down * 0.1f;
        Debug.Log(targetPosition.position);
    }

    void Lift()
    {
        targetPosition.position += Vector3.up *0.1f;
    }
}
