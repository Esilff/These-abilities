using System;
using UnityEngine;

public class DeafEmitter : MonoBehaviour
{
    [SerializeField] private DeafListener listener;

    [Range(1,1000)][SerializeField] private float emissionRadius;
    [SerializeField] private LayerMask layerMask;

    private bool _isColliding;

    private RaycastHit _hitInfo;

    private bool _isEmitting = true;
    
    public bool IsEmitting { get => _isEmitting; set => _isEmitting = value; }

    private void FixedUpdate()
    {
        if (!_isEmitting) return;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, emissionRadius, layerMask);
        _isColliding = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform != listener.transform) continue;
            _isColliding = true;
            break;
        }

        if (!_isColliding) return;
        Physics.Raycast(
            new Ray(transform.position, listener.transform.position - transform.position),
            out _hitInfo, Vector3.Distance(transform.position, listener.transform.position), layerMask
        );
        if (_hitInfo.collider.gameObject.GetComponent<DeafCollider>()) return;
        var directionToTarget = (transform.position - listener.transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(directionToTarget);
        listener.Emitters.Add(targetRotation);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, emissionRadius);
    }
}
