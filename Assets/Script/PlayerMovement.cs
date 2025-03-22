using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xClampRange = 10f;
    [SerializeField] float yClampRange = 10f;

    [SerializeField] float controlRollFactor = 20f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float controlPitchFactor = 20f;

    Vector2 movement;

    void Update()
    { 
        ProcessTranslation();
        ProcessRotation();
    }


    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void ProcessTranslation()
    {
        float xOffset = movement.x * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;

        float yOffset = movement.y * controlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;

        // 설정한 값 범위로 제한한
        float clampedXPos = Mathf.Clamp(rawXPos, -xClampRange, xClampRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yClampRange, yClampRange);


        transform.localPosition = new Vector3(clampedXPos, clampedYPos, 0f);
    }

    void ProcessRotation()
    {
        float pitch = -controlPitchFactor * movement.y;
        float roll = -controlRollFactor * movement.x;

        Quaternion targetRotation = Quaternion.Euler(pitch, 0f, roll);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
