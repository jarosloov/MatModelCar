using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatModel : MonoBehaviour
{
    // Моменты энерции
    public float i_xx, i_zz, i_xz;
    // Прокции скоросте на оси
    public float speed_x, speed_y;
    // Масса автомобиля
    public const int mass = 1500;
    // Производные по времени от скорости
    public float d_speed_x, d_speed_y;
    // Угловое ускорение относительно оси Z_C
    public float d_angle_speed_z;
    // Продольные силы, действующие на колеса автомобиля
    // тормозящие силы или силы, приведенные к двигателю и ускоряющие движение
    public float t_x1_l, t_x2_l, t_x1_r, t_x2_r;
    // Боковые силы, действующие на колеса автомобиля 


}
