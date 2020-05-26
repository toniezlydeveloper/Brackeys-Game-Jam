using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    private void OnTriggerEnter2D(Collider2D other1)
    {
        anim.SetTrigger("Move");
    }
}
