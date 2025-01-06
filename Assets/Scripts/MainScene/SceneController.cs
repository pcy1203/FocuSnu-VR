using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    // distractions
    public Toggle lightBlinkToggle; // Reference to the Toggle component
    public Toggle musicToggle; // Reference to the Toggle component
    public Toggle floatingBoxToggle; // Reference to the Toggle component
    public Toggle NPCToggle; // Reference to the Toggle component
    public Toggle ballToggle;
    public static bool distractMusic = false;
    public static bool distractFloating = false;
    public static bool distractNPC = false;
    public static bool distractHaptic = false;
    public static bool distractLightBlink = false;
    public static bool distractBall = false;
    
    public static Vector3 cameraOffset = new Vector3(0, 0, 0);
    public Transform xrRig;
    public Transform vrCamera;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Debug : "+cameraOffset);
        xrRig.transform.localPosition = Vector3.zero - cameraOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTask1Scene()
    {
        distractMusic = musicToggle.isOn;
        distractFloating = floatingBoxToggle.isOn;
        distractNPC = NPCToggle.isOn;
        distractLightBlink = NPCToggle.isOn;
        SceneController.cameraOffset = vrCamera.transform.localPosition;
        SceneManager.LoadScene("Task1Scene_CardMatching");
    }

    public void LoadTask2Scene()
    {
        distractMusic = musicToggle.isOn;
        distractFloating = floatingBoxToggle.isOn;
        distractNPC = NPCToggle.isOn;
        distractLightBlink = NPCToggle.isOn;
        SceneController.cameraOffset = vrCamera.transform.localPosition;
        SceneManager.LoadScene("Task2Scene_RememberLocation");
    }

    public void LoadTask3Scene()
    {
        distractMusic = musicToggle.isOn;
        distractFloating = floatingBoxToggle.isOn;
        distractNPC = NPCToggle.isOn;
        distractLightBlink = NPCToggle.isOn;
        SceneController.cameraOffset = vrCamera.transform.localPosition;
        SceneManager.LoadScene("Task3Scene_PictureOrdering");
    }

    public void LoadTask4Scene()
    {
        distractMusic = musicToggle.isOn;
        distractFloating = floatingBoxToggle.isOn;
        distractNPC = NPCToggle.isOn;
        distractLightBlink = NPCToggle.isOn;
        SceneController.cameraOffset = vrCamera.transform.localPosition;
        SceneManager.LoadScene("Task4Scene_ObstacleAvoidance");
    }
    
    public void LoadTask5Scene()
    {
        distractMusic = musicToggle.isOn;
        distractFloating = floatingBoxToggle.isOn;
        distractNPC = NPCToggle.isOn;
        distractLightBlink = lightBlinkToggle.isOn;
        distractBall = ballToggle.isOn;
        SceneController.cameraOffset = vrCamera.transform.localPosition;
        SceneManager.LoadScene("Task5Scene_TableTennis");
    }
}
