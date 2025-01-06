using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static float task1Time = 200f;
    public static float task2Time = 60f;
    public static float task3Time = 200f;
    public static float task4Time = 200f;
    public static float task5Time = 60f;

    public Dropdown task1Dropdown;
    public Dropdown task2Dropdown;
    public Dropdown task3Dropdown;
    public Dropdown task4Dropdown;
    public Dropdown task5Dropdown;


    void Start()
    {
        task1Dropdown.onValueChanged.AddListener(delegate { UpdateTaskTime(1, task1Dropdown.options[task1Dropdown.value].text); });
        task2Dropdown.onValueChanged.AddListener(delegate { UpdateTaskTime(2, task2Dropdown.options[task2Dropdown.value].text); });
        task3Dropdown.onValueChanged.AddListener(delegate { UpdateTaskTime(3, task3Dropdown.options[task3Dropdown.value].text); });
        task4Dropdown.onValueChanged.AddListener(delegate { UpdateTaskTime(4, task4Dropdown.options[task4Dropdown.value].text); });
        task5Dropdown.onValueChanged.AddListener(delegate { UpdateTaskTime(5, task5Dropdown.options[task5Dropdown.value].text); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTaskTime(int taskNumber, string dropdownText)
    {
        float selectedTime;
        if (float.TryParse(dropdownText.TrimEnd('s'), out selectedTime))
        {
            switch (taskNumber)
            {
                case 1:
                    task1Time = selectedTime;
                    break;
                case 2:
                    task2Time = selectedTime;
                    break;
                case 3:
                    task3Time = selectedTime;
                    break;
                case 4:
                    task4Time = selectedTime;
                    break;
                case 5:
                    task5Time = selectedTime;
                    break;
            }
        }
        else
        {
            Debug.LogError("Invalid time format in dropdown: " + dropdownText);
        }
    }

    public void LoadTask1Scene()
    {
        SceneManager.LoadScene("Task1Scene");
    }

    public void LoadTask2Scene()
    {
        SceneManager.LoadScene("Task2Scene");
    }
    
    public void LoadTask3Scene()
    {
        SceneManager.LoadScene("Task3Scene");
    }

    public void LoadTask4Scene()
    {
        SceneManager.LoadScene("Task4Scene");
    }

    public void LoadTask5Scene()
    {
        SceneManager.LoadScene("Task5Scene");
    }
}
