using UnityEngine;

public class Printer : MonoBehaviour
{
    [Tooltip("Position that instantiate the characterPrefab")]
    public Transform hitPoint;
    public Transform paperMovingSystem;  
    public GameObject characterPrefab;
    [SerializeField]
    Transform characterParent;

    public void PrintCharacter(string letter)
    {
        characterParent = paperMovingSystem.transform.Find("PaperContainer/CharacterContainer");
        GameObject c = Instantiate(
            characterPrefab,
            hitPoint.position,
            hitPoint.rotation,
            characterParent
        );

        c.GetComponent<TypeCharacter>().SetCharacter(letter);
    }
}