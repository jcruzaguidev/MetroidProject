using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChecker : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float distance;

    public bool isTouching;

    private void Update()
    {
        isTouching = Physics2D.Raycast(transform.position, direction, distance, layerMask);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = isTouching ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, direction * distance);
    }
#endif
}
