using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetAndChange : MonoBehaviour
{
 
    public void resetGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   public void useBFS()
    {             
        GameObject.Find("Game manager").GetComponent<Graph_generation>().check=1;
        GameObject.Find("Player").GetComponent<Player>().lblthuatToan.text="TT: BFS";
    }
    public void useDFS()
    { 
        GameObject.Find("Game manager").GetComponent<Graph_generation>().check=2;
        GameObject.Find("Player").GetComponent<Player>().lblthuatToan.text="TT: DFS";
    }
    public void useDLS()
    { 
        GameObject.Find("Game manager").GetComponent<Graph_generation>().check=3;
        GameObject.Find("Player").GetComponent<Player>().lblthuatToan.text="TT: DLS";
    }
    public void useGS()
    { 
        GameObject.Find("Game manager").GetComponent<Graph_generation>().check=4;
        GameObject.Find("Player").GetComponent<Player>().lblthuatToan.text="TT: GS";
    }
    public void useA_star()
    { 
        GameObject.Find("Game manager").GetComponent<Graph_generation>().check=5;
        GameObject.Find("Player").GetComponent<Player>().lblthuatToan.text="TT: A*";
    }
}
