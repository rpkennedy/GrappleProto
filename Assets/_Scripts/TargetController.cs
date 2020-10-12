using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public GameObject target;
    [Header("Whatever you want m8:")]
    public int rot;

    void Start()
    {
        target = this.gameObject;
    }

    void FixedUpdate()
    {
        target.transform.Rotate(rot,0,0);
    }
}
