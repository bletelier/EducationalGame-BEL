using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPantallaInicio : MonoBehaviour
{
    public Color[] colores;
    public Light luz;
    public SpriteRenderer aura;
    public SpriteRenderer ojos;
    // Start is called before the first frame update
    void Start()
    {
        luz = luz.gameObject.GetComponent<Light>();
        aura = aura.gameObject.GetComponent<SpriteRenderer>();
        ojos = ojos.gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(CambiarColor());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.right * 5.0f * Time.deltaTime;
    }
    IEnumerator CambiarColor()
    {
        while (true)
        {
            int color = Random.Range(0, colores.Length);
            float time = Random.Range(0.2f, 1.3f);
            luz.color = colores[color];
            aura.color = colores[color];
            ojos.color = colores[color];
            yield return new WaitForSeconds(time);
        }
    }
}
