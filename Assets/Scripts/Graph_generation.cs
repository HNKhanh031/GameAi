using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Graph_generation : MonoBehaviour
{

    public GameObject tilePrefab;

    public Transform agent;

    public int width;

    public int height;
    public int check;
    
    public Node[] nodes;

    public Transform pole;

    public Node previousStartNode;

    public List<Vector3> wayPoints;

    public Material[] materialTile;

    public int[] wallGrid;

    public GameObject wallPrefab;
    public GameObject coinPrefab;

    public int endPointPreviousPosition;
    public int startPointPreviousPosition;



    public void Awake()
    {
        wallGrid = new int[]
        {
            1,0,0,1,0,0,1,1,
            0,0,0,0,0,0,0,0,
            0,0,1,0,1,0,1,0,
            1,0,1,0,0,0,0,1,
            1,0,0,0,1,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,1,0,
            1,0,0,0,1,0,0,0,
            0,0,1,0,0,0,0,0
        };


        spawTiles();
        populateTileNeighbours();

        //wayPoints = new List<Vector3>();

        //findWayPoints(findEndNode(nodes[getObjectTilePos(agent)], nodes[getObjectTilePos(pole)]));
    }

    public void Update()
    {
        
    }
    
    
    public void resetWaypoints()
    {

        int endPointCurrentPos = getObjectTilePos(pole);

        int startPointCurrentPos = getObjectTilePos(agent);

        if (endPointCurrentPos != endPointPreviousPosition || startPointPreviousPosition != startPointCurrentPos)
        {
            foreach (Node node in nodes)
            {
                node.visited = false;
                node.parent = null;
            }

            endPointPreviousPosition = endPointCurrentPos;

            startPointPreviousPosition = startPointCurrentPos;

            agent.GetComponent<Agent_navigation>().waypointIndex = 0;


            wayPoints = new List<Vector3>();
            if(check==1){
                findWayPoints(BFS(nodes[startPointCurrentPos], nodes[endPointCurrentPos]));
            }
            else if(check==2){
                findWayPoints(DFS(nodes[startPointCurrentPos], nodes[endPointCurrentPos]));
            }
            
        }

       
    }

    
  

    public void spawTiles()
    {
        nodes = new Node[width * height];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++, i++)
            {
                
                nodes[i] = new Node(i, new Vector3(x * 10, 1f, z * 10));
                nodes[i].neighbours = new List<Node>();

               

                // Spawn wall
                if (wallGrid[i] == 1)
                {
                    nodes[i].wall = true;
                    Instantiate(wallPrefab, new Vector3(x * 10, 5f, z * 10), Quaternion.identity);
                }
                
                // Spawn tile
                else
                {
                    if(i%2==0){
                        nodes[i].coin = true;
                        Instantiate(coinPrefab, new Vector3(x * 10, 1f, z * 10), Quaternion.identity);
                    }
                    GameObject go = Instantiate(tilePrefab, new Vector3(x * 10, 0.005f, z * 10), Quaternion.identity);
                    
                }

            }
        }

    }

    public void populateTileNeighbours()
    {
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++, i++)
            {

                //Debug.Log(nodes[i].ID);
                // Horizontal neighbours

                // Horizontal bounds
                if (nodes[i].location.x != 0 && nodes[i].location.x != (width-1)*10)
                {
                    if (nodes[i - 1].wall == false)
                    {
                        nodes[i].neighbours.Add(nodes[i - 1]);
                    }

                    if (nodes[i + 1].wall == false)
                    {
                        nodes[i].neighbours.Add(nodes[i + 1]);
                    }
                }

                // In-between borders horizontal neighbours
                else
                {
                    if (nodes[i].location.x == 0)
                    {
                        if (nodes[i + 1].wall == false)
                        {
                            nodes[i].neighbours.Add(nodes[i + 1]);
                        }
                    }

                    else if(nodes[i].location.x == (width - 1)*10)
                    {
                        if (nodes[i - 1].wall == false)
                        {
                            nodes[i].neighbours.Add(nodes[i - 1]);
                        }
                    }
                }

                //Vertical neighbours
                if (nodes[i].location.z != 0 && nodes[i].location.z != (height - 1) * 10)
                {

                    if (nodes[i - width].wall == false)
                    {
                        nodes[i].neighbours.Add(nodes[i - width]);
                    }

                    if (nodes[i + width].wall == false)
                    {
                        nodes[i].neighbours.Add(nodes[i + width]);
                    }
                
                }

                // In-between borders horizontal neighbours
                else
                {
                    if (nodes[i].location.z == 0)
                    {
                        if (nodes[i + height].wall == false)
                        {
                            nodes[i].neighbours.Add(nodes[i + height]);
                        } 
                    }

                    else if (nodes[i].location.z == (height - 1) * 10)
                    {
                        if (nodes[i - height].wall == false)
                        {
                            nodes[i].neighbours.Add(nodes[i - height]);
                        }
                        
                    }
                }



            }
        }
    }
    
