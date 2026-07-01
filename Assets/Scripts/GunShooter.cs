using UnityEngine;
using UnityEngine.Events;

public class GunShooter : MonoBehaviour
{
    [Header("References")]
    public Transform muzzle;      

    [Header("Shooting settings")]
    public float range = 100f;     
    public float fireCooldown = 0.3f; 

    [Header("Events")]
    public UnityEvent OnFire;          
    public UnityEvent<GameObject> OnTargetHit; 

    private float lastFireTime = -999f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (Time.time - lastFireTime < fireCooldown)
            return;

        lastFireTime = Time.time;

        OnFire?.Invoke();

        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, range))
        {
            Debug.Log("Touché : " + hit.collider.name);
            OnTargetHit?.Invoke(hit.collider.gameObject);
        }
        else
        {
            Debug.Log("Tir dans le vide");
        }
    }
}