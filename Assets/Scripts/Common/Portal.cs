using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform targetPortal;  
    public Transform GetTargetPortal() => targetPortal;
}
