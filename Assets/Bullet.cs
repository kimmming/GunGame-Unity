using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 1; // �Ѿ��� �ӵ�

    public Vector3 target; // ��ǥ ����
    private bool isFired; // �߻� ����

    // ��ǥ ���� ���� �� �߻� ���� Ȱ��ȭ
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
        // ��ǥ ������ ���� �Ѿ� �̵�
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // ��ǥ ������ ���� �����ϸ� �Ѿ� ����
        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            Destroy(gameObject); // �Ѿ� ��ü �ı�
        }
    }
}
