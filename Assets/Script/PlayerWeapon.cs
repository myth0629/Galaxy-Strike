using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] GameObject[] lasers;
    [SerializeField] RectTransform crosshair;
    [SerializeField] Transform targetPoint;
    [SerializeField] float targetDistance = 100f;
    bool isFiring = false;

    private void Start() 
    {
        Cursor.visible = false;
    }

    void Update()
    {
        ProcessingFiring();
        MoveCrosshair();
        MoveTargetPoint();
        AimLasers();
    }

    public void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
    }

    void ProcessingFiring()
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isFiring;
        }
    }

    void MoveCrosshair()
    {
        crosshair.position = Input.mousePosition;
    }

    void MoveTargetPoint()
    {
        // x,y값은 마우스 위치, z값은 변수로 입력력
        Vector3 targetPointPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetDistance);
        targetPoint.position = Camera.main.ScreenToWorldPoint(targetPointPosition);
    }

    void AimLasers()
    {
        foreach (GameObject laser in lasers)
        {
            // 목표 위치 - 레이저 위치 = 레이저와 목표 지점 사이 거리 (Vector)
            Vector3 fireDirection = targetPoint.position - transform.position;
            // 레이저를 위 벡터에 맞추도록 회전 계산산
            Quaternion rotationToTarget = Quaternion.LookRotation(fireDirection);
            // 레이저의 rotation을 
            laser.transform.rotation = rotationToTarget;
        }
    }
}
