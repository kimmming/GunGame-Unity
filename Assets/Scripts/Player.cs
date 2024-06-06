using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody rbody;
    public GameObject bulletPrefab;
    public Transform muzzle;
    public Vector3 moveDir;
    public float moveSpeed;

    public float jumpForce = 5f;
    private bool isGrounded;

    public float crouchHeight = 0.5f; // �ɾ��� �� ����
    public float normalHeight = 2f; // �� ���� �� ����
    public float crouchSpeed = 1f; // �ɰ� �Ͼ�� �ӵ�
    private bool isCrouching = false; // �÷��̾ �ɾ� �ִ��� Ȯ��
    private CapsuleCollider capsuleCollider;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            Debug.Log("����!");

        }

        // �ɱ� �Է� ó��
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleCrouch();
            Debug.Log("�ɱ�/�Ͼ�� ���� ��ȯ");

        }
    }

    private void FixedUpdate()
    {
        rbody.MovePosition(rbody.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // �ٴڿ� ��Ҵ��� Ȯ��
        {
            isGrounded = true;
        }
     //   if (collision.gameObject.GetComponent<Coin>() != null) 
     // {
     //      gameManager.GotCoin();
     //       Destroy(collision.gameObject);
     //}

     //   ContactPoint contactPoint = collision.contacts[0];
     //  print(contactPoint.point);
     }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // �ٴڿ��� ������� Ȯ��
        {
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // �ٴڿ� ��� �ִ� ���� ���� ���� ������Ʈ
        {
            isGrounded = true;
        }
    }

    private void ToggleCrouch()
    {
        if (isCrouching)
        {
            StartCoroutine(Crouch(normalHeight));
        }
        else
        {
            StartCoroutine(Crouch(crouchHeight));
        }
        isCrouching = !isCrouching;
    }

    private IEnumerator Crouch(float targetHeight)
    {
        float currentHeight = capsuleCollider.height;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * crouchSpeed;
            capsuleCollider.height = Mathf.Lerp(currentHeight, targetHeight, t);
            yield return null;
        }

        capsuleCollider.height = targetHeight;
    }

}
