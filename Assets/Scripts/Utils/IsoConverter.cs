using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoConverter
{

    public static Vector3 IsoVectorConvert(Vector3 vector)
    {
        Quaternion rot = Quaternion.Euler(0, 45, 0);
        Matrix4x4 isoMat = Matrix4x4.Rotate(rot);
        Vector3 res = isoMat.MultiplyPoint3x4(vector);
        return res;
    }

}
