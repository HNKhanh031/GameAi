using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Fcamera : MonoBehaviour
{

    public GameObject fplayer;
    // Start is called before the first frame update

    void Start()
    {
        transform.position=new Vector3(36,82,-7f);
        transform.rotation=Quaternion.Euler(new Vector3(60,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rtt= Quaternion.Euler(new Vector3(65,0,0));
        Quaternion rtd= Quaternion.Euler(new Vector3(55,0,0));
        if(Input.GetKey(KeyCode.S)){
            transform.position=fplayer.transform.position + new Vector3(-19.8f,75,-15.5f);
            transform.rotation=Quaternion.Slerp(transform.rotation,rtd,0.5f*Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.W)){
            transform.position=fplayer.transform.position + new Vector3(-19.8f,75,-15.5f);
            transform.rotation=Quaternion.Slerp(transform.rotation,rtt,0.5f*Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.A)){
            transform.position=fplayer.transform.position + new Vector3(-19.8f,75,-15.5f);
        }
        else if(Input.GetKey(KeyCode.D)){
            transform.position=fplayer.transform.position + new Vector3(-19.8f,75,-15.5f);
        }
        
    }
}
