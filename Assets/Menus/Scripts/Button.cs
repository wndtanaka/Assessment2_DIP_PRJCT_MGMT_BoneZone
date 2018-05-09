using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    public void OnHover()
    {
        anim.SetBool("isHover", true); 
    }

    public void OnClick()
    {
        anim.SetBool("isClicked", true);
        anim.SetBool("isHover", false);
    }

}
