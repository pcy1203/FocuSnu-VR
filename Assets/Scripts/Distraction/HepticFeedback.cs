
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedback : MonoBehaviour
{
    [Range(0, 1)]
    public float intensity;
    public float duration;
    // public XRBaseInteractable interactable;


    void Start()
    {
        // Set the first random interval
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.activated.AddListener(TriggerHaptic);
    }
    public void TriggerHaptic(BaseInteractionEventArgs eventArgs){
        if (eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor){
            TriggerHaptic(controllerInteractor.xrController);
        }
    }
    public void TriggerHaptic(XRBaseController controller)
    {
        controller.SendHapticImpulse(intensity, duration);
    }
    public void TriggerHapticFeedback(XRBaseController controller)
    {
        TriggerHaptic(controller);
    }
}
