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
    public static DataAmount dataAmount = DataAmount.Little;
    public TMP_Dropdown musicDropdown;
    public TMP_Dropdown shapeDropdown;
    public TMP_Dropdown dataAmountDropdown;
    public GameObject mainMenuPanel;
    public GameObject instructionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuPanel.SetActive(true);
        instructionsPanel.SetActive(false);

        musicDropdown.onValueChanged.AddListener(delegate {
            MusicDropdownValueChanged(musicDropdown);
        });
        shapeDropdown.onValueChanged.AddListener(delegate {
            ShapeDropdownValueChanged(shapeDropdown);
        });
        dataAmountDropdown.onValueChanged.AddListener(delegate {
            DataAmountDropdownValueChanged(dataAmountDropdown);
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

    public void DataAmountDropdownValueChanged(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                StartMenuHandler.dataAmount = DataAmount.Little;
                break;
            case 1:
                StartMenuHandler.dataAmount = DataAmount.Normal;
                break;
            case 2:
                StartMenuHandler.dataAmount = DataAmount.Lot;
                break;
            default:
                StartMenuHandler.dataAmount = DataAmount.TooMuch;
                break;
        }
    }

    public static void ResetDefaults()
    {
        StartMenuHandler.dataAmount = DataAmount.Little;
        StartMenuHandler.audioChoice = AudioChoice.Normal;
        StartMenuHandler.shapeType = ShapeType.Sphere;
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
