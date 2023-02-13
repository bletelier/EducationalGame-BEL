using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiarColor : MonoBehaviour
{
    public Light luz;
    public SpriteRenderer aura;
    public SpriteRenderer ojos;
    public Color color1;
    public Color color2;
    private bool xd = true;

    // Start is called before the first frame update
    void Start()
    {
        luz = luz.gameObject.GetComponent<Light>();
        aura = aura.gameObject.GetComponent<SpriteRenderer>();
        ojos = ojos.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && xd)
        {
            luz.color = color1;
            aura.color = color1;
            ojos.color = color1;
            xd = !xd;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !xd)
        {
            luz.color = color2;
            aura.color = color2;
            ojos.color = color2;
            xd = !xd;
        }
    }
}
