using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSoundManager : MonoBehaviour
{
    public void SpawnSound(Vector3 pos, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, radius);
        foreach (var hitCollider in hitColliders)
        {
            GameObject soundObject = hitCollider.transform.gameObject;
            if (soundObject.GetComponent<ZombieBehavior>())
            {
                soundObject.GetComponent<ZombieBehavior>().hearPlayer();
                soundObject.GetComponent<ZombieBehavior>().SetZombieDestination(pos);
            }
        }
    }
}
