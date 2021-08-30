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
    private Material hoverMat;
    private Material highlightMat;

    private bool isHovered = false;
    private bool isHighlighted = false;

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

    public void SetMaterials(Material inputMat, Material inputHoverMat, Material inputHighlightMat)
    {
        normalMat = inputMat;
        hoverMat = inputHoverMat;
        highlightMat = inputHighlightMat;
    }

    public void SetShellPrefab(GameObject shapePrefab)
    {
        shell = Instantiate(shapePrefab);
        shell.transform.SetParent(transform);
        shell.transform.localPosition = Vector3.zero;
        shell.GetComponent<Renderer>().material = normalMat;

        // This is the hack to get the DataPolyhedron when a raycast hits the prefab's collider
        shell.GetComponent<ReferenceToPolyhedron>().SetPolyhedron(this);
    }

    public void Highlight()
    {
        shell.GetComponent<Renderer>().material = highlightMat;
        isHighlighted = true;
        isHovered = false;
    }
    public void UnHighlight()
    {
        shell.GetComponent<Renderer>().material = normalMat;
        isHighlighted = false;
        isHovered = true;
    }
    public void Hover()
    {
        shell.GetComponent<Renderer>().material = hoverMat;
        isHovered = true;
    }

    // Getters
    public bool IsHighlighted()
    {
        return isHighlighted;
    }
    public bool IsHovered()
    {
        return isHovered;
    }
}
