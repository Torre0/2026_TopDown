using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("방향별 위치")]
    public Vector3 rightPosition;
    public Vector3 leftPosition;
    public Vector3 upPosition;
    public Vector3 downPosition;

    [Header("방향별 회전")]
    public Vector3 rightRotation;
    public Vector3 leftRotation;
    public Vector3 upRotation;
    public Vector3 downRotation;

    [Header("현재 장착 무기")]
    public WeaponData currentWeapon;

    [Header("버릴 무기 프리팹")]
    public GameObject weaponDropPrefab;

    [Header("손에 보이는 무기")]
    public SpriteRenderer weaponRenderer;

    [Header("무기 위치 오브젝트")]
    public Transform weaponHolder;

    [Header("무기 거리")]
    public float weaponDistance = 0.3f;

    [Header("휘두르기")]
    public float swingAngle = 90f;
    public float swingSpeed = 10f;

    private bool isSwinging;

    private WeaponPickup nearbyWeapon;
    private PlayerController playerController;

    public Animator weaponAnimator;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();

        RefreshWeaponSprite();
    }

    private void Update()
    {
        UpdateWeaponPosition();

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupWeapon();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            DropWeapon();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void UpdateWeaponPosition()
    {
        if (weaponHolder == null || playerController == null)
            return;

        Vector2 dir = playerController.lookDirection;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            // 좌우
            if (dir.x > 0)
            {
                // 오른쪽
                weaponHolder.localPosition = rightPosition;
                weaponHolder.localEulerAngles = rightRotation;

                weaponRenderer.sortingOrder = 1;
            }
            else
            {
                // 왼쪽
                weaponHolder.localPosition = leftPosition;
                weaponHolder.localEulerAngles = leftRotation;

                weaponRenderer.sortingOrder = 1;
            }
        }
        else
        {
            // 상하
            if (dir.y > 0)
            {
                // 위
                weaponHolder.localPosition = upPosition;
                weaponHolder.localEulerAngles = upRotation;

                weaponRenderer.sortingOrder = -1;
            }
            else
            {
                // 아래
                weaponHolder.localPosition = downPosition;
                weaponHolder.localEulerAngles = downRotation;

                weaponRenderer.sortingOrder = 1;
            }
        }
    }

    public int GetDamage()
    {
        int playerAttack =
            GameDataManager.Instance.GetPlayerAttack();

        if (currentWeapon == null)
            return playerAttack;

        return playerAttack +
            currentWeapon.attackDamage;
    }

    public float GetAttackSpeed()
    {
        if (currentWeapon == null)
            return 0.5f;

        return currentWeapon.attackSpeed;
    }

    public float GetAttackRange()
    {
        if (currentWeapon == null)
            return 0.3f;

        return currentWeapon.attackRange;
    }

    public float GetEffectScale()
    {
        if (currentWeapon == null)
            return 1f;

        return currentWeapon.effectScale;
    }

    void PickupWeapon()
    {
        if (nearbyWeapon == null)
        {
            Debug.Log("근처 무기 없음");
            return;
        }

        // 기존 무기가 있으면 바닥에 떨어뜨림
        if (currentWeapon != null)
        {
            GameObject obj =
                Instantiate(
                    weaponDropPrefab,
                    transform.position + Vector3.down * 0.2f,
                    Quaternion.identity);

            WeaponPickup pickup =
                obj.GetComponent<WeaponPickup>();

            if (pickup != null)
            {
                pickup.weaponData = currentWeapon;
                pickup.RefreshSprite();
            }
        }

        // 새 무기 장착
        currentWeapon = nearbyWeapon.weaponData;

        RefreshWeaponSprite();

        Debug.Log("무기 획득 : " + currentWeapon.weaponName);

        Destroy(nearbyWeapon.gameObject);

        nearbyWeapon = null;
    }

    void DropWeapon()
    {
        if (currentWeapon == null)
            return;

        GameObject obj =
            Instantiate(
                weaponDropPrefab,
                transform.position + Vector3.down * 0.2f,
                Quaternion.identity);

        WeaponPickup pickup =
            obj.GetComponent<WeaponPickup>();

        if (pickup != null)
        {
            pickup.weaponData = currentWeapon;

            pickup.RefreshSprite(); // 추가
        }

        currentWeapon = null;

        RefreshWeaponSprite();
    }

    void RefreshWeaponSprite()
    {
        if (weaponRenderer == null)
            return;

        if (currentWeapon == null)
        {
            weaponRenderer.sprite = null;
        }
        else
        {
            weaponRenderer.sprite = currentWeapon.icon;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        WeaponPickup weapon =
            other.GetComponent<WeaponPickup>();

        if (weapon != null)
        {
            nearbyWeapon = weapon;

            Debug.Log("무기 발견");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        WeaponPickup weapon =
            other.GetComponent<WeaponPickup>();

        if (weapon == nearbyWeapon)
        {
            nearbyWeapon = null;
        }
    }

    IEnumerator SwingWeapon()
    {
        if (isSwinging)
            yield break;

        isSwinging = true;

        float startZ =
            weaponHolder.localEulerAngles.z;

        float targetZ =
            startZ + swingAngle;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * swingSpeed;

            float angle =
                Mathf.Lerp(
                    startZ,
                    targetZ,
                    t);

            Vector3 rot =
                weaponHolder.localEulerAngles;

            rot.z = angle;

            weaponHolder.localEulerAngles = rot;

            yield return null;
        }

        weaponHolder.localEulerAngles =
            new Vector3(
                weaponHolder.localEulerAngles.x,
                weaponHolder.localEulerAngles.y,
                startZ);

        isSwinging = false;
    }

    void Attack()
    {
        int dir = 0;

        Vector2 look = playerController.lookDirection;

        if (Mathf.Abs(look.x) > Mathf.Abs(look.y))
        {
            dir = look.x > 0 ? 0 : 1;
        }
        else
        {
            dir = look.y > 0 ? 2 : 3;
        }

        weaponAnimator.SetInteger("Direction", dir);
        weaponAnimator.SetTrigger("Attack");
    }
}