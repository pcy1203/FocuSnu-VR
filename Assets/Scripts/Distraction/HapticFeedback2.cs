using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedback2 : MonoBehaviour
{
    public XRBaseController leftController;
    public XRBaseController rightController;

    public void TriggerHaptic(bool isLeftHand, float amplitude, float duration)
    {
        if (isLeftHand)
        {
            if (leftController != null)
                leftController.SendHapticImpulse(amplitude, duration);
        }
        else
        {
            if (rightController != null)
                rightController.SendHapticImpulse(amplitude, duration);
        }
    }
}
