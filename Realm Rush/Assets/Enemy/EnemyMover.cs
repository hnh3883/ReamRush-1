using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인스펙터 창에서 추가할 수 있는 속성들을 코드로 추가
// EnemyMover 스크립트만 추가해도 자동으로 Enemy 스크립트가 추가됨.
[RequireComponent(typeof(Enemy))] 

public class EnemyMover : MonoBehaviour
{
    // 이동 경로를 담아줄 배열을 선언함
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    
    // 적군의 이동 속도를 선언하고, 범위는 0~5 사이로 설정
    [SerializeField] [Range(0f, 5f)]float speed = 1f;   // 레인지 강의 116 10분대에 나옴

    // 참조타입 : "Enemy" 클래스 (해당 클래스에 접근하기 위함)
    Enemy enemy;

    //void start는 한 번 밖에 실행안되서 OnEnable 메소드 사용
    // OnEnable은 하이어라키에서 활성화 또는 비활성화 될 때 호출
    // EnemyHealth 클래스의 ProcessHit 메소드에서 적이 죽으면 비활성화가 되도록 코드 작성됨 
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }
     void Start()
    {
        // 외부 메소드에 접근
        enemy = GetComponent<Enemy>();
    }


    void FindPath()
    {
        // 배열을 비워줌
        path.Clear();

        // 게임오브젝트 타입으로 부모를 선언(자식들은 경로 타일), "Path" 태그가 붙어있는 오브젝트를 담아줌
        // Path 오브젝트안에 자식들은 경로 타일
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        // Path 오브젝트에 Path 태그가 붙어있고, 그 안에 경로타일들이 자식으로 있음
        // foreach(자료형 element in 그룹형 변수명)
        // 자식타일들을 처음부터 끝까지 순환하게 됨
        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if(waypoint != null)
            {
                // path 배열에 waypoints 안의 배열들을 넣어줌
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart()
    {
        // 이 스크립트가 붙은 오브젝트(적)의 위치는 path 배열의 첫 번째 위치로 이동(출발지점)
        transform.position = path[0].transform.position;
    }

    void FinishPath() // 적이 끝까지 도착했을 때 실행할 메소드
    {
        // 적을 도착지까지 못막으면 penalty를 부여
        enemy.StealGold();

        // 오브젝트(적)를 비활성화
        gameObject.SetActive(false);
    } 
    
    IEnumerator FollowPath()  // 자연스러운 움직임을 나타내는 함수
    {
        // path 배열을 처음부터 마지막까지 반복하겠다. (waypoint는 인자값)
        foreach(Waypoint waypoint in path)
        {
            // 강의 116 7:30초

            // 시작위치는 현재 오브젝트(적)의 위치
            Vector3 startPosition = transform.position;

            // 종료위치는 path의 값이며, 배열의 끝까지 반복 (waypoint는 path 배열안의 한 인자값)
            Vector3 endPosition = waypoint.transform.position;
            
            // 자연스런 움직임을 위한 변수
            float travelPercent = 0f;

            // 종료위치를 바라보게함 (회전을 나타내는 코드)
            transform.LookAt(endPosition);

            while(travelPercent < 1f) // 종료지점 
            {
                // 한 프레임에 걸린 시간을 더해줌 (speed 변수로 속도 조절 가능)
                travelPercent += Time.deltaTime * speed;

                // 선형보간법(Lerp)을 이용해서 자연스러운 움직임을 표현함
                // Vector3.Lerp(위치1, 위치2, 0~1 사이값) 
                // travelPercent가 1이 되는 순간 위치1에서 위치2로 이동
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                // yield return은 잠시 통제권을 넘겨주는 것
                // WaitForEndOfFrame은 프레임의 끝까지 기다린 다음 다시 코루틴을 시작하는 코드
                yield return new WaitForEndOfFrame(); 
                // 강의113 4:40초 대
            }

        }

        FinishPath(); // 적이 끝까지 도착했을 때 페널티를 먹이고, 비활성화를 시켜라
    }
}
