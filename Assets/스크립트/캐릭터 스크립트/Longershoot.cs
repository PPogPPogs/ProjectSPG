using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longershoot : MonoBehaviour
{
    private Transform monster;
    public GameObject arrowPrefab; // ȭ�� ������
    public float arrowSpeed = 10f; // ȭ�� �߻� �ӵ�
    public float gravity = 9.81f; // �߷� ���ӵ�
    private float nextAttackTime = 0.0f;
    private float currentAttackCooldown = 0.0f;
    public float minAttackCooldown = 2.0f; // �ּ� ���� ��Ÿ��
    public float maxAttackCooldown = 5.0f; // �ִ� ���� ��Ÿ��
    private Animator animator;



    private void Start()
    {
        animator = GetComponent<Animator>();
    }



    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger("Attack");
            Vector3 direction = monster.position - transform.position;
            float distance = direction.magnitude;

            // ������ � ���
            float time = distance / arrowSpeed;
            float verticalSpeed = arrowSpeed - (gravity * time) / 2;
            Vector3 initialVelocity = direction.normalized * arrowSpeed;
            initialVelocity.y = verticalSpeed;

            // ȭ�� �߻�
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
            arrowRb.velocity = initialVelocity;
            // ���⿡ ���� ���� ������ ����
            Debug.Log("���Ͱ� �÷��̾ �����մϴ�!");
            currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
            nextAttackTime = Time.time + currentAttackCooldown;
        }
    }
}