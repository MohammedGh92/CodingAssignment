using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vectors
{

    public static Vector3Int intVectValues(Vector3 vector)
    {
        return new Vector3Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y), Mathf.FloorToInt(vector.z));
    }

}