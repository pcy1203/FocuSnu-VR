using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class DistractionController : MonoBehaviour
{
    public AudioController audioController;
    public FloatingCubeSpawner floatingCubeSpawner;
    public NPCSpawner NPCSpawner;
    public LightBlinker lightBlinker1; // Reference to the LightBlinker component
    public LightBlinker lightBlinker2; // Reference to the LightBlinker component

    public HapticFeedback2 hapticFeedback;
    public XRBaseController controller;

    public Toggle lightBlinkToggle; // Reference to the Toggle component
    public Toggle musicToggle; // Reference to the Toggle component
    public Toggle floatingBoxToggle; // Reference to the Toggle component
    public Toggle NPCToggle; // Reference to the Toggle component

    public bool distractMusic = false;
    public bool distractFloating = false;
    public bool distractNPC = false;
    public bool distractHaptic = false;
    public bool distractLightBlink = false;
    public int musicthreshold = 50;
    public int hapticThreshold = 0;
    public int lightBlinkThreshold = 50;
    public float blinkInterval = 3.0f; // Interval in seconds between blinks
    private float blinkTimer1;
    private float blinkTimer2;
    private float musicTimer;
    public float musicInterval = 5.0f;

    private int randNum;

    private void Awake()
    {
        // audioController = GetComponent<AudioController>();
        // lightBlinker1 = FindObjectOfType<LightBlinker>();
        // if (lightBlinker1 == null)
        // {
        //     Debug.LogError("LightBlinker not found in the scene.");
        // }
        // lightBlinker2 = FindObjectOfType<LightBlinker>();
        // if (lightBlinker1 == null)
        // {
        //     Debug.LogError("LightBlinker not found in the scene.");
        // }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (distractMusic)
        {
            audioController.PlayMusic();
        }

        if (distractFloating)
        {
        }else{
            floatingCubeSpawner.MakeInvisible();
        }

        if (distractNPC)
        {
        }else{
            NPCSpawner.MakeInvisible();
        }
        if (distractHaptic)
        {
            hapticFeedback.TriggerHaptic(isLeftHand: true, amplitude: 0.5f, duration: 0.2f);

            // hapticFeedback.TriggerHapticFeedback(controller);
        }

        if (distractLightBlink)
        {
            lightBlinker1.StartBlinking();
            lightBlinker2.StartBlinking();
        }  else{
            lightBlinker1.StopBlinking();
            lightBlinker2.StopBlinking();
        }
    }

    // Update is called once per frame
    void Update()
    {
        musicTimer += Time.deltaTime;
        randNum = Random.Range(0, 100);
        if (musicTimer >= musicInterval && distractMusic)
        {
            audioController.PlayMusic();
        }
        // randNum = Random.Range(0, 100);
        // if (randNum > hapticThreshold)
        // {
        //     hapticFeedback.TriggerHapticFeedback(controller);
        // }

        blinkTimer1 += Time.deltaTime;
        if (blinkTimer1 >= blinkInterval && distractLightBlink)
        {
            Debug.Log("Blinking light1");
            blinkTimer1 = 0f;
            lightBlinker1.StartBlinking();
        }

        blinkTimer2 += Time.deltaTime;
        if (blinkTimer2 >= blinkInterval && distractLightBlink)
        {
            Debug.Log("Blinking light2");
            blinkTimer2 = 0f;
            lightBlinker2.StartBlinking();
        }  
        }
    public void SetDistractionState()
    {
        //distractMusic = musicToggle.isOn;
        distractMusic = SceneController.distractMusic;
        //distractFloating = floatingBoxToggle.isOn;
        distractFloating = SceneController.distractFloating;
        if(distractFloating){
            floatingCubeSpawner.MakeVisible();
        }else{
            floatingCubeSpawner.MakeInvisible();
        }
        //distractNPC = NPCToggle.isOn;
        distractNPC = SceneController.distractNPC;
        if(distractNPC){
            NPCSpawner.MakeVisible();
        }else{
            NPCSpawner.MakeInvisible();
        }
        distractHaptic = false;
        //distractLightBlink = lightBlinkToggle.isOn;
        distractLightBlink = SceneController.distractLightBlink;
    }
    

}