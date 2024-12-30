using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAI : MonoBehaviour
{
    public Transform player; 
    public float speed = 5f;

    void Update()
    {
        if (player != null)
        {
            
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            transform.position += direction * speed * Time.deltaTime;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }
}
