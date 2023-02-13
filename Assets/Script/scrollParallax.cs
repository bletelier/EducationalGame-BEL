using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollParallax : MonoBehaviour
{
    public float velocidad = 0.0f;
    public Transform player;
    private Renderer rd;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rd.material.mainTextureOffset = new Vector2(player.position.x * velocidad, 0.0f);
    }
}
