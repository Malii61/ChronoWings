using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClockSpawner : MonoBehaviour
{
    [SerializeField] private Transform clockPrefab;

    private List<Transform> clocks = new();
    private bool isGameFinished;

    private void Start()
    {
        GameHandler.Instance.OnDied += OnFinished;
        clockPrefab.gameObject.SetActive(false);
        SpawnClock();
    }

    private void OnFinished(object sender, EventArgs e)
    {
        isGameFinished = true;
    }

    private void FixedUpdate()
    {
        if (!isGameFinished)
            MoveClocks();
    }

    private void SpawnClock()
    {
        var pos = GetRandomPosition();
        var clock = Instantiate(clockPrefab, pos, Quaternion.identity);
        clock.gameObject.SetActive(true);
        clocks.Add(clock);

        if (!isGameFinished)
            Invoke(nameof(SpawnClock), 1.2f);
    }

    private Vector2 GetRandomPosition()
    {
        var randomPos = new Vector2(Random.Range(30f, 70f), Random.Range(-40f, 50f));
        var result = Physics2D.OverlapCircle(randomPos, 5f);
        while (result != null)
        {
            randomPos = new Vector2(Random.Range(30f, 70f), Random.Range(-40f, 50f));
            result = Physics2D.OverlapCircle(randomPos, 5f);
        }

        return randomPos;
    }

    private void MoveClocks()
    {
        for (int i = 0; i < clocks.Count; i++)
        {
            var clock = clocks[i];
            clock.Translate(new Vector3(-1, 0, 0) * Level.GetInstance().PIPE_MOVE_SPEED * Time.fixedDeltaTime);
            if (clock.position.x < -100)
            {
                clocks.Remove(clock);
                Destroy(clock.gameObject);
            }
        }
    }
}