using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]// �̰� ����?
[RequireComponent(typeof(TextMeshPro))] // �ν����� â���� �߰��� �� �ִ� �Ӽ����� �ڵ�� �߰�

public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        waypoint = GetComponentInParent<Waypoint>();
        
        // �����Ҷ� ��ǥ���� �����ֱ� ���� Awake���� �� �� ����
        DisplayCoordinates();
    }

    void Update()
    {
        // ���� ������ �ʴٸ� ������ �����϶�
        // ȯ�������Ҷ��� �Ʒ� �ڵ尡 �ʿ��ϱ� ���� (���ӽ����� ���� �ʿ�X)
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    void SetLabelColor()
    {
        if (waypoint.Isplaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void DisplayCoordinates()
    {
        // vector3 Ÿ�԰� vector2 Ÿ���� ��Ī�� �ȵǱ� ������ ������ ��ȯ�Ͽ� �־���
        // snap���� �����ִ� ������ 10������ �����̵��� snap�� �����ؼ� 1������ �������ֱ� ����
        // ����Ƽ ���� ��ǥ�� (10,10) �̶�� tile ���� ��ǥ�� (1,1)�� �ǵ��� ��
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x/ UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z/ UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        // ���̾��Ű â������ �̸��� ��ǥ������ ���� (string ���·� ��ȯ�Ͽ� �޾ƿ�)
        transform.parent.name = coordinates.ToString();
    }
}
