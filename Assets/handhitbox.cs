using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handhitbox : MonoBehaviour
{


    //MUISTA KOMMENTOIDA!!!!!
    //KOODI TOIMII, ÄLÄ KOSKE


    public bool Sisalla;

    // Start is called before the first frame update
    void Start()
    {
        Sisalla = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.transform.position);
    }

    //OTHER.TAG ?
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "rengas")
        {
            Sisalla = true;
        }
    }


    //OTHER.TAG ?
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "rengas")
        {
            Sisalla = false;
        }
    }

}
