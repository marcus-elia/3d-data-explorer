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

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Make the text face the player, but calling transform.lookAt makes it backwards.
        // This fix is from https://answers.unity.com/questions/132592/lookat-in-opposite-direction.html
        text.transform.rotation = Quaternion.LookRotation(text.transform.position - playerTransform.position);
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
        text.text = dataString;
        if(numCharacters < 4)
        {
            text.fontSize = 6;
        }
        else if(numCharacters == 4)
        {
            text.fontSize = 5;
        }
        else if(numCharacters == 5)
        {
            text.fontSize = 4;
        }
        else if(numCharacters == 6)
        {
            text.fontSize = 3;
        }
        else
        {
            text.text = dataString.Substring(0, 3) + "...";
            text.fontSize = 3;
        }
    }

    public void InitializeText()
    {
        text = gameObject.AddComponent<TextMeshPro>();
        text.transform.SetParent(transform);
        text.transform.localPosition = Vector3.zero;
        text.alignment = TextAlignmentOptions.Center;
    }
}
