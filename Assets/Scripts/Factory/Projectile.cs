using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{
    protected Vector3 startPos;
    protected Vector3 dirPos;
    protected Rigidbody2D rigid;
    protected float speed = 6f;
    protected Action<float> targetHealth;
    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
}
