using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerprefab;
    [SerializeField] bool isplaceable;


    // get 메소드는 변수만 반환
    // 읽기 전용 메소드가 되어버림  (쓰기전용은 set)
    // isplaceable의 값을 반환
    public bool Isplaceable { get { return isplaceable; } }

    void OnMouseDown()  // 마우스를 눌렀을때
    {
        // 인스펙터에서 isplaceable을 체크해서 체크된것만 클릭하면 나오게함 (인스펙터 창 보여주기)
        // 적이 다니는 통로는 설치하지 못하기 위함

        // 만약 isplaceable이 true라면 (타워를 설치 할수 있는 타일이라면)
        if (isplaceable)
        {
            // Tower 스크립트의 CreateTower 메소드에서 true나 false 값을 받아옴
            // 현재 가진 돈이 타워 설치비용보다 많다면 true를 반환 (CreateTower에서 인스턴스화 진행)
            bool isPlaced = towerprefab.CreateTower(towerprefab, transform.position);

            Debug.Log(transform.name);

            // 타워를 생성할 수 있다면 isPlaced = true일텐데
            // 타워가 생성된 타일이라면 중복 설치를 막도록
            // isplaceable = !isPlaced = false 로 값을 바꾸어서 또 실행되지 않도록 함
            isplaceable = !isPlaced;
            
        }
    }
}
