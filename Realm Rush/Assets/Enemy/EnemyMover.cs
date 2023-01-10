using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ν����� â���� �߰��� �� �ִ� �Ӽ����� �ڵ�� �߰�
// EnemyMover ��ũ��Ʈ�� �߰��ص� �ڵ����� Enemy ��ũ��Ʈ�� �߰���.
[RequireComponent(typeof(Enemy))] 

public class EnemyMover : MonoBehaviour
{
    // �̵� ��θ� ����� �迭�� ������
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    
    // ������ �̵� �ӵ��� �����ϰ�, ������ 0~5 ���̷� ����
    [SerializeField] [Range(0f, 5f)]float speed = 1f;   // ������ ���� 116 10�д뿡 ����

    // ����Ÿ�� : "Enemy" Ŭ���� (�ش� Ŭ������ �����ϱ� ����)
    Enemy enemy;

    //void start�� �� �� �ۿ� ����ȵǼ� OnEnable �޼ҵ� ���
    // OnEnable�� ���̾��Ű���� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ �� �� ȣ��
    // EnemyHealth Ŭ������ ProcessHit �޼ҵ忡�� ���� ������ ��Ȱ��ȭ�� �ǵ��� �ڵ� �ۼ��� 
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }
     void Start()
    {
        // �ܺ� �޼ҵ忡 ����
        enemy = GetComponent<Enemy>();
    }


    void FindPath()
    {
        // �迭�� �����
        path.Clear();

        // ���ӿ�����Ʈ Ÿ������ �θ� ����(�ڽĵ��� ��� Ÿ��), "Path" �±װ� �پ��ִ� ������Ʈ�� �����
        // Path ������Ʈ�ȿ� �ڽĵ��� ��� Ÿ��
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        // Path ������Ʈ�� Path �±װ� �پ��ְ�, �� �ȿ� ���Ÿ�ϵ��� �ڽ����� ����
        // foreach(�ڷ��� element in �׷��� ������)
        // �ڽ�Ÿ�ϵ��� ó������ ������ ��ȯ�ϰ� ��
        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if(waypoint != null)
            {
                // path �迭�� waypoints ���� �迭���� �־���
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart()
    {
        // �� ��ũ��Ʈ�� ���� ������Ʈ(��)�� ��ġ�� path �迭�� ù ��° ��ġ�� �̵�(�������)
        transform.position = path[0].transform.position;
    }

    void FinishPath() // ���� ������ �������� �� ������ �޼ҵ�
    {
        // ���� ���������� �������� penalty�� �ο�
        enemy.StealGold();

        // ������Ʈ(��)�� ��Ȱ��ȭ
        gameObject.SetActive(false);
    } 
    
    IEnumerator FollowPath()  // �ڿ������� �������� ��Ÿ���� �Լ�
    {
        // path �迭�� ó������ ���������� �ݺ��ϰڴ�. (waypoint�� ���ڰ�)
        foreach(Waypoint waypoint in path)
        {
            // ���� 116 7:30��

            // ������ġ�� ���� ������Ʈ(��)�� ��ġ
            Vector3 startPosition = transform.position;

            // ������ġ�� path�� ���̸�, �迭�� ������ �ݺ� (waypoint�� path �迭���� �� ���ڰ�)
            Vector3 endPosition = waypoint.transform.position;
            
            // �ڿ����� �������� ���� ����
            float travelPercent = 0f;

            // ������ġ�� �ٶ󺸰��� (ȸ���� ��Ÿ���� �ڵ�)
            transform.LookAt(endPosition);

            while(travelPercent < 1f) // �������� 
            {
                // �� �����ӿ� �ɸ� �ð��� ������ (speed ������ �ӵ� ���� ����)
                travelPercent += Time.deltaTime * speed;

                // ����������(Lerp)�� �̿��ؼ� �ڿ������� �������� ǥ����
                // Vector3.Lerp(��ġ1, ��ġ2, 0~1 ���̰�) 
                // travelPercent�� 1�� �Ǵ� ���� ��ġ1���� ��ġ2�� �̵�
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                // yield return�� ��� �������� �Ѱ��ִ� ��
                // WaitForEndOfFrame�� �������� ������ ��ٸ� ���� �ٽ� �ڷ�ƾ�� �����ϴ� �ڵ�
                yield return new WaitForEndOfFrame(); 
                // ����113 4:40�� ��
            }

        }

        FinishPath(); // ���� ������ �������� �� ���Ƽ�� ���̰�, ��Ȱ��ȭ�� ���Ѷ�
    }
}
