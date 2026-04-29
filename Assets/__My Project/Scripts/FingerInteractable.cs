using UnityEngine;

public class FingerInteractable : MonoBehaviour
{
    [SerializeField]
    InteractableGeneral subject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "InteractionBox")
        {
            subject = other.GetComponent<InteractableGeneral>();
            subject.onPrimaryInteract.Invoke();
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (subject != null)
        {
            subject.onPrimaryInteractLift.Invoke();
        }
    }
}
