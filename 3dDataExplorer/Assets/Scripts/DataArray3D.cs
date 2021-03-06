using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataArray3D : MonoBehaviour
{
    // This stores the 2D Data array objects
    private GameObject[] array;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeData(Data[][][] data, Material normalMat, Material hoverMat, Material highlightMat, GameObject shellPrefab)
    {
        array = new GameObject[data.Length];

        // Calculate where to put the front array
        float curDataLoc;
        if (data.Length % 2 == 0)
        {
            curDataLoc = -Manager.offsetBetweenEntries * (data.Length / 2f - 0.5f);
        }
        else
        {
            curDataLoc = -Manager.offsetBetweenEntries * (data.Length / 2f - 0.5f);
        }

        // Put all of the data in the correct positions and initialize them
        for (int i = 0; i < data.Length; i++)
        {
            GameObject dataArray2d = new GameObject();
            dataArray2d.transform.SetParent(transform);
            dataArray2d.transform.localPosition = new Vector3(0, 0, curDataLoc);
            dataArray2d.AddComponent<DataArray2D>();
            dataArray2d.GetComponent<DataArray2D>().SetIndex(i);
            dataArray2d.GetComponent<DataArray2D>().InitializeData(data[i], normalMat, hoverMat, highlightMat, shellPrefab);
            array[i] = dataArray2d;

            curDataLoc += Manager.offsetBetweenEntries;
        }
    }

    // This should be called whenever Manager.offsetBetweenEntries changes
    public void UpdateDistanceBetweenData()
    {
        // Calculate where to put the front array
        float curDataLoc;
        if (array.Length % 2 == 0)
        {
            curDataLoc = -Manager.offsetBetweenEntries * (array.Length / 2f - 0.5f);
        }
        else
        {
            curDataLoc = -Manager.offsetBetweenEntries * (array.Length / 2f - 0.5f);
        }

        // Put all of the data in the correct positions
        for (int i = 0; i < array.Length; i++)
        {
            array[i].transform.localPosition = new Vector3(0, 0, curDataLoc);
            array[i].GetComponent<DataArray2D>().UpdateDistanceBetweenData();

            curDataLoc += Manager.offsetBetweenEntries;
        }

    }

            // Getters
            public GameObject GetArrayAtZ(int z)
    {
        return array[z];
    }
    public int Length()
    {
        return array.Length;
    }

    public void UnHighlightAll()
    {
        foreach(GameObject da2d in array)
        {
            da2d.GetComponent<DataArray2D>().UnHighlightAll();
        }
    }
}
