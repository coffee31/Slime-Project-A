using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimit : MonoBehaviour
{
    float minX = -32f;
    float maxX = 32f;
    float minY = -12f;
    float maxY = 13f;

    // �÷��̾��� Transform ������Ʈ
    private Transform playerTransform;

    private Player player;

    private void Start()
    {
        // �÷��̾��� Transform ������Ʈ ��������
        playerTransform = GetComponent<Transform>();
        player = GetComponent<Player>();
        initset();

    }

    private void Update()
    {
        // ���� �÷��̾��� ��ġ
        Vector3 playerPosition = playerTransform.position;

        // X ��ǥ�� ������ ����� ���
        if (playerPosition.x < minX)
        {
            playerPosition.x = minX;
            player.targetPosition = playerPosition;
        }
        else if (playerPosition.x > maxX)
        {
            playerPosition.x = maxX;
            player.targetPosition = playerPosition;
        }

        // Y ��ǥ�� ������ ����� ���
        if (playerPosition.y < minY)
        {
            playerPosition.y = minY;
            player.targetPosition = playerPosition;
        }
        else if (playerPosition.y > maxY)
        {
            playerPosition.y = maxY;
            player.targetPosition = playerPosition;
        }

        // ���ѵ� ��ġ�� �÷��̾� �̵�
        playerTransform.position = playerPosition;
    }

    void initset()
    {
        minX = -20f;
        maxX = 20f;
        minY = -12f;
        maxY = 13f;
    }
}
