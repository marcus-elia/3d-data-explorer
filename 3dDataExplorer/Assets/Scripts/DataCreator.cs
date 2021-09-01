using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCreator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This makes the t_b(n, k) triangles from my dissertation
    public static Data[][][] MakeTriangleData(int maxB, int maxN)
    {
        Data[][][] array3d = new Data[maxB - 1][][];
        for(int b = 2; b <= maxB; b++)
        {
            Data[][] array2d = new Data[maxN][];
            for(int n = 1; n <= maxN; n++)
            {
                Data[] array1d = new Data[n];
                for(int k = 1; k <= n; k++)
                {
                    array1d[k - 1] = new Data(T(b, n, k));
                }
                array2d[n - 1] = array1d;
            }
            array3d[b - 2] = array2d;
        }

        return array3d;
    }

    public static long T(int b, int n, int k)
    {
        if(n < 1 || k < 1 || k > n)
        {
            return 0;
        }
        else if(k == 1 || k == n)
        {
            return 1;
        }
        return T(b, n - 1, k - 1) + (k + b - 2)* (k + b - 2) * T(b, n - 1, k);
    }
}