public Node BFS(Node startNode, Node endNode)
{
    Queue<Node> nodeQueue = new Queue<Node>();

    // Bắt đầu từ nút kết thúc và thêm nó vào hàng đợi
    endNode.visited = true;
    nodeQueue.Enqueue(endNode);

    while (nodeQueue.Count > 0)
    {
        Node currentNode = nodeQueue.Dequeue();

        foreach (Node neighbour in currentNode.neighbours)
        {
            if (!neighbour.visited)
            {
                // Thiết lập nút hiện tại là cha của hàng xóm
                neighbour.parent = currentNode;

                // Kiểm tra xem hàng xóm có phải là nút bắt đầu không
                if (neighbour == startNode)
                {
                    return neighbour.parent;
                }

                // Đánh dấu hàng xóm đã được ghé thăm và thêm vào hàng đợi
                neighbour.visited = true;
                nodeQueue.Enqueue(neighbour);
            }
        }
    }

    // Nếu không tìm thấy đường đi, trả về null
    return null;
}
    public Node DFS(Node startNode, Node endNode)
{
    Stack<Node> nodeStack = new Stack<Node>();

    endNode.visited = true;
    nodeStack.Push(endNode);

    while (nodeStack.Count > 0)
    {
        Node currentNode = nodeStack.Pop();

        foreach (Node neighbour in currentNode.neighbours)
        {
            if (!neighbour.visited)
            {
                // Add current node to neighbour as its parent
                neighbour.parent = currentNode;

                // Check if neighbour is the start node
                if (neighbour == startNode)
                {
                    return neighbour.parent;
                }

                // Mark neighbour as visited
                neighbour.visited = true;

                // Add neighbour to stack for further exploration
                nodeStack.Push(neighbour);
            }
        }
    }

    // If once all nodes have been visited the end node can't be found, return null.
    return null;
}

    public void findWayPoints(Node node)
    {

        if(node == null)
        {
            return;
        }

        if (node.parent == null)
        {
            wayPoints.Add(node.location);
            return;
        }

        else
        {
            wayPoints.Add(node.location);
            findWayPoints(node.parent);
        }

        agent.GetComponent<Agent_navigation>().target = pole;

    }

    public int getObjectTilePos(Transform someObject)
    {
        int x = (int)Mathf.Round(someObject.position.x/10);
        //Debug.Log("x=" + x);
        int z = (int)Mathf.Round(someObject.position.z/10);
        //Debug.Log("z=" + z);

        return x + z * width;

    }

}

[System.Serializable]
public class Node
{
    public int ID;

    public Vector3 location;

    public Node parent;

    public bool wall;
    public bool coin;

    public List<Node> neighbours;

    public bool visited;

    public Node(int _ID, Vector3 _location)
    {
        ID = _ID;
        location = _location;
        visited = false;
    }
}
