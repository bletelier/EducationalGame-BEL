using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public Transform portalSalida;
    public Transform player;
    void Start()
    {
        portalSalida = portalSalida.gameObject.GetComponent<Transform>();
        player = player.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.position = portalSalida.position;
        }
    }

}
