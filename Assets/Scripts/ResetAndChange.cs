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
}
