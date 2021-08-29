using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum DataType { Int, Float, String };

public struct Data
{
    public int intData;
    public float floatData;
    public string stringData;
    public DataType dataType;

    public Data(int input)
    {
        intData = input;
        floatData = 0f;
        stringData = null;
        dataType = DataType.Int;
    }
    public Data(float input)
    {
        intData = 0;
        floatData = input;
        stringData = null;
        dataType = DataType.Float;
    }
    public Data(string input)
    {
        intData = 0;
        floatData = 0f;
        stringData = input;
        dataType = DataType.String;
    }

    public string GetString()
    {
        if(dataType == DataType.Int)
        {
            return intData.ToString();
        }
        else if(dataType == DataType.Float)
        {
            return floatData.ToString();
        }
        else
        {
            return stringData;
        }
    }
}

public class DataText : MonoBehaviour
{
    private TextMeshPro text;
    private Data data;
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

    public void SetData(Data inputData)
    {
        data = inputData;
        dataString = data.GetString();
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
