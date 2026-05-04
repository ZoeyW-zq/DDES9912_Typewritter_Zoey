using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaperCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    string textString;
    int count;
    InteractableTrigger interactableTrigger; //Area trigger prefab from EZPZ Toolkit


    private void Start()
    {
        count = 0;
        textString = "Current Paper: ";
        if(text == null)
            text = transform.GetComponentInChildren<TextMeshProUGUI>();
        text.text = textString+count.ToString();

        interactableTrigger = transform.GetComponent<InteractableTrigger>();
    }

    public void UpdateCount()
    {
        count = interactableTrigger.contactList.Count; 
        text.text = textString+count.ToString();
    }
}
