using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour, IDamageable
{
    public float maxHealth = 10.0f;
    public float Health { get; set; }

    GameObject targetVisual;

    [SerializeField]
    ParticleSystem deathParticle;

    MeshCollider meshCol;

    void Start()
    {
        Health = maxHealth;
        targetVisual = transform.GetChild(0).gameObject; // Grab the first transform attached as a child.
        WorkTable.OnTestWeaponButton.AddListener(ResetTargetDummy);

        meshCol = GetComponent<MeshCollider>();
    }

    public void Die()
    {
        targetVisual.SetActive(false);
        deathParticle.Play();
        Health = maxHealth;
        meshCol.enabled = false;
    }

    public void ResetTargetDummy()
    {
        targetVisual.SetActive(true);
        meshCol.enabled = true;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Die();
        }
    }
}