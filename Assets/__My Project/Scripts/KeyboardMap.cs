using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMap : MonoBehaviour
{
    Dictionary<char, Transform> keyLocationMap;

    [SerializeField]
    Transform[] keysTransform;

    public string text2Print;
    public Vector3[] key2Press;

    public MoveToPress MoveToPress;

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
        keyLocationMap = new Dictionary<char, Transform>();
        for (int i = 0;i<keys.Length; i++)
        {
            keyLocationMap.Add(keys[i], keysTransform[i]);
        }
        foreach (char key in text2Print)
        {
            Debug.Log("key:" + key + "   Transform:" + keyLocationMap[key].position);
        }

        key2Press = new Vector3[text2Print.Length];
        for (int i = 0; i<text2Print.Length; i++)
        {
            key2Press[i] = keyLocationMap[text2Print[i]].position;
            //Debug.Log(key2Press[i]);
        }

        StartCoroutine(MoveToPress.MoveFinger());
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
