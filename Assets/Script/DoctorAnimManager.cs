using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorAnimManager : MonoBehaviour
{

    public Animator anim;
    private void Start()
    {
        anim = anim.gameObject.GetComponent<Animator>();
    }

    public void DoctorHablando()
    {
        anim.SetBool("hablando", true);
    }
    public void DoctorCallado()
    {
        anim.SetBool("hablando", false);
    }
}
