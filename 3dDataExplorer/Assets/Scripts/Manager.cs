using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Material normalMat;
    public Material highlightMat;
    public GameObject cubePrefab;

    // Keep track of this so the data text can always face the camera
    public static Transform playerTransform;

    private GameObject testArray;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = Camera.main.transform;

        Data[] sampleData = new Data[3];
        sampleData[0] = new Data(1234);
        sampleData[1] = new Data(55555);
        sampleData[2] = new Data(-4);

        testArray = new GameObject();
        testArray.AddComponent<DataArray1D>();
        testArray.GetComponent<DataArray1D>().SetIndices(0, 0);
        testArray.GetComponent<DataArray1D>().InitializeData(sampleData, normalMat, highlightMat, cubePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
