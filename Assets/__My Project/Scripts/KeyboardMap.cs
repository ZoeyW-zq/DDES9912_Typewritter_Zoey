using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class KeyboardMap : MonoBehaviour
{
    public enum Hand
    {
        Left,
        Right
    }
    public Dictionary<char, (Transform,Hand)> keyLocationMap;

    [SerializeField]
    Transform[] keysTransform;

    public string text2Print;
    public Dictionary<char, (Transform, Hand)> key2Press;

    public MoveToPress MoveToPress;
    public Transform centerPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        

        key2Press = new Dictionary<char, (Transform, Hand)>();
        for (int i = 0; i<text2Print.Length; i++)
        {
            //key2Press.Add(text2Print[i],keyLocationMap[text2Print[i]]);
            key2Press[text2Print[i]] = (keyLocationMap[text2Print[i]].Item1, keyLocationMap[text2Print[i]].Item2);
            //Debug.Log(key2Press[i]);
        }
        if (MoveToPress)
        {
            StartCoroutine(MoveToPress.MoveFinger());
        }
        
    }

    void AddChildTransform()
    {
        keysTransform = new Transform[transform.childCount];
        for (int i = 0; i < (transform.childCount); i++)
        {
            keysTransform[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
