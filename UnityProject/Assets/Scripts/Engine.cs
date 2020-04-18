using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine
{
    public static float LinearInterpolation(float t, float input_min, float input_max, float output_min, float output_max) {

        return (t - input_min) / (input_max - input_min) * (output_max - output_min) + output_min;
    }
}
