using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Shape : MonoBehaviour
{
    private LayerMask slotToCheck;

    private SpriteRenderer spriteRenderer;
    private Sprite spriteToCheck;

    private Camera mainCamera;

    private Vector2 defaultPosition;
    private Vector2 offset;
    private Vector2 screenToWorldPos;

    readonly float RAY_DISTANCE = 10f;
    private bool isDraggable = true;

    void Start()
    {
        Initialize();
    }

    private void OnMouseDown()
    {
        if(isDraggable && !GameManager.Instance.IsGameOver)
            OnShapeSelected();
    }

    private void OnMouseDrag()
    {
        if (isDraggable && !GameManager.Instance.IsGameOver)
            OnShapeDragging();
    }

    private void OnMouseUp()
    {
        if (isDraggable && !GameManager.Instance.IsGameOver)
            OnShapeDeselected();
    }

    private void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteToCheck = spriteRenderer.sprite;

        slotToCheck = LayerMask.GetMask("BoardSlot");
        mainCamera = Camera.main;

        defaultPosition = transform.position;
    }

    private void OnShapeSelected()
    {
        screenToWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = (Vector2)transform.position - screenToWorldPos;
    }

    private void OnShapeDragging()
    {
        screenToWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 newPosition = screenToWorldPos + offset;
        transform.position = newPosition;
    }

    private void OnShapeDeselected()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hit = Physics2D.GetRayIntersectionAll(ray, RAY_DISTANCE, slotToCheck);

        if (hit.Length > 0)
        {
            SpriteRenderer _spriteRenderer = hit[0].transform.GetComponent<SpriteRenderer>();

            if (_spriteRenderer.sprite == spriteToCheck)
            {
                transform.position = _spriteRenderer.transform.position;

                SoundManager.Instance.PlayAudio(AudioType.Correct);
                isDraggable = false;

                GameManager.Instance.IncreaseCorrectCount();
            }
            else
            {
                ResetPosition();
                SoundManager.Instance.PlayAudio(AudioType.Wrong);
            }
        }
        else
            ResetPosition();
    }

    private void ResetPosition()
    {
        transform.position = defaultPosition;
    }
}
