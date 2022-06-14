using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PN_Node 
{
    #region F/P
    Vector3 mPosition = Vector3.zero;
    Color mFloor = Color.white;
    Color mMountain = Color.black;
    public Vector3 Position => mPosition;
    public Color FloorColor
    {
        set => mFloor = value;
    }

    public Color MountainColor
    {
        set => mMountain = value;
    }

    #endregion

    #region Constructor
    public PN_Node(float _x, float _y, float _z)
    {
        mPosition = new Vector3(_x,_y,_z);
    }

    #endregion

    #region Custom Method

    public void OnDrawNode(float _size)
    {
        Gizmos.color = Color.Lerp(mFloor,mMountain,Position.y);
         Gizmos.DrawSphere(mPosition,_size);
    }

    #endregion

}
