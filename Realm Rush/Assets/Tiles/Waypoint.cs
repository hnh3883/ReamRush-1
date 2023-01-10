using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerprefab;
    [SerializeField] bool isplaceable;


    // get �޼ҵ�� ������ ��ȯ
    // �б� ���� �޼ҵ尡 �Ǿ����  (���������� set)
    // isplaceable�� ���� ��ȯ
    public bool Isplaceable { get { return isplaceable; } }

    void OnMouseDown()  // ���콺�� ��������
    {
        // �ν����Ϳ��� isplaceable�� üũ�ؼ� üũ�Ȱ͸� Ŭ���ϸ� �������� (�ν����� â �����ֱ�)
        // ���� �ٴϴ� ��δ� ��ġ���� ���ϱ� ����

        // ���� isplaceable�� true��� (Ÿ���� ��ġ �Ҽ� �ִ� Ÿ���̶��)
        if (isplaceable)
        {
            // Tower ��ũ��Ʈ�� CreateTower �޼ҵ忡�� true�� false ���� �޾ƿ�
            // ���� ���� ���� Ÿ�� ��ġ��뺸�� ���ٸ� true�� ��ȯ (CreateTower���� �ν��Ͻ�ȭ ����)
            bool isPlaced = towerprefab.CreateTower(towerprefab, transform.position);

            Debug.Log(transform.name);

            // Ÿ���� ������ �� �ִٸ� isPlaced = true���ٵ�
            // Ÿ���� ������ Ÿ���̶�� �ߺ� ��ġ�� ������
            // isplaceable = !isPlaced = false �� ���� �ٲپ �� ������� �ʵ��� ��
            isplaceable = !isPlaced;
            
        }
    }
}
