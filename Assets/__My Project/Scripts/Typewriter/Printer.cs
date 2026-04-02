using UnityEngine;

public class Printer : MonoBehaviour
{
    [Tooltip("Position that instantiate the characterPrefab")]
    public Transform hitPoint;           
    public Transform characterParent;   
    public GameObject characterPrefab;  

    public void PrintCharacter(string letter)
    {
        GameObject c = Instantiate(
            characterPrefab,
            hitPoint.position,
            hitPoint.rotation,
            characterParent
        );

        c.GetComponent<TypeCharacter>().SetCharacter(letter);
    }
}