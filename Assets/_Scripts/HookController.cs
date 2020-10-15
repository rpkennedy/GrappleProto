using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    public GameObject grapplePrefab;
    private GameObject anchor;
    public GameObject grapple;

    void Start()
    {
    }

    public GameObject Anchor
    {
        get { return anchor; }
        set { anchor = value; }
    }

    void Update()
    {
        if (anchor == null)
        {
            Destroy(grapple);
            return;
        }


        if (Input.GetMouseButtonDown(0) && anchor.GetComponent<AnchorController>().isTargeted && anchor.GetComponent<AnchorController>().isGrappled)
        {
            Destroy(grapple);
            anchor.GetComponent<AnchorController>().isGrappled = false;
        }
        if(Input.GetMouseButtonDown(0) && anchor.GetComponent<AnchorController>().isTargeted)
        {
            if (anchor.GetComponent<AnchorController>().isGrappled == false)
            {
                Vector2 destiny = anchor.GetComponent<AnchorController>().transform.position;
                grapple = (GameObject)Instantiate(grapplePrefab, transform.position, Quaternion.identity);
                grapple.GetComponent<RopeController>().destiny = destiny;
                anchor.GetComponent<AnchorController>().isGrappled = true;                
            }
            else
            {
                Destroy(grapple);
                anchor.GetComponent<AnchorController>().isGrappled = false;
            }
        }
        
    }
}