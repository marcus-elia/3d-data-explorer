using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataArray1D : MonoBehaviour
{
    // This stores the DataPolyhedron objects
    private GameObject[] array;

    // The distance between the centers of two adjacent data
    public static float offsetBetweenEntries = 2f;

    // This is a horizontal row of a rectangular prism of data. So it needs to know its
    // y and z incides of where it is within the prism.
    private int y, z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIndices(int inputY, int inputZ)
    {
        y = inputY;
        z = inputZ;
    }

    public void InitializeData(Data[] data, Material normalMat, Material highlightMat, GameObject shellPrefab)
    {
        array = new GameObject[data.Length];

        // Calculate where to put the leftmost data
        float curDataLoc;
        if(data.Length % 2 == 0)
        {
            curDataLoc = -offsetBetweenEntries * (0.5f + data.Length / 2f);
        }
        else
        {
            curDataLoc = -offsetBetweenEntries * (data.Length / 2f);
        }

        // Put all of the data in the correct positions and initialize them
        for(int i = 0; i < data.Length; i++)
        {
            GameObject dataEntry = new GameObject();
            dataEntry.transform.SetParent(transform);
            dataEntry.transform.localPosition = new Vector3(curDataLoc, 0, 0);
            dataEntry.AddComponent<DataPolyhedron>();
            dataEntry.GetComponent<DataPolyhedron>().SetMaterials(normalMat, highlightMat);
            dataEntry.GetComponent<DataPolyhedron>().SetShellPrefab(shellPrefab);
            dataEntry.GetComponent<DataPolyhedron>().InitializeText(data[i]);
            dataEntry.GetComponent<DataPolyhedron>().SetIndices(i, this.y, this.z);

            curDataLoc += offsetBetweenEntries;
        }
    }
}
