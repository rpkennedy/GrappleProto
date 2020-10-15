using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RopeController : MonoBehaviour
{

    public Vector2 destiny;

    public float speed;
    public float distance = 2;
    public GameObject nodePrefab;
    public GameObject player;
    public GameObject lastNode;
    public LineRenderer lr;

    int vertexCount = 2;
    public List<GameObject> Nodes = new List<GameObject>();
    bool done = false;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        lastNode = transform.gameObject;
        Nodes.Add(transform.gameObject);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destiny, speed);
        //rope construction
        if ((Vector2)transform.position != destiny) //haven't arrived yet
        {
            if (Vector2.Distance(player.transform.position, lastNode.transform.position) > distance)
            { //if dist from last node to player is greater than spec. val:
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
        }       //connect player to hinge rope



        //climb pt1
        if (player.GetComponent<PlayerController>().isClimbing) //player moves to last node while climbing bool true
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, Nodes[Nodes.Count - 1].transform.position, 0.05f);
        }
        //climb pt2
        if (Vector2.Distance(player.transform.position, Nodes[Nodes.Count - 1].transform.position) <= 0.1f) //if player gets too close to node
        {
            Nodes[Nodes.Count-2].GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
            Destroy(Nodes[Nodes.Count - 1]);    //connect hinge rope from player to node behind last
            Nodes.RemoveAt(Nodes.Count - 1);    //destroy and remove last node
            vertexCount--;                      //take point out of line renderer
        }
        
        
        RenderLine();
    }

    void FixedUpdate()
    {
        //descending
        if (player.GetComponent<PlayerController>().isDescending)
        {
            Vector2 anchor = player.GetComponent<HookController>().Anchor.gameObject.transform.position;
            Vector2 node = Nodes[Nodes.Count - 1].transform.position;
            player.transform.position = Vector2.MoveTowards(player.transform.position, -anchor, 0.1f); //move away from the anchor

            if (Vector2.Distance(player.transform.position, node) > distance) //if dist from player to last node is > spec val
            {
                CreateNode();       //create a node and connect it to hinge rope
                lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
            }
        }
    }

    void RenderLine()
    {
        lr.positionCount = vertexCount;     //points = number of nodes

        int i;
        for (i = 0; i < Nodes.Count; i++)
        {
            lr.SetPosition(i, Nodes[i].transform.position); //set points to node values
        }
        lr.SetPosition(i, player.transform.position); //last point is player not a node
    }

    void CreateNode()
    {
        lastNode = Nodes[Nodes.Count - 1];
        Vector2 pos2Create = player.transform.position - lastNode.transform.position;   
        pos2Create.Normalize();             //find direction from player to node
        pos2Create *= distance;             //change pos to spec. val away in direction
        pos2Create += (Vector2)lastNode.transform.position;

        GameObject go = (GameObject)Instantiate(nodePrefab, pos2Create, Quaternion.identity);

        go.transform.SetParent(transform);

        lastNode.GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();
        lastNode = go;                      //connect to hinge rope, switch new node to last node
        Nodes.Add(lastNode);                //add last (new) node to list

        vertexCount++;              //for the line renderer
    }
}