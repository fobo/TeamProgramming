﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPosition : MonoBehaviour
{
    public GameObject target;





    // Start is called before the first frame update
    void Start()
    {
       
    }

    

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.GetComponent<Transform>().position.x-8, transform.position.y, transform.position.z);
        
    }

    private void FixedUpdate()
    {
       
    }
    void moveCharacter(Vector2 direction)
    {
      
           
    }
            
        
  
        
    
} // class end
