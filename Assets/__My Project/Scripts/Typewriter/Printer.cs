using UnityEngine;

public class Printer : MonoBehaviour
{
    [Tooltip("Position that instantiate the characterPrefab")]
    public Transform hitPoint;
    public Transform paperMovingSystem;  
    public GameObject characterPrefab;
    [SerializeField]
    Transform characterParent;

    private void Start()
    {
        //FindCharacterContainer();
    }

    public void PrintCharacter(string letter)
    {
        FindCharacterContainer();
        
        
        GameObject c = Instantiate(
            characterPrefab,
            hitPoint.position,
            hitPoint.rotation,
            characterParent
        );

        c.GetComponent<TypeCharacter>().SetCharacter(letter);
    }

    void FindCharacterContainer()
    {
        if (paperMovingSystem.transform.Find("PaperContainer/CharacterContainer"))
        {
            characterParent = paperMovingSystem.transform.Find("PaperContainer/CharacterContainer");
        }
        else if (paperMovingSystem.transform.Find("PaperContainer(Clone)/CharacterContainer"))
        {
            characterParent = paperMovingSystem.transform.Find("PaperContainer(Clone)/CharacterContainer");
        }
        else
        {
            Debug.LogError("Can't find PaperContainer/CharacterContainer");
        }
    }
}