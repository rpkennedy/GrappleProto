using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorController : MonoBehaviour
{
    public bool isTargeted;
    public bool isGrappled;
    public bool isBuffed;
    public bool isDebuffed;
    public GameObject core;

    public GameObject targetPrefab;
    GameObject target;

    void Start()
    {
        target = targetPrefab;
        target.transform.position = Vector3.zero;
        
        isTargeted = false;
        isGrappled = false;
        isBuffed = false;
        isDebuffed = false;
        
        target.transform.Translate(1.44f, 0, 0);
    }

    void OnMouseEnter()
    {
        Debug.Log("enter");               
        Instantiate(target, this.transform);

        isTargeted = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<HookController>().Anchor = this.gameObject;
    }

    void OnMouseExit()
    {
        Destroy(GameObject.FindGameObjectWithTag("Target"));
        isTargeted = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<HookController>().Anchor = null;
    }

    void Update()
    {
        if (isBuffed)
        {
            
        }
        else if (isDebuffed)
        {
            
        }
        else
        {
            
        }
    }
    public void changeMat(Material mat)
    {
        core.gameObject.GetComponent<Renderer>().material = mat;        
    }
}
