using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// https://www.youtube.com/watch?v=OrMVmeVdo-M
/// 
/// 
/// 
/// 
/// </summary>
public class liike : MonoBehaviour
{
    
    //LUE:
    //MUISTA KOMMENTOIDA!!!!!
    //MASTERKOODI, VAATII OPTIMOINTIA
    //YHDEN KÄDEN KÄÄNTÖLIIKE VÄÄRIN, EI SIIS KUULU KÄÄNTYÄ PAIKALLAAN
    //KUN GRABIT POHJASSA, JATKAA MATKAA <--- TÄMÄ VÄÄRIN



    public bool tartuOikea;
    public bool tartuVasen;
    public bool oikeaSisalla;
    public bool vasenSisalla;
    public bool OikeaAlkuOtettu;
    public bool VasenAlkuOtettu;

    public GameObject oikeaKasi;
    public GameObject vasenKasi;
    public GameObject Pyoratuoli;
    public GameObject outbox;

    public Vector3 oAlkuPositio;
    public Vector3 vAlkuPositio;
    
    public float oikea_distance_alkupisteesta;      // muuttujat
    public float vasen_distance_alkupisteesta;
    public float oikea_distance_ohjaimeen;
    public float vasen_distance_ohjaimeen;

    //oikean_liike ja vasemman_liike on tehty nopeuden ja suunnan kaavoja varten
    //
    public float oikean_liike;
    public float vasemman_liike;
    public float summaliike;

    //Liike
    public Rigidbody rb;
    public float torque;
    public float liikekerroin;
    public float kiertoliikekerroin;
    //

    // Start is called before the first frame update
    void Start()
    {
        OikeaAlkuOtettu = false;
        VasenAlkuOtettu = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Valmiina uutta toimintoa varten
        if (SteamVR_Actions._default.testinput.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("tetstestsetetstsetsetset");
           

        }

        //grab down right
        if (SteamVR_Actions._default.tartuO.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Oikea painettu.");
            tartuOikea = true;

        }
        //grab up right
        if (SteamVR_Actions._default.tartuO.GetStateUp(SteamVR_Input_Sources.Any))
        {
            tartuOikea = false;
            OikeaAlkuOtettu = false;
        }
        //grab down right
        if (SteamVR_Actions._default.tartuV.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Vasen painettu.");
            tartuVasen = true;
        }
        //grab up right
        if (SteamVR_Actions._default.tartuV.GetStateUp(SteamVR_Input_Sources.Any))
        {
            tartuVasen = false;
            VasenAlkuOtettu = false;
        }

        //onko oikea/vasen ohjain hitboxin sisällä
        oikeaSisalla = oikeaKasi.GetComponent<handhitbox>().Sisalla;
        vasenSisalla = vasenKasi.GetComponent<handhitbox>().Sisalla;
        //oAlkuPositio = oikeaKasi.transform;
        //oLoppuPositio = Vector3.;

        //oikeaKasi.transform.position

        //hitboxin sisäinen DISTANCE
        //========================================================================
        // OIKEA KÄSI
        //========================================================================
        //Jos oikea grab on alhaalla
        if (tartuOikea == true)
        {
            //jos oikea alku on otettu ja oikea ohjain on sisällä
            if (!OikeaAlkuOtettu && oikeaSisalla == true)
            {
                
                oAlkuPositio = (oikeaKasi.transform.position);      // Oikean ohjaimen alkupositio
                oikea_distance_alkupisteesta = Vector3.Distance(oAlkuPositio, outbox.transform.position);       // Etäisyys alkupisteestä mittaboksiin eli outboxiin
                Debug.Log("Alkupositio: " + oAlkuPositio); //Konsoli
                Debug.Log("oikea distance alkupisteestä " + oikea_distance_alkupisteesta); //Konsoli
                OikeaAlkuOtettu = true; //Oikea alkupiste otettu
            }

            Vector3 pos = oikeaKasi.transform.position; //oikean käden sijainti
            //distance = (Vector3.Distance(oAlkuPositio, oikeaKasi.transform.position) - Vector2.Distance(oAlkuPositio, oikeaKasi.transform.position));
            oikea_distance_ohjaimeen = Vector3.Distance(oikeaKasi.transform.position, outbox.transform.position);       // etäisyys ohjaimesta mittaboksiin eli outboxiin
            Debug.Log("Distance to other: " + oikea_distance_ohjaimeen); //Konsoli

            oikean_liike = oikea_distance_alkupisteesta - oikea_distance_ohjaimeen; // Oikean ohjaimen liike: hitboxin ja alkupisteen etäisyys ohjaimeen; näiden erotus
            
        }
        else
        {
            oikean_liike = 0;
        }

        //========================================================================
        // VASEN KÄSI
        //========================================================================
        //Vasen käden samat kuin oikean
        if (tartuVasen == true)
        {
            if (!VasenAlkuOtettu && vasenSisalla == true)
            {
                vAlkuPositio = (vasenKasi.transform.position); // Vasemman alkupositio
                vasen_distance_alkupisteesta = Vector3.Distance(vAlkuPositio, outbox.transform.position); //Etäisyys alkupisteestä mittaboksiin eli outboxiin
                Debug.Log("Alkupositio: " + vAlkuPositio);
                Debug.Log("vasen distance alkupisteestä " + vasen_distance_alkupisteesta);
                VasenAlkuOtettu = true;
            }

            Vector3 pos = vasenKasi.transform.position;
            vasen_distance_ohjaimeen = Vector3.Distance(vasenKasi.transform.position, outbox.transform.position); //etäisyys ohjaimesta mittaboksiin eli outboxiin
            Debug.Log("Distance to other: " + vasen_distance_ohjaimeen);

            vasemman_liike = vasen_distance_alkupisteesta - vasen_distance_ohjaimeen;

        }
        else
        {
            vasemman_liike = 0;
        }


        //========================================================================
        //  LIIKE, force, liikekerroin jne
        //========================================================================

        torque = (vasemman_liike - oikean_liike);
        rb.AddTorque(transform.up * torque * kiertoliikekerroin);
        // summaliike = oikean_liike + vasemman_liike;

        /*
        if ( summaliike < 0.0f)
        {
            rb.AddForce(transform.forward * summaliike * kiertoliikekerroin * -1);
        }

        if (summaliike > 0.0f)
        {
            rb.AddForce(transform.forward * summaliike * kiertoliikekerroin);
        }
        */



        if (vasemman_liike > 0 && oikean_liike > 0)
        {
            rb.AddForce(transform.forward * ((vasemman_liike + oikean_liike) / 2) * liikekerroin);
            
        }
        if (vasemman_liike < 0 && oikean_liike < 0)
        {

            rb.AddForce(transform.forward * ((vasemman_liike + oikean_liike) / 2) * liikekerroin);

        }

    }
}

