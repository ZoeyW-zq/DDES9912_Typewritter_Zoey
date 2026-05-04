using System.Collections.Generic;
using UnityEngine;

public class KeyboardMap : MonoBehaviour
{
    [SerializeField] Transform[] keysTransform;
    [SerializeField] MoveToPress MoveToPress;
    [SerializeField][Tooltip("Used to distinguish between the left and right hand areas")]
    Transform centerPosition;

    //which hand to type
    public enum Hand
    {
        Left,
        Right
    }
    Dictionary<char, (Transform, Hand)> keyLocationMap;

    [Tooltip("The words you want the NPC to type")]
    public string text2Type;
    public Dictionary<char, (Transform, Hand)> key2Press;

    
    void Start()
    {
        //AddChildTransform();
        char[] keys = new char[]
        {
        '1','2','3','4','5','6','7','8','9','0','⅜','_',
        'Q','W','E','R','T','Y','U','I','O','P','⅝',
        'A','S','D','F','G','H','J','K','L', ';', ':',
        'Z','X','C','V','B','N','M', ',', '.', '/',' ',
        };

        //Store the transform and hand corresponding to the character
        keyLocationMap = new Dictionary<char, (Transform, Hand)>();
        for (int i = 0;i<keys.Length; i++)
        {
            if (keysTransform[i].position.x < centerPosition.position.x)
            {
                keyLocationMap.Add(keys[i], (keysTransform[i],Hand.Left));
            }
            else
            {
                keyLocationMap.Add(keys[i], (keysTransform[i], Hand.Right));
            }
        }

        //Store the transform and hand corresponding to specific characters
        key2Press = new Dictionary<char, (Transform, Hand)>();
        for (int i = 0; i<text2Type.Length; i++)
        {
            key2Press[text2Type[i]] = (keyLocationMap[text2Type[i]].Item1, keyLocationMap[text2Type[i]].Item2);
        }
        if (MoveToPress)
        {
            StartCoroutine(MoveToPress.Type());
        }
        
    }

    //get transform of each key
    void AddChildTransform()
    {
        keysTransform = new Transform[transform.childCount];
        for (int i = 0; i < (transform.childCount); i++)
        {
            keysTransform[i] = transform.GetChild(i);
        }
    }

}
