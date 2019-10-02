using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private int health;
    private bool dead = false;

    // Player ship's transform 
    private Transform target;
}
