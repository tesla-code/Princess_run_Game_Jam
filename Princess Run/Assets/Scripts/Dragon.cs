using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    // Start is called before the first frame update
    private bool fly = false;
    void Start()
    {
        
    }
    private void Update()
    {        
        GetComponent<Player>().isFlying = fly;
    }

    // Update is called once per frame
    void OnTriggerStay(Collider collider)
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collider.CompareTag("Dragon"))
            {
                
                fly = !fly;
            }
        }
    }
}
