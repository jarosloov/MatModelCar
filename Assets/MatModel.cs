using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatModel : MonoBehaviour
{
    private float k1, k2, k3, k4, l1, l2, l3, l4, n1, n2, n3, n4;

    [Header("П-регулятор")]
    [SerializeField] private bool p_regulator_Euler;
    [SerializeField] private bool p_regulator_RungeKutty4;
    [Header("ПД-регулятор")]
    [SerializeField] private bool pd_regulator_Euler;

    [Header("Настройки")]
    public float kp = 0.8f;
    public float kd = 4;
    public float ki = 3;
    public float dt = 0.05f;    
    public float h = 0.05f;     // the same as dt, added just for understanding PK 4
    public float v = 10f;

    [Header("Начальные значения")]
    [SerializeField] private float x_0 = 2;
    [SerializeField] private float y_0 = -1;
    [SerializeField] private float a_o = Mathf.PI / 4;

    public List<float> x, y, a; 

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            ClearingData();
            PID();
        }
    }

    private void StartValues(){
        x.Add(x_0);
        y.Add(y_0);
        a.Add(a_o);
    }

    private void ClearingData(){
        x.Clear();
        y.Clear();
        a.Clear();
    }

    private void P_regulator_Euler(int n){
        a.Add(a[n] + kp * (x[n] - 1) * dt);
        a.Add(a[n] + kp * Mathf.Sqrt(((x[n] - 1) * (x[n] - 1) + (y[n] - (v * dt + y_0)) * (y[n] - (v * dt + y_0))) * dt));
        x.Add(x[n] + Mathf.Cos(a[n]) * dt);
        y.Add(y[n] + Mathf.Sin(a[n]) * dt);
    }

    private void  PD_regulator_Euler(int n){
        if (n > 0){
        a.Add(a[n] + (kp * (x[n] - 1) + kd * ((x[n] - x[n - 1]) / dt) * dt));
        x.Add(x[n] + Mathf.Cos(a[n]) * dt);
        y.Add(y[n] + Mathf.Sin(a[n]) * dt);
        }
        else{
        a.Add(a[n] + kp * (x[n] - 1) * dt);
        x.Add(x[n] + Mathf.Cos(a[n]) * dt);
        y.Add(y[n] + Mathf.Sin(a[n]) * dt);
        }
    }

    private void P_regulator_RungeKutty4(int n){
        k1 = h * Mathf.Cos(a[n]);
        k2 = h * Mathf.Cos(a[n] + k1 / 2);
        k3 = h * Mathf.Cos(a[n] + k2 / 2);
        k4 = h * Mathf.Cos(a[n] + k3 / 2);

        l1 = h * Mathf.Sin(a[n]);
        l2 = h * Mathf.Sin(a[n] + k1 / 2);
        l3 = h * Mathf.Sin(a[n] + k2 / 2);
        l4 = h * Mathf.Sin(a[n] + k3 / 2);

        //функция kp *(x[n] - 1)
        n1 = h * kp * (x[n] - 1);
        n2 = h * kp * (x[n] - 1) + k1 / 2;
        n3 = h * kp * (x[n] - 1) + k2 / 2;
        n4 = h * kp * (x[n] - 1) + k3 / 2;

        x.Add(x[n] + 1 / 6f * (k1 + 2 * k2 + 2 * k3 + k4));
        y.Add(y[n] + 1 / 6f * (l1 + 2 * l2 + 2 * l3 + l4));
        a.Add(a[n] + 1 / 6f * (n1 + 2 * n2 + 2 * n3 + n4));
    }

    private void PID(){
        // добавляем начальные значения в листы 
        StartValues();

        for (int n = 0; n < 1200; n++){
            //P regulator Euler
            if(p_regulator_Euler)
                P_regulator_Euler(n);
           
            // PD_regulator_Euler
            else if(pd_regulator_Euler)
                PD_regulator_Euler(n);

            // P_regulator_RungeKutty4
            else if(p_regulator_RungeKutty4)
                P_regulator_RungeKutty4(n);
        }
    }

    private void OnDrawGizmos() {
        DrawGon();
        if(x.Count <= 1) return;
        for (int i = 0; i < 1200; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(new Vector3(x[i], y[i], 0), new Vector3(x[i+1], y[i+1], 0));
        }
 
    }

    private void DrawGon(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(1, -1, 0),new Vector3(1, 1200, 0));
    }

}

