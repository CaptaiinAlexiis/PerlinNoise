using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PN_Grid : MonoBehaviour
{
    #region Event 

    public event Action<float> OnDrawGrid = null;

    #endregion

    #region F/P

    [SerializeField, Header("Size grid")] int mSizeX = 1;
    [SerializeField] int mSizeZ = 1;
    [SerializeField, Header("Size sphere debug"), Range(0.1f, 1.0f)] float mSizeSphere = 0.1f;
    [SerializeField,Range(0.1f,1.0f),Header("Spacing")] float mPerlinSpacing = 0.1f;
    [SerializeField, Range(0.0f, 100.0f)] float mSpacingXZ = 1.0f;
    [SerializeField, Range(0.0f, 100.0f)] float mSpacingY = 10.0f;
    [SerializeField, Header("With cube gameobject")] bool mCube = true;
    [SerializeField,Header("Perling")] PN_PerlinNoise mPerlin = null;

    [SerializeField, Header("Color")] Color mFloor = Color.white;
    [SerializeField] Color mMountain = Color.black;

    #endregion

    #region Unity Method

    private void Start()
    {
        if (!mPerlin) return;
        mPerlin.FillGradient();
        GenerateGrid();
    }
    public virtual void OnDrawGizmos() => OnDrawGrid?.Invoke(mSizeSphere);

    #endregion


    #region Custom Method

    void GenerateGrid()
    {
        for (float i = 0; i < mSizeX; i+= mPerlinSpacing)
        {
            for (float j = 0; j < mSizeZ; j+= mPerlinSpacing)
            {
                if (mCube) GenerateCube(i, j);
                else GenerateNode(i,j);
            }
        }
    }

    void GenerateCube(float _x, float _z)
    {
        GameObject _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (!_cube) return;
        _cube.transform.position = new Vector3(_x * mSpacingXZ, mPerlin.Perlin(_x, _z) * mSpacingY, _z * mSpacingXZ);
        _cube.GetComponent<Renderer>().material.color = Color.Lerp(mFloor, mMountain, _cube.gameObject.transform.position.y);
    }

    void GenerateNode(float _x, float _z)
    {
        PN_Node _tmp = new PN_Node(_x * mSpacingXZ, mPerlin.Perlin(_x, _z) * mSpacingY, _z * mSpacingXZ);
        _tmp.FloorColor = mFloor;
        _tmp.MountainColor = mMountain;
        OnDrawGrid += (float _v) => { _tmp.OnDrawNode(_v); };
    }

    #endregion
}
