using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Material normalMat;
    public Material highlightMat;
    public GameObject cubePrefab;

    // The distance between the centers of two adjacent data
    public static float offsetBetweenEntries = 2f;

    // Keep track of this so the data text can always face the camera
    public static Transform playerTransform;

    private GameObject testArray;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = Camera.main.transform;

        Data[][] sampleData = new Data[3][];
        sampleData[0] = new Data[3] { new Data(1), new Data(2), new Data(3) };
        sampleData[1] = new Data[4] { new Data(111), new Data(67), new Data(-9.8f), new Data("hey") };
        sampleData[2] = new Data[1] { new Data(3.14f) };

        testArray = new GameObject();
        testArray.AddComponent<DataArray2D>();
        testArray.GetComponent<DataArray2D>().SetIndex(0);
        testArray.GetComponent<DataArray2D>().InitializeData(sampleData, normalMat, highlightMat, cubePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
