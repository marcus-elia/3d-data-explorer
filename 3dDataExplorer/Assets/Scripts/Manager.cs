using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShapeType { Cube, Sphere };

public class Manager : MonoBehaviour
{
    public static float HIGHLIGHT_LINE_TOLERANCE = 0.01f;

    public Material normalMat;
    public Material hoverMat;
    public Material highlightMat;

    // The shells
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    // Keep track of whether we are using a ProBuilder solid. Those need offsetting
    // because they aren't centered properly
    public static bool proBuilder;

    // Need the camera for raycasting
    public new Camera camera;

    // Keep track of datas that are highlighted
    private DataPolyhedron hovered;
    private DataPolyhedron highlighted;
    private bool lineIsHighlighted = false;

    // The distance between the centers of two adjacent data
    public static float offsetBetweenEntries = 2f;

    // Keep track of this so the data text can always face the camera
    public static Transform playerTransform;

    // This is where all the stuff is
    private GameObject mainArray3d;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = Camera.main.transform;

        Data[][] sampleData = new Data[3][];
        sampleData[0] = new Data[3] { new Data(1), new Data(2), new Data(3) };
        sampleData[1] = new Data[4] { new Data(111), new Data(67), new Data(-9.8f), new Data("hey") };
        sampleData[2] = new Data[1] { new Data(3.14f) };

        Data[][][] triangles = DataCreator.MakeTriangleData(10, 6);

        // Figure out which shape the user chose
        GameObject chosenShellPrefab;
        if(StartMenuHandler.shapeType == ShapeType.Sphere)
        {
            chosenShellPrefab = spherePrefab;
            proBuilder = true;
        }
        else
        {
            chosenShellPrefab = cubePrefab;
            proBuilder = false;
        }

        // Create the object
        mainArray3d = new GameObject();
        mainArray3d.AddComponent<DataArray3D>();
        mainArray3d.GetComponent<DataArray3D>().InitializeData(triangles, normalMat, hoverMat, highlightMat, chosenShellPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
        ReactToClick();
        ReactToMouseWheel();
    }

    public void Raycast()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            // This is my annoying hack to get the DataPolyhedron from the prefab
            DataPolyhedron dp = hit.collider.gameObject.GetComponent<ReferenceToPolyhedron>().GetPolyhedron();
            if (hovered != null && !hovered.IsHighlighted())
            {
                hovered.UnHighlight();
            }
            if(!dp.IsHighlighted())
            {
                dp.Hover();
            }
            hovered = dp;
        }
    }

    public void ReactToClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // If a line is already highlighted, unhighlight it
            if(this.lineIsHighlighted)
            {
                this.UnHighlightAll();
                highlighted = null;
            }
            // If not looking at a polyhedron, do nothing
            if (hovered == null)
            {
                return;
            }
            // If nothing is highlighted, highlight this
            else if (highlighted == null)
            {
                hovered.Highlight();
                highlighted = hovered;
            }
            // If only one is highlighted and the user clicks on it
            else if(hovered == highlighted)
            {
                highlighted.UnHighlight();
                highlighted.Hover();
                highlighted = null;
            }
            // Otherwise, one is already highlighted, and this is the other.
            // Highlight the line containing them.
            else
            {
                this.HighlightLineBetween(hovered, highlighted);
                this.lineIsHighlighted = true;
            }
        }
    }

    public void UnHighlightAll()
    {
        mainArray3d.GetComponent<DataArray3D>().UnHighlightAll();
        this.lineIsHighlighted = false;
    }

    // This function is based on an old project I did
    // https://github.com/marcus-elia/integer-sequence-polyhedron/blob/master/numberCubePolyhedron.cpp
    public void HighlightLineBetween(DataPolyhedron dp1, DataPolyhedron dp2)
    {
        int x1 = dp1.GetX(), y1 = dp1.GetY(), z1 = dp1.GetZ();
        int x2 = dp2.GetX(), y2 = dp2.GetY(), z2 = dp2.GetZ();

        // If the two are in the same plane
        if(z1 == z2)
        {
            DataArray2D da2 = mainArray3d.GetComponent<DataArray3D>().GetArrayAtZ(z1).GetComponent<DataArray2D>();
            // Vertical line
            if (x1 == x2)
            {
                for (int y = 0; y < da2.Length(); y++)
                {
                    DataArray1D da1 = da2.GetArrayAtY(y).GetComponent<DataArray1D>();
                    if (da1.Length() > x1)
                    {
                        da1.GetDataPolyhedronAtX(x1).GetComponent<DataPolyhedron>().Highlight();
                    }
                }
            }
            // Non-vertical line
            else
            {
                float slope = (y2 - y1) / (float)(x2 - x1);
                float yInt = y1 - slope * x1;
                for (int y = 0; y < da2.Length(); y++)
                {
                    DataArray1D da1 = da2.GetArrayAtY(y).GetComponent<DataArray1D>();
                    for (int x = 0; x < da1.Length(); x++)
                    {
                        // Check if the coordinates lie on the line
                        if (Mathf.Abs(slope * x + yInt - y) < 0.01)
                        {
                            da1.GetDataPolyhedronAtX(x).GetComponent<DataPolyhedron>().Highlight();
                        }
                    }
                }
            }
        }
        else  // The line goes across tables
        {
            // We will view both x and y as linear functions of z
            float xSlope = (x2 - x1) / (float)(z2 - z1);
            float xInt = x1 - xSlope * z1;
            float ySlope = (y2 - y1) / (float)(z2 - z1);
            float yInt = y1 - ySlope * z1;
            for (int z = 0; z < mainArray3d.GetComponent<DataArray3D>().Length(); z++)
            {
                DataArray2D da2 = mainArray3d.GetComponent<DataArray3D>().GetArrayAtZ(z).GetComponent<DataArray2D>();
                for (int y = 0; y < da2.Length(); y++)
                {
                    DataArray1D da1 = da2.GetArrayAtY(y).GetComponent<DataArray1D>();
                    for (int x = 0; x < da1.Length(); x++)
                    {
                        if (Mathf.Abs(xSlope * z + xInt - x) < HIGHLIGHT_LINE_TOLERANCE && Mathf.Abs(ySlope * z + yInt - y) < HIGHLIGHT_LINE_TOLERANCE)
                        {
                            da1.GetDataPolyhedronAtX(x).GetComponent<DataPolyhedron>().Highlight();
                        }
                    }
                }
            }
        }
    }

    public void ReactToMouseWheel()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Manager.offsetBetweenEntries += 1;
            mainArray3d.GetComponent<DataArray3D>().UpdateDistanceBetweenData();
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            Manager.offsetBetweenEntries = Mathf.Max(1f, Manager.offsetBetweenEntries - 1);
            mainArray3d.GetComponent<DataArray3D>().UpdateDistanceBetweenData();
        }
    }
}
