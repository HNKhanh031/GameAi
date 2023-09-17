using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Player : MonoBehaviour
{
    public int speedPlayer=10;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lblthuatToan;
    public TextMeshProUGUI lbltime;
    public TextMeshProUGUI lbltime2;
    public Canvas MainUi;
    public TextMeshProUGUI lblMain;
    private float startTime;
    private float gTime;
   
    int point = 0;
    
    

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        lblthuatToan.text="TT: DFS";
        MainUi.enabled=false;
        
    }

    // Update is called once per frame
    void Update()
    {
        gTime=Time.time-startTime;
        lbltime2.text="Time: "+gTime.ToString("F2")+"s";
        
        if(Input.GetKey(KeyCode.S)){
            transform.position-=transform.forward*speedPlayer*Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.W)){
            transform.position+=transform.forward*speedPlayer*Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.A)){
            transform.position-=transform.right*speedPlayer*Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D)){
            transform.position+=transform.right*speedPlayer*Time.deltaTime;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("coin"))
        {
            Destroy(other.gameObject); 
            point+= 10;
            scoreText.text = "Điểm: " + point.ToString();
            if(point==200){
                lblMain.text="YOU WIN";
                gTime=Time.time-startTime;
                lbltime.text="Time: "+gTime.ToString("F2")+"s";
                MainUi.enabled=true;
                GameObject enemy = GameObject.FindWithTag("Enemy");
                Destroy(enemy);
            }
        }
    }
    private void OnDestroy()
    {
        lblMain.text="YOU LOSE";
        gTime=Time.time-startTime;
        lbltime.text="Time: "+gTime.ToString("F2")+"s";
        scoreText.enabled=false;
        MainUi.enabled=true;
        
    
    }
      
}
