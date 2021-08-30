using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Material normalMat;
    public Material hoverMat;
    public Material highlightMat;
    public GameObject cubePrefab;
    // Need the camera for raycasting
    public new Camera camera;

    // Keep track of datas that are highlighted
    private DataPolyhedron hovered;
    private GameObject highlighted1;
    private GameObject highlighted2;
    private GameObject highlighted3;

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

        Data[][][] triangles = DataCreator.MakeTriangleData(6, 5);

        testArray = new GameObject();
        testArray.AddComponent<DataArray3D>();
        testArray.GetComponent<DataArray3D>().InitializeData(triangles, normalMat, hoverMat, highlightMat, cubePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
    }

    public void Raycast()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            // This is my annoying hack to get the DataPolyhedron from the prefab
            DataPolyhedron dp = hit.collider.gameObject.GetComponent<ReferenceToPolyhedron>().GetPolyhedron();
            if(hovered != null)
            {
                hovered.UnHighlight();
            }
            dp.Hover();
            hovered = dp;
        }
    }
}
