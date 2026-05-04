using UnityEngine;

public class FingerInteractable : MonoBehaviour
{
    InteractableGeneral subject; //usually are keys,levers and reset buttons


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "InteractionBox") //Preventing triggering by paperContainer (the Holdable script is inherited from InteractableGeneral)
        {
            subject = other.GetComponent<InteractableGeneral>(); 
            if (subject != null) 
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
