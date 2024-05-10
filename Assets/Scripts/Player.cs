using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody rbody;
    //public FixedJoystick fixedJoystick;
    public GameObject bulletPrefab;
    public Transform muzzle;
    public Vector3 moveDir;
    public float moveSpeed;

    private void Update()
    {
        //float x = Input.GetAxisRaw("Horizontal") + fixedJoystick.Horizontal;
        //float z = Input.GetAxisRaw("Vertical") + fixedJoystick.Vertical;
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(x, 0, z);
        moveDir.Normalize(); // �밢�� �̵����� ���� Ŀ���°� 1�� ��������

        // ȸ��
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 
            float.MaxValue, 1 << LayerMask.NameToLayer("MouseRay")))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0;
            transform.LookAt(transform.position + lookDir);
        }

        // �ѽ��
        if(Input.GetMouseButtonDown(0)) 
        {
            Instantiate(bulletPrefab,muzzle.position, transform.rotation );
        }
    }

    private void FixedUpdate()
    {
        rbody.MovePosition(rbody.position + moveDir * moveSpeed);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<Coin>() != null) 
    //    {
    //        gameManager.GotCoin();
    //        Destroy(collision.gameObject);
    //    }

    //    ContactPoint contactPoint = collision.contacts[0];
    //     print(contactPoint.point);
    //}

}
