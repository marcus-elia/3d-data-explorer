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

    // Three versions of this since the data could be int, float, or string
    public void InitializeText(int input)
    {
        text = new GameObject();
        text.AddComponent<DataText>();
        text.transform.SetParent(transform);
        text.GetComponent<DataText>().SetData(input);
    }
    public void InitializeText(float input)
    {
        text = new GameObject();
        text.AddComponent<DataText>();
        text.transform.SetParent(transform);
        text.GetComponent<DataText>().SetData(input);
    }
    public void InitializeText(string input)
    {
        text = new GameObject();
        text.AddComponent<DataText>();
        text.transform.SetParent(transform);
        text.GetComponent<DataText>().SetData(input);
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
