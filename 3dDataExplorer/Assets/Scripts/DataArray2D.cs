using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataArray2D : MonoBehaviour
{
    // This stores the 1D Data array objects
    private GameObject[] array;

    // This is a rectangle of data in the xy plane, contained in a 3d array. So it needs to know its
    // z index of how deep it is within the prism.
    private int z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIndex(int inputZ)
    {
        z = inputZ;
    }

    public void InitializeData(Data[][] data, Material normalMat, Material hoverMat, Material highlightMat, GameObject shellPrefab)
    {
        array = new GameObject[data.Length];

        // Calculate where to put the top array
        float curDataLoc;
        if (data.Length % 2 == 0)
        {
            curDataLoc = Manager.offsetBetweenEntries * (data.Length / 2f - 0.5f);
        }
        else
        {
            curDataLoc = Manager.offsetBetweenEntries * (data.Length / 2f - 0.5f);
        }

        // Put all of the data in the correct positions and initialize them
        for (int i = 0; i < data.Length; i++)
        {
            GameObject dataArray1d = new GameObject();
            dataArray1d.transform.SetParent(transform);
            dataArray1d.transform.localPosition = new Vector3(0, curDataLoc, 0);
            dataArray1d.AddComponent<DataArray1D>();
            dataArray1d.GetComponent<DataArray1D>().SetIndices(i, this.z);
            dataArray1d.GetComponent<DataArray1D>().InitializeData(data[i], normalMat, hoverMat, highlightMat, shellPrefab);
            array[i] = dataArray1d;

            curDataLoc -= Manager.offsetBetweenEntries;
        }
    }

    // This should be called whenever Manager.offsetBetweenEntries changes
    public void UpdateDistanceBetweenData()
    {
        // Calculate where to put the top array
        float curDataLoc;
        if (array.Length % 2 == 0)
        {
            curDataLoc = Manager.offsetBetweenEntries * (array.Length / 2f - 0.5f);
        }
        else
        {
            curDataLoc = Manager.offsetBetweenEntries * (array.Length / 2f - 0.5f);
        }

        // Put all of the data in the correct positions and initialize them
        for (int i = 0; i < array.Length; i++)
        {
            array[i].transform.localPosition = new Vector3(0, curDataLoc, 0);
            array[i].GetComponent<DataArray1D>().UpdateDistanceBetweenData();
            curDataLoc -= Manager.offsetBetweenEntries;
        }
    }

    public GameObject GetArrayAtY(int y)
    {
        return array[y];
    }

    public int Length()
    {
        return array.Length;
    }

    public void UnHighlightAll()
    {
        foreach (GameObject da1d in array)
        {
            da1d.GetComponent<DataArray1D>().UnHighlightAll();
        }
    }
}
