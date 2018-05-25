using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
    [SerializeField] int n;

    // Use this for initialization
    void Start()
    {
        print(Fibonnaci(n));

    }

    float Fibonnaci(int n)
    {
        int f = 0;


        int[] sequence = new int[n];
        
        //holders
        int v = 1;
        int vv = 1;
        int sum = 0;

        for (int i = 0; i < n; i++)
        {
            sum = v + vv;
            v = vv;
            vv = sum;

        }

        return sum;

    }


}
