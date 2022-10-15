using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatModel : MonoBehaviour
{
    public float kp = 0.8f;
    public float kd = 4;
    public float ki = 3;
    public float dt = 0.05f;

    public const float x_0 = 2;
    public const float y_0 = -1;
    public const float a_o = Mathf.PI / 4;

    public List<float> x, y,a; 

    private void Start() {
        // добавляем начальные значения в листы 
        x.Add(x_0);
        y.Add(y_0);
        a.Add(a_o);

        for (int n = 0; n < 600; n++)
        {
            a.Add(a[n] + kp * (x[n] - 1) * dt);
            x.Add(x[n] + Mathf.Cos(a[n]) * dt);
            y.Add(y[n] + Mathf.Sin(a[n]) * dt);
        }
    }

    private void OnDrawGizmos() {
        if(x.Count <= 1) return;
        for (int i = 0; i < 500; i++)
        {
            Gizmos.DrawLine(new Vector3(x[i], y[i], 0), new Vector3(x[i+1], y[i+1], 0));
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(1,1,0),new Vector3(1, 1000, 0));
    }

}

