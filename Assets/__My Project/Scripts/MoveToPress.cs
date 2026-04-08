using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR;

public class MoveToPress : MonoBehaviour
{
    public KeyboardMap keyMap;
    public Transform targetPositionRightHand;
    public Transform targetPositionLeftHand;
    public Transform RigRightHand;
    public Transform RigLeftHand;
    [SerializeField] float speed = 0.5f;
    
   

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RigRightHand.position = Vector3.MoveTowards(RigRightHand.position,targetPositionRightHand.position, speed*Time.deltaTime);
        RigLeftHand.position = Vector3.MoveTowards(RigLeftHand.position, targetPositionLeftHand.position, speed * Time.deltaTime);

    }
     public IEnumerator MoveFinger()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < keyMap.text2Print.Length; i++)
        {
            var keyInfo = keyMap.key2Press[keyMap.text2Print[i]];
            Vector3 keyPosition = keyInfo.Item1.position;
            Vector3 pressPosition = keyPosition + Vector3.up * 0.1f;
            if(keyInfo.Item2 == KeyboardMap.Hand.Left)
            {
                targetPositionLeftHand.position = pressPosition;
                yield return new WaitForSeconds(1);
                Press(targetPositionLeftHand);
                yield return new WaitForSeconds(0.2f);
                Lift(targetPositionLeftHand);
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                targetPositionRightHand.position = pressPosition;
                yield return new WaitForSeconds(1);
                Press(targetPositionRightHand);
                yield return new WaitForSeconds(0.2f);
                Lift(targetPositionRightHand);
                yield return new WaitForSeconds(0.2f);
            }

            
        }
    }

    void Press(Transform hand)
    {
        hand.position += Vector3.down * 0.07f;
    }

    void Lift(Transform hand)
    {
        hand.position += Vector3.up *0.07f;
    }
    
   
}
