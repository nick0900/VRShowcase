using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expander : MonoBehaviour
{
    [SerializeField] float MaxScale = 50.0f;
    [SerializeField] float StepScale = 0.2f;

    void Update()
    {
        this.transform.localScale *= 1.0f + StepScale * (MaxScale - this.transform.localScale.y) / MaxScale;
    }
}
