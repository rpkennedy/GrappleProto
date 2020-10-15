using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RopeController : MonoBehaviour
{

    public Vector2 destiny;

    public float speed = 1;
    public float distance = 2;
    public GameObject nodePrefab;
    public GameObject player;
    public GameObject lastNode;
    public LineRenderer lr;
    public GameObject playerNode;

    int vertexCount = 2;
    public List<GameObject> Nodes = new List<GameObject>();
    bool done = false;

    void Start()
    {
        playerNode = GameObject.FindGameObjectWithTag("pNode");
        lr = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        lastNode = transform.gameObject;
        Nodes.Add(transform.gameObject);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destiny, speed);

        if ((Vector2)transform.position != destiny)
        {
            if (Vector2.Distance(player.transform.position, lastNode.transform.position) > distance)
            {
                CreateNode();
            }
        }
        else if (done == false)
        {
            done = true;

            while (Vector2.Distance(player.transform.position, lastNode.transform.position) > distance)
            {
                CreateNode();
            }
            lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        }

        /*
                GameObject nnode = (GameObject)Instantiate(nodePrefab, player.transform.position, Quaternion.identity);
                Debug.Log("Should be");

                
                lastNode = Nodes[Nodes.Count - 1];
                
                
                lastNode.GetComponent<HingeJoint2D>().connectedBody = nnode.GetComponent<Rigidbody2D>();
                lastNode = nnode;
                Nodes.Add(lastNode);

                vertexCount++; */
        //move pNode to Node[Count]
        //switch Node[Count] to pNode
        //delete old node

        
        if (Vector2.Distance(player.transform.position, Nodes[Nodes.Count - 1].transform.position) <= 0.1f)
        {
            Nodes[Nodes.Count-2].GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
            Destroy(Nodes[Nodes.Count - 1]);
            Nodes.RemoveAt(Nodes.Count - 1);
            vertexCount--;
        }
        
        if (player.GetComponent<PlayerController>().isClimbing)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, Nodes[Nodes.Count-1].transform.position, 0.05f);
        }
        RenderLine();
    }

    void FixedUpdate()
    {
        if (player.GetComponent<PlayerController>().isDescending)
        {
            Vector2 anchor = player.GetComponent<HookController>().Anchor.gameObject.transform.position;
            Vector2 node = Nodes[Nodes.Count - 1].transform.position;
            player.transform.position = Vector2.MoveTowards(player.transform.position, -anchor, 0.1f);

            if (Vector2.Distance(player.transform.position, node) > distance)
            {
                CreateNode();
                lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
            }
        }
    }

    void RenderLine()
    {
        lr.positionCount = vertexCount;

        int i;
        for (i = 0; i < Nodes.Count; i++)
        {
            lr.SetPosition(i, Nodes[i].transform.position);
        }
        lr.SetPosition(i, player.transform.position);
    }

    void CreateNode()
    {
        lastNode = Nodes[Nodes.Count - 1];
        Vector2 pos2Create = player.transform.position - lastNode.transform.position;
        pos2Create.Normalize();
        pos2Create *= distance;
        pos2Create += (Vector2)lastNode.transform.position;

        GameObject go = (GameObject)Instantiate(nodePrefab, pos2Create, Quaternion.identity);

        go.transform.SetParent(transform);

        lastNode.GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();
        lastNode = go;
        Nodes.Add(lastNode);

        vertexCount++;
    }
}