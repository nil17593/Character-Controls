using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarManager : MonoBehaviour
{
    public ProgressBar bar;
    public int value = 60;

    void Update()
    {
        bar.BarValue = value;
    }
}
