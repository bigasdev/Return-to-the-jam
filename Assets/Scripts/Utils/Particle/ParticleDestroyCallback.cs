using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyCallback : MonoBehaviour
{
    [SerializeField] string poolName;

    private void OnParticleSystemStopped() {
        PoolsManager.Instance.GetPool(poolName).AddToPool(this.gameObject);
    }
}
