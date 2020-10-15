using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Materials")]
    public Material buffMat;
    public Material debuffMat;
    public Material defaultMat;

    public GameObject[] anchors;
    public GameObject buffAnchor;
    public AnchorController buff;

    [Header("This is you")]
    public float buffTimer;
    public float debuffTimer;

    void Start()
    {
        anchors = GameObject.FindGameObjectsWithTag("Anchor");
        Buff();
    }

    void Update()
    {
        //  select random anchor
        //  modify the anchor
        // select renderer of core
        // change mat to buff / debuff
        // do the thing
        

        


    }
    public void Buff()
    {
        Debug.Log("Buffin");
        buffAnchor = anchors[(int)Random.Range(0, anchors.Length)];
        buff = buffAnchor.GetComponent<AnchorController>();
        buff.isBuffed = true;
        buff.changeMat(buffMat);        
        Invoke("BuffOutro", buffTimer);
    }

    public void BuffOutro()
    {
        buff.isBuffed = false;
        buff.changeMat(defaultMat);
        Buff();
    }
}
