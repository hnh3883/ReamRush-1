using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]// 이거 뭐지?
[RequireComponent(typeof(TextMeshPro))] // 인스펙터 창에서 추가할 수 있는 속성들을 코드로 추가

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
        
        // 시작할때 좌표값을 보여주기 위해 Awake에서 한 번 실행
        DisplayCoordinates();
    }

    void Update()
    {
        // 실행 중이지 않다면 다음을 실행하라
        // 환경조성할때만 아래 코드가 필요하기 때문 (게임실행할 때는 필요X)
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
        // vector3 타입과 vector2 타입이 매칭이 안되기 때문에 정수로 변환하여 넣어줌
        // snap으로 나눠주는 이유는 10단위씩 움직이도록 snap을 설정해서 1단위로 변경해주기 위함
        // 유니티 상의 좌표는 (10,10) 이라면 tile 상의 좌표는 (1,1)이 되도록 함
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x/ UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z/ UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        // 하이어라키 창에서의 이름을 좌표값으로 변경 (string 형태로 변환하여 받아옴)
        transform.parent.name = coordinates.ToString();
    }
}
