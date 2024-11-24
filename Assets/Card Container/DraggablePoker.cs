using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggablePoker : MonoBehaviour
{
    private Vector3 offset; // 鼠标点击与物体中心的偏移
    private bool isDragging = false; // 标记是否正在拖动
    private Camera mainCamera; // 主摄像机

    private void Start()
    {
        mainCamera = Camera.main; // 获取主摄像机
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 鼠标左键或触摸开始
        {
            RaycastHit2D hit = Physics2D.Raycast(GetMouseWorldPosition(), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                offset = transform.position - GetMouseWorldPosition();
                isDragging = true;
            }
        }

        if (isDragging && Input.GetMouseButton(0)) // 拖动中
        {
            transform.position = GetMouseWorldPosition() + offset;
        }

        if (Input.GetMouseButtonUp(0)) // 鼠标释放或触摸结束
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
        // 鼠标点击时计算偏移
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
        Debug.Log($"isDragging: {isDragging}");
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            // 更新物体位置为鼠标位置加偏移
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private void OnMouseUp()
    {
        // 停止拖动
        isDragging = false;
    }

    // 获取鼠标的世界坐标
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition; // 获取鼠标屏幕坐标
        mouseScreenPosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z); // 深度值
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition); // 转换为世界坐标
    }
}