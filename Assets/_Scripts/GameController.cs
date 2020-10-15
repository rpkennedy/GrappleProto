using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] anchors;

    void Start()
    {
        anchors = GameObject.FindGameObjectsWithTag("Anchor");
    }

    void Update()
    {
        
    }
}
