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
    GameObject[] bList;
    GameObject[] dbList;

    [Header("This is you")]
    public float buffTimer;
    public float debuffTimer;

    void Start()
    {
        anchors = GameObject.FindGameObjectsWithTag("Anchor");
        Buff();
        Debuff();
    }

    void FixedUpdate()
    {
        bList = GameObject.FindGameObjectsWithTag("BuffLight");
        if (bList.Length > 1) Destroy(bList[0]);

        dbList = GameObject.FindGameObjectsWithTag("DebuffLight");
        if (dbList.Length > 1) Destroy(dbList[0]);

    }
    public void Buff()
    {
        buffAnchor = anchors[(int)Random.Range(0, anchors.Length)];
        buff = buffAnchor.GetComponent<AnchorController>();
        if (buff.isDebuffed) BuffCut();
        buff.isBuffed = true;
        buff.changeMat(buffMat);
        b = (GameObject)Instantiate(bLight, buffAnchor.transform);
        Invoke("BuffOutro", buffTimer);
    }

    public void Debuff()
    {
        debuffAnchor = anchors[(int)Random.Range(0, anchors.Length)];
        debuff = debuffAnchor.GetComponent<AnchorController>();
        if (debuff.isBuffed) DebuffCut();
        debuff.isDebuffed = true;
        debuff.changeMat(debuffMat);
        Instantiate(dbLight, debuffAnchor.transform);        
        Invoke("DebuffOutro", debuffTimer);
    }

    public void BuffCut()
    {
        buffAnchor = null;
        buff = null;
        Buff();
    }

    public void DebuffCut()
    {
        debuffAnchor = null;
        debuff = null;
        Debuff();
    }

    public void DebuffOutro()
    {
        Destroy(GameObject.FindGameObjectWithTag("DebuffLight"));
        debuff.isDebuffed = false;
        debuff.changeMat(defaultMat);
        Destroy(db);
        debuffAnchor = null;
        debuff = null;
        Debuff();
    }

    public void BuffOutro()
    {
        buff.isBuffed = false;
        buff.changeMat(defaultMat);
        Destroy(b);
        buffAnchor = null;
        buff = null;
        Buff();
    }
}
