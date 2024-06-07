using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimit : MonoBehaviour
{
    float minX = -32f;
    float maxX = 32f;
    float minY = -12f;
    float maxY = 13f;

    // 플레이어의 Transform 컴포넌트
    private Transform playerTransform;

    private Player player;

    private void Start()
    {
        // 플레이어의 Transform 컴포넌트 가져오기
        playerTransform = GetComponent<Transform>();
        player = GetComponent<Player>();
        initset();

    }

    private void Update()
    {
        // 현재 플레이어의 위치
        Vector3 playerPosition = playerTransform.position;

        // X 좌표가 범위를 벗어나는 경우
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

        // Y 좌표가 범위를 벗어나는 경우
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

        // 제한된 위치로 플레이어 이동
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
