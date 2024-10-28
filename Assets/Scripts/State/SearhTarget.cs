using System;
using UnityEngine;

public class SearhTarget
{
    Transform transform;
    UnitData unitData;
    LayerMask targetLayer;
    public SearhTarget(Transform transform,UnitData unitData,LayerMask targetLayer)
    {
        this.transform = transform;
        this.unitData = unitData;
        this.targetLayer = targetLayer;
    }

    public Transform TargetTransform()
    {
        Transform target = null;
        float closestDistance = Mathf.Infinity;

        // Collider2D[]로 탐색 영역 내의 모든 타겟을 가져옴
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, unitData.DetectRange, targetLayer);

        foreach (Collider2D hitCollider in hitColliders)
        {
            float distance = Vector2.Distance(transform.position, hitCollider.transform.position);

            // 가장 가까운 타겟을 찾아서 할당
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = hitCollider.transform;
            }
        }

        return target;
    }
    public bool DetectComPareTag(string detectTag, out Action<float> targetHealth)
    {
        Transform target = null;
        target = TargetTransform();
        targetHealth = null;
        if (target == null)
            return false;
        targetHealth = target.GetComponent<IUnitHeatlh>().GetHit;

        return target.CompareTag(detectTag);
    }
    public bool DetectComPareTag(string detectTag)
    {
        Transform target = null;
        target = TargetTransform();
        if (target == null)
            return false;
        return target.CompareTag(detectTag);
    }
    public Transform HealTargetTransform()
    {
        Transform target = null;

        // Collider2D[]로 탐색 영역 내의 모든 타겟을 가져옴
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, unitData.DetectRange, targetLayer);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // 자기 자신을 제외하기 위한 조건
            if (hitCollider.transform == transform || hitCollider.transform.name == "FriendlyCaslte")
            {
                continue;
            }

            if (hitCollider.TryGetComponent(out IUnitHeatlh unitHeatlh))
            {
                if (!unitHeatlh.IsMaxHealth())
                    return hitCollider.transform;
                else
                    target = hitCollider.transform;
            }
            else
                continue;

            //if (hitCollider.GetComponent<IUnitHeatlh>().IsMaxHealth())
            //    return target;
            //else
            //    continue;
        }

        return target;
    }
    public bool DetectHealComPareTag(string detectTag, out Action<float> targetHealth)
    {

        Transform target = HealTargetTransform();
        targetHealth = null;
        if (target == false)
            return false;
        
        targetHealth = target.GetComponent<IUnitHeatlh>().GetHeal; 

        return target.CompareTag(detectTag);
    }
    public string TargetTag()
    {
        return LayerMask.LayerToName((int)Mathf.Log(targetLayer.value, 2));
    }
}
