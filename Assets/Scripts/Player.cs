using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public ParticleSystem levelUp;
    public float moveSpeed = 5f;

    private bool isPressed = false;
    private Vector2 startPos;
    private Vector2 currentPos;
    private Animator animator;

    public GameObject bullet;
    public float bulletSpeed = 1f;
    float shootTimer = 0f;

    public bool isShooting;
    float shootDelay;
    Coroutine shootCo;
    private int bulletCount;

    float[] PosX01 = { 0.0f };
    float[] PosX02 = { -0.15f, 0.15f };
    float[] PosX03 = { -0.25f, 0.0f, 0.25f };
    float[] PosX04 = { -0.45f, -0.15f, 0.15f, 0.45f };
    float[] PosX05 = { -0.5f, -0.25f, 0.0f, 0.25f, 0.5f };

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (shootTimer > 0f)
            shootTimer -= Time.deltaTime;

        // 키가 눌려 있는 상태이고
        // 쿨타임이 끝났다면 발사
        if (isShooting && shootTimer <= 0.01f)
        {
            AnimatorChange("SHOOT");
            ShootBullet();

            // 다음 발사까지 쿨타임 설정
            shootTimer = bulletSpeed;
        }

        AnimatorChange("IDLE");
        if (!isPressed) return;

        currentPos = Mouse.current.position.ReadValue();
        float dragDelta = currentPos.x - startPos.x;
        float dir = Mathf.Sign(dragDelta);

        float dragAmount = Mathf.Abs(dragDelta);

        // √ 기반 부드러운 속도 증가
        float speedMultiplier = Mathf.Clamp(Mathf.Sqrt(dragAmount) / 10f, 0.2f, 1.5f);
        float finalSpeed = moveSpeed * speedMultiplier;

        transform.Translate(Vector3.right * dir * finalSpeed * Time.deltaTime);
        AnimatorChange("RUN");

    }


    private void AnimatorChange(string temp)
    {
        if (temp == "SHOOT")
        {
            animator.SetTrigger("SHOOT");

            return;
        }

        animator.SetBool("RUN", false);
        animator.SetBool("IDLE", false);

        animator.SetBool(temp, true);
    }

    void ShootBullet()
    {
        SoundManager.instance.AudioStart(1);

        for (int i = 0; i < PosX(bulletCount).Length; i++)
        {
            GameObject go = Instantiate(bullet, new Vector3(transform.position.x + PosX(bulletCount)[i], transform.position.y + 0.5f, transform.position.z + 1.0f), Quaternion.identity);
            Destroy(go, 3.0f);
        }
    }

    IEnumerator ShootCo()
    {
        while (isShooting)
        {
            AnimatorChange("SHOOT");
            ShootBullet();
            yield return new WaitForSeconds(bulletSpeed);
        }

        shootCo = null;
    }

    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            // 마우스 눌렀을 때
            isPressed = true;
            startPos = Mouse.current.position.ReadValue();
            Debug.Log("Press start");
        }
        else
        {
            // 마우스 뗐을 때
            isPressed = false;
            startPos = Vector2.zero;
            currentPos = Vector2.zero;
            Debug.Log("Press end");
        }
    }

    public void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            // 키를 누른 순간
            isShooting = true;
            print("shoot");
        }
        else
        {
            // 키를 뗀 순간
            isShooting = false;
            print("dont shoot");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ATK_Speed")
        {
            SoundManager.instance.AudioStart(2);
            levelUp.Play();
            bulletSpeed -= 0.2f;
            if (bulletSpeed <= 0.2f)
            {
                bulletSpeed = 0.2f;
            }
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "ATK_Count")
        {
            levelUp.Play();
            SoundManager.instance.AudioStart(2);
            bulletCount++;
            if (bulletCount >= 4)
            {
                bulletCount = 4;
            }
            Destroy(other.gameObject);
        }
    }

    private float[] PosX(int count)
    {
        switch (count)
        {
            case 0: return PosX01;
            case 1: return PosX02;
            case 2: return PosX03;
            case 3: return PosX04;
            case 4: return PosX05;
            default: return null;
        }
    }
}
