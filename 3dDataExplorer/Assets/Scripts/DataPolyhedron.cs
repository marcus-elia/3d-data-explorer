using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPolyhedron : MonoBehaviour
{
    // The actual shell object
    private GameObject shell;

    // The TextMeshPro of the data
    private GameObject text;

    // Materials
    private Material normalMat;
    private Material highlightMat;

    // Keep track of where this is in the array
    private int x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIndices(int inputX, int inputY, int inputZ)
    {
        x = inputX;
        y = inputY;
        z = inputZ;
    }
    public int GetX()
    {
        return x;
    }
    public int GetY()
    {
        return y;
    }
    public int GetZ()
    {
        return z;
    }

    public void InitializeText(Data inputData)
    {
        text = new GameObject();
        text.AddComponent<DataText>();
        text.transform.SetParent(transform);
        text.transform.localPosition = Vector3.zero;
        text.GetComponent<DataText>().InitializeText();
        text.GetComponent<DataText>().SetData(inputData);
        text.GetComponent<DataText>().SetPlayerTransform(Manager.playerTransform);
    }

    public void SetMaterials(Material inputMat, Material inputHighlightMat)
    {
        normalMat = inputMat;
        highlightMat = inputHighlightMat;
    }

    public void SetShellPrefab(GameObject shapePrefab)
    {
        shell = Instantiate(shapePrefab);
        shell.transform.SetParent(transform);
        shell.transform.localPosition = Vector3.zero;
        shell.GetComponent<Renderer>().material = normalMat;
    }

    public void Highlight()
    {
        shell.GetComponent<Renderer>().material = highlightMat;
    }
    public void UnHighlight()
    {
        shell.GetComponent<Renderer>().material = normalMat;
    }
}
