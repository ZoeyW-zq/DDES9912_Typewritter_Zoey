using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaperCounter : MonoBehaviour
{
    string textString;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    int count;

    InteractableTrigger interactableTrigger;


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
