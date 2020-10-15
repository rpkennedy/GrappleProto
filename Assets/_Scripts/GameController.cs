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

    public GameObject debuffAnchor;
    public AnchorController debuff;

    public GameObject dbLight;
    public GameObject bLight;

    private GameObject db;
    private GameObject b;

    [Header("This is you")]
    public float buffTimer;
    public float debuffTimer;

    void Start()
    {
        anchors = GameObject.FindGameObjectsWithTag("Anchor");
        Buff();
        Debuff();
    }

    void Update()
    {
        //  select random anchor
        //  modify the anchor
        // select renderer of core
        // change mat to buff / debuff
        // do the thing


        GameObject[] bList = GameObject.FindGameObjectsWithTag("BuffLight");
        if (bList.Length > 1) Destroy(bList[0]);

        GameObject[] dbList = GameObject.FindGameObjectsWithTag("DebuffLight");
        if (dbList.Length > 1) Destroy(dbList[0]);

    }
    public void Buff()
    {
        if (b != null) Destroy(b);
        Debug.Log("Buffin");
        buffAnchor = anchors[(int)Random.Range(0, anchors.Length)];
        buff = buffAnchor.GetComponent<AnchorController>();
        if (buff.isDebuffed) Buff();
        buff.isBuffed = true;
        buff.changeMat(buffMat);
        b = (GameObject)Instantiate(bLight, buffAnchor.transform);
        Invoke("BuffOutro", buffTimer);
    }

    public void Debuff()
    {
        if (db != null) Destroy(db);
        debuffAnchor = anchors[(int)Random.Range(0, anchors.Length)];
        debuff = debuffAnchor.GetComponent<AnchorController>();
        if (debuff.isBuffed) Debuff();
        debuff.isDebuffed = true;
        debuff.changeMat(debuffMat);
        db = (GameObject)Instantiate(dbLight, debuffAnchor.transform);
        Invoke("debuffOutro", debuffTimer);
    }

    public void debuffOutro()
    {
        debuff.isDebuffed = false;
        debuff.changeMat(defaultMat);
        Destroy(db);
        Debuff();
    }

    public void BuffOutro()
    {
        buff.isBuffed = false;
        buff.changeMat(defaultMat);
        Destroy(b);
        Buff();
    }
}
