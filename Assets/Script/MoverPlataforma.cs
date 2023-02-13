using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlataforma : MonoBehaviour
{
    public AudioClip activate;
    public Transform platAMover;
    private bool first;
    public float cantAMover;
    public SpriteRenderer sensor;
    public Sprite sensorActivo;
    // Start is called before the first frame update
    void Start()
    {
        platAMover = platAMover.gameObject.GetComponent<Transform>();
        sensor = sensor.GetComponent<SpriteRenderer>();
        first = true;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(first && (collision.gameObject.tag == "Body" || collision.gameObject.tag == "Player")  )
        {
            SoundManagerTutorial.PlaySound(activate);
            sensor.sprite = sensorActivo;
            sensor.gameObject.GetComponent<Animator>().SetTrigger("encendido");
            first = false;
            StartCoroutine(Mover(0.3f));
        }
    }
    IEnumerator Mover(float time)
    {
        float c = 0;
        while (c < time)
        {

            platAMover.position += (Vector3.down * ((cantAMover * 0.1f) / time));
            yield return new WaitForSeconds(0.1f);
            c += 0.1f;

        }
    }
}
