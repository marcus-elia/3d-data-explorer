using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataText : MonoBehaviour
{
    private TextMeshPro text;
    private int intData = 0;
    private float floatData = 0f;
    private string stringData = null;
    private string dataString = "";

    private int fontSize;
    private int numCharacters;
    private float maxTextWidth;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.LookAt(playerTransform);
    }

    public void SetPlayerTransform(Transform input)
    {
        playerTransform = input;
    }

    public void SetData(int input)
    {
        intData = input;
        dataString = intData.ToString();
        UpdateTextMesh();
    }
    public void SetData(float input)
    {
        floatData = input;
        dataString = floatData.ToString();
        UpdateTextMesh();
    }
    public void SetData(string input)
    {
        stringData = input;
        dataString = stringData;
        UpdateTextMesh();
    }

    private void UpdateTextMesh()
    {
        numCharacters = dataString.Length;
        float characterSize = numCharacters / maxTextWidth;
        fontSize = Mathf.FloorToInt(characterSize);
        text.text = dataString;
        text.fontSize = fontSize;
    }

    public void InitializeText()
    {
        text = new TextMeshPro();
        text.transform.SetParent(transform);
        text.transform.LookAt(playerTransform);
    }
}
