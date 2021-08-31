using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuHandler : MonoBehaviour
{
    public static AudioChoice audioChoice = AudioChoice.Normal;
    public static ShapeType shapeType = ShapeType.Sphere;
    public TMP_Dropdown musicDropdown;
    public TMP_Dropdown shapeDropdown;
    public GameObject mainMenuPanel;
    public GameObject instructionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuPanel.SetActive(true);
        instructionsPanel.SetActive(false);

        // Add listener for when the value of the Dropdown changes, to take action
        musicDropdown.onValueChanged.AddListener(delegate {
            MusicDropdownValueChanged(musicDropdown);
        });
        shapeDropdown.onValueChanged.AddListener(delegate {
            ShapeDropdownValueChanged(shapeDropdown);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MusicDropdownValueChanged(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                StartMenuHandler.audioChoice = AudioChoice.Normal;
                break;
            case 1:
                StartMenuHandler.audioChoice = AudioChoice.Intense;
                break;
            default:
                StartMenuHandler.audioChoice = AudioChoice.None;
                break;
        }
    }

    public void ShapeDropdownValueChanged(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                StartMenuHandler.shapeType = ShapeType.Sphere;
                break;
            case 1:
                StartMenuHandler.shapeType = ShapeType.Cube;
                break;
            default:
                StartMenuHandler.shapeType = ShapeType.Tetrahedron;
                break;
        }
    }

    public void SwitchToInstructions()
    {
        mainMenuPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }
    public void SwitchToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        instructionsPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
