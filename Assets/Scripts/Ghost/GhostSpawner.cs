using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GhostFactory _ghostFactory;
    [SerializeField] private Vector2 _minSpawnArea;
    [SerializeField] private Vector2 _maxSpawnArea;
    [SerializeField] private LevelGhostOrder _easyMode;
    [SerializeField] private LevelGhostOrder _hardMode;

    private List<Ghost> ghosts = new List<Ghost>();

    public void SpawnGhost(int round, bool easyMode)
    {
        ghosts.Clear();

        if (easyMode)
        {
            foreach (GhostType ghost in _easyMode.getGhostByRound(round))
                ghosts.Add(_ghostFactory.Create((int)ghost));
            
        }

        else
        {
            foreach (GhostType ghost in _hardMode.getGhostByRound(round))
                ghosts.Add(_ghostFactory.Create((int)ghost));
            
        }

        foreach (var ghost in ghosts)
        {
            float randomX = Random.Range(_minSpawnArea.x, _maxSpawnArea.x);
            float randomY = Random.Range(_minSpawnArea.y, _maxSpawnArea.y);
            ghost.transform.position = new Vector2(randomX, randomY);
            ghost.transform.SetParent(this.transform);
        }
    }
}
