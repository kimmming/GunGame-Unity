using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 1; // 총알의 속도

    public Vector3 target; // 목표 지점
    private bool isFired; // 발사 상태

    // 목표 지점 설정 및 발사 상태 활성화
    public void Shoot(Vector3 newTarget)
    {
        target = newTarget;
        isFired = true;
    }

    void Update()
    {
        if (isFired)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        // 목표 지점을 향해 총알 이동
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // 목표 지점에 거의 도달하면 총알 제거
        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            Destroy(gameObject); // 총알 객체 파괴
        }
    }
}
