using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;  // Ÿ�� ��ġ���


    public bool CreateTower(Tower tower, Vector3 position)  // true �Ǵ� false�� ��ȯ�ϴ� bool Ÿ�� (Waypoint ��ũ��Ʈ���� ���)
    {
        // ����Ÿ�� : "Bank" Ŭ����
        // Bank Ŭ������ ����
        Bank bank = FindObjectOfType<Bank>();

        
/*        if(bank == null)
        {
            return false;
        }*/


        // ���� bankŬ������ ����ݾ��� Ÿ����ġ��뺸�� ���ٸ� 
        if (bank.CurrentBalance >= cost)
        {
            // �ν��Ͻ�ȭ �϶�
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.withdraw(cost); // ���࿡�� cost��ŭ ����
            return true; // true�� ��ȯ�϶�
        }

        return false;  // false�� ��ȯ�϶�
    }
}
