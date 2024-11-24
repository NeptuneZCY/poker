using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggablePoker : MonoBehaviour
{
    private Vector3 offset; // ��������������ĵ�ƫ��
    private bool isDragging = false; // ����Ƿ������϶�
    private Camera mainCamera; // �������

    private void Start()
    {
        mainCamera = Camera.main; // ��ȡ�������
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ������������ʼ
        {
            RaycastHit2D hit = Physics2D.Raycast(GetMouseWorldPosition(), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                offset = transform.position - GetMouseWorldPosition();
                isDragging = true;
            }
        }

        if (isDragging && Input.GetMouseButton(0)) // �϶���
        {
            transform.position = GetMouseWorldPosition() + offset;
        }

        if (Input.GetMouseButtonUp(0)) // ����ͷŻ�������
        {
            isDragging = false;
        }
    }

    private Vector3 ClampPosition(Vector3 position)
    {
        float xMin = -5f, xMax = 5f;
        float yMin = -3f, yMax = 3f;
        position.x = Mathf.Clamp(position.x, xMin, xMax);
        position.y = Mathf.Clamp(position.y, yMin, yMax);
        return position;
    }

    private void OnMouseDown()
    {
        // �����ʱ����ƫ��
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
        Debug.Log($"isDragging: {isDragging}");
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            // ��������λ��Ϊ���λ�ü�ƫ��
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private void OnMouseUp()
    {
        // ֹͣ�϶�
        isDragging = false;
    }

    // ��ȡ������������
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition; // ��ȡ�����Ļ����
        mouseScreenPosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z); // ���ֵ
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition); // ת��Ϊ��������
    }
}