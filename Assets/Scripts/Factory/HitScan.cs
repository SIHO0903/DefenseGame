using System;
using System.Collections;
using UnityEngine;

public abstract class HitScan : MonoBehaviour
{
    [field : SerializeField]public float Damage { get; set; }
    protected Animator animator;
    protected bool canAttack;
    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public abstract void OnTriggerEnter2D(Collider2D collision);

    protected IEnumerator SetActiveFalse()
    {
        while (true)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.normalizedTime >= 1f)
            {
                gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }
    
}