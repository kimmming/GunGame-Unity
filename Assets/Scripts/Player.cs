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

    public float crouchHeight = 0.5f; // 앉았을 때 높이
    public float normalHeight = 2f; // 서 있을 때 높이
    public float crouchSpeed = 1f; // 앉고 일어나는 속도
    private bool isCrouching = false; // 플레이어가 앉아 있는지 확인
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
        moveDir.Normalize(); // 대각선 이동에서 값이 커지는걸 1로 제한해줌

        // 회전
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 
            float.MaxValue, 1 << LayerMask.NameToLayer("MouseRay")))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0;
            transform.LookAt(transform.position + lookDir);
        }

        // 총쏘기
        if(Input.GetMouseButtonDown(0)) 
        {
            Instantiate(bulletPrefab,muzzle.position, transform.rotation );
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            Debug.Log("점프!");

        }

        // 앉기 입력 처리
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleCrouch();
            Debug.Log("앉기/일어서기 상태 전환");

        }
    }

    private void FixedUpdate()
    {
        rbody.MovePosition(rbody.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // 바닥에 닿았는지 확인
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
        if (collision.gameObject.CompareTag("Ground")) // 바닥에서 벗어났는지 확인
        {
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // 바닥에 닿아 있는 동안 점프 상태 업데이트
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
