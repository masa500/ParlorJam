using UnityEngine;

public class GhostFactory : MonoBehaviour
{
    [SerializeField] private Ghost _normalGhost;
    [SerializeField] private Ghost _slowGhost;
    [SerializeField] private Ghost _fastGhost;
    [SerializeField] private Ghost _noMovementGhost;
    [SerializeField] private Ghost _ghostHide;
    [SerializeField] private Ghost _changeHideMovementGhost;
    [SerializeField] private Ghost _ufoGhost;
    [SerializeField] private Ghost _normalVectorGhost;


    public Ghost Create(int id)
    {
        switch (id)
        {
            case 0:
                return Instantiate(_normalGhost);
            case 1:
                return Instantiate(_noMovementGhost);
            case 2:
                return Instantiate(_slowGhost);
            case 3:
                return Instantiate(_fastGhost);
            case 4:
                return Instantiate(_ghostHide);
            case 5:
                return Instantiate(_ufoGhost);
            case 6:
                return Instantiate(_normalVectorGhost);
            case 7:
                return Instantiate(_changeHideMovementGhost);
            default:
                return null;
        }
    }
}
