using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GhostFactory _ghostFactory;
    [SerializeField] private Vector2 _minSpawnArea;
    [SerializeField] private Vector2 _maxSpawnArea;

    private List<Ghost> ghosts = new List<Ghost>();

    public void SpawnGhost(int round)
    {
        ghosts.Clear();

        switch (round)
        {
            case 1:
                ghosts.Add(_ghostFactory.Create((int)GhostType.NoMovement));
                break;
            case 2:
                ghosts.Add(_ghostFactory.Create((int)GhostType.NoMovement));
                ghosts.Add(_ghostFactory.Create((int)GhostType.NoMovement));
                break;
            case 3:
                ghosts.Add(_ghostFactory.Create((int)GhostType.Slow));
                break;
            case 4:
                ghosts.Add(_ghostFactory.Create((int)GhostType.Slow));
                ghosts.Add(_ghostFactory.Create((int)GhostType.Slow));
                ghosts.Add(_ghostFactory.Create((int)GhostType.Slow));
                break;
            case 5:
                ghosts.Add(_ghostFactory.Create((int)GhostType.Normal));
                ghosts.Add(_ghostFactory.Create((int)GhostType.Normal));
                break;
            case 6:
                ghosts.Add(_ghostFactory.Create((int)GhostType.GhostHide));
                ghosts.Add(_ghostFactory.Create((int)GhostType.GhostHide));
                break;
            case 7:
                ghosts.Add(_ghostFactory.Create((int)GhostType.GhostHide));
                ghosts.Add(_ghostFactory.Create((int)GhostType.GhostHide));
                ghosts.Add(_ghostFactory.Create((int)GhostType.GhostHide));
                break;
            case 8:
                ghosts.Add(_ghostFactory.Create((int)GhostType.Slow));
                ghosts.Add(_ghostFactory.Create((int)GhostType.Normal));
                ghosts.Add(_ghostFactory.Create((int)GhostType.Fast));
                break;
            case 9:
                ghosts.Add(_ghostFactory.Create((int)GhostType.NoMovement));
                ghosts.Add(_ghostFactory.Create((int)GhostType.GhostHide));
                ghosts.Add(_ghostFactory.Create((int)GhostType.Fast));
                ghosts.Add(_ghostFactory.Create((int)GhostType.Fast));
                break;
            case 10:
                ghosts.Add(_ghostFactory.Create((int)GhostType.Fast));
                ghosts.Add(_ghostFactory.Create((int)GhostType.Fast));
                ghosts.Add(_ghostFactory.Create((int)GhostType.Fast));
                break;
            case 11:
                ghosts.Add(_ghostFactory.Create((int)GhostType.NormalVector));
                ghosts.Add(_ghostFactory.Create((int)GhostType.NormalVector));
                ghosts.Add(_ghostFactory.Create((int)GhostType.ChangeHideMovement));
                ghosts.Add(_ghostFactory.Create((int)GhostType.ChangeHideMovement));
                break;
            case 12:
                ghosts.Add(_ghostFactory.Create((int)GhostType.UFO));
                ghosts.Add(_ghostFactory.Create((int)GhostType.UFO));
                break;
            case 13:
                ghosts.Add(_ghostFactory.Create((int)GhostType.NormalVector));
                ghosts.Add(_ghostFactory.Create((int)GhostType.UFO));
                ghosts.Add(_ghostFactory.Create((int)GhostType.ChangeHideMovement));
                ghosts.Add(_ghostFactory.Create((int)GhostType.NoMovement));
                ghosts.Add(_ghostFactory.Create((int)GhostType.Normal));
                break;
            case 14:
                ghosts.Add(_ghostFactory.Create((int)GhostType.GhostHide));
                ghosts.Add(_ghostFactory.Create((int)GhostType.GhostHide));
                ghosts.Add(_ghostFactory.Create((int)GhostType.ChangeHideMovement));
                ghosts.Add(_ghostFactory.Create((int)GhostType.ChangeHideMovement));
                ghosts.Add(_ghostFactory.Create((int)GhostType.ChangeHideMovement));
                break;
            case 15:
                ghosts.Add(_ghostFactory.Create((int)GhostType.Normal));
                ghosts.Add(_ghostFactory.Create((int)GhostType.Slow));
                ghosts.Add(_ghostFactory.Create((int)GhostType.Fast));
                ghosts.Add(_ghostFactory.Create((int)GhostType.NoMovement));
                ghosts.Add(_ghostFactory.Create((int)GhostType.GhostHide));
                ghosts.Add(_ghostFactory.Create((int)GhostType.ChangeHideMovement));
                ghosts.Add(_ghostFactory.Create((int)GhostType.UFO));
                ghosts.Add(_ghostFactory.Create((int)GhostType.NormalVector));
                break;
            default:
                break;
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
