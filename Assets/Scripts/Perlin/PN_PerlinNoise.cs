using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PN_PerlinNoise : MonoBehaviour
{
    #region F/P

    [SerializeField, Header("Gradient")] int IYMAX = 100;
    [SerializeField] int IXMAX = 100;
    [SerializeField, Header("Seed")] int mSeed = 0;
    Vector2[,] mGradient;

    #endregion

    #region Unity Method

    private void Awake()
    {
        mGradient = new Vector2[IYMAX, IXMAX];
    }

    #endregion

    #region Algorithm Perlin noise

    public void FillGradient()
    {
        Random.InitState(mSeed);
        for(int i = 0; i < IYMAX; i++)
        {
            for(int j = 0 ;j < IXMAX; j++)
            {
                mGradient[i, j].x = Random.value;
                mGradient[i, j].y = Random.value;
            }
        }
    }

    float DotGridGradient(int _iX, int _iY, float _x, float _y)
    {
        float dx = _x - (float)_iX;
        float dy = _y - (float)_iY;
        return (dx * mGradient[_iY, _iX].x + dy * mGradient[_iY, _iX].y);
    }

    public float Perlin(float _x, float _y)
    {
        int _x0 = (int)Mathf.Floor(_x);
        int _x1 = _x0 + 1;
        int _y0 = (int)Mathf.Floor(_y);
        int _y1 = _y0 + 1;

        // Determine interpolation weights
        // Could also use higher order polynomial/s-curve here
        float _sx = _x - (float)_x0;
        float _sy = _y - (float)_y0;

        // Interpolate between grid point gradients
        float _n0, _n1, _ix0, _ix1, _value;

        _n0 = DotGridGradient(_x0, _y0, _x, _y);
        _n1 = DotGridGradient(_x1, _y0, _x, _y);
        _ix0 = Lerp(_n0, _n1, _sx);

        _n0 = DotGridGradient(_x0, _y1, _x, _y);
        _n1 = DotGridGradient(_x1, _y1, _x, _y);
        _ix1 = Lerp(_n0, _n1, _sx);

        _value = Lerp(_ix0, _ix1, _sy);

        return _value;
    }

    float Lerp(float _a0, float _a1, float _w)
    {
         return (1.0f - _w) * _a0 + _w * _a1;
    }

    #endregion
}
