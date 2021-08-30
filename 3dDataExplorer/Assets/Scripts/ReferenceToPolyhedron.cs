using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This silly class is my hacky way of letting a raycast hit the box collider on the
// prefab and reference the DataPolyhedron object. 
public class ReferenceToPolyhedron : MonoBehaviour
{
    private DataPolyhedron polyhedron;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPolyhedron(DataPolyhedron input)
    {
        polyhedron = input;
    }
    public DataPolyhedron GetPolyhedron()
    {
        return polyhedron;
    }
}
