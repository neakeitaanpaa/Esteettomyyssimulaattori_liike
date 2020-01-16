using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_hand : MonoBehaviour
{

    public bool vasen;
    public bool oikea;

    public GameObject vasen_ohjain;
    public GameObject oikea_ohjain;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    //--------------------------
    //KOODI, JOLLA PALLOT PYSYVÄT OHJAIMEN KOHDALLA
    //ÄLÄ KOSKE, TOIMII!!
    //--------------------------

    // Update is called once per frame
    void Update()
    {
        if (vasen)
        {
            gameObject.transform.position = vasen_ohjain.GetComponent<Transform>().position;
        }

        if (oikea)
        {
            gameObject.transform.position = oikea_ohjain.GetComponent<Transform>().position;
        }
    }
}
