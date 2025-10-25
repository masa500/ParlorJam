using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private InputReader _controls = default;
    [SerializeField] private Scratch _scratch;
    [SerializeField] private Transform _linesParent;
    [SerializeField] private CircleCollider2D _cc2d;
    private Line _activeLine;

    void OnEnable()
    {
        _controls.clickPerformedEvent += PressDrawKey;
        _controls.clickCanceledEvent += ReleaseDrawKey;
        _controls.mousePositionEvent += MousePos;
    }

    void OnDisable()
    {
        _controls.clickPerformedEvent -= PressDrawKey;
        _controls.clickCanceledEvent -= ReleaseDrawKey;
        _controls.mousePositionEvent -= MousePos;
    }

    private void PressDrawKey()
    {
        GameObject newline = Instantiate(_linePrefab, _linesParent);
        _activeLine = newline.GetComponent<Line>();
        _cc2d.enabled = true;
    }

    private void ReleaseDrawKey()
    {
        _activeLine = null;
        _cc2d.enabled = false;
    }

    private void MousePos(Vector2 pos)
    {
        if(_activeLine != null)
        {
            Vector2 worldPos = _mainCamera.ScreenToWorldPoint(pos);
            _cc2d.transform.position = worldPos;
            _activeLine.updateLine(worldPos);
            _scratch.AssignScreenAsMask();
        }
    }
}
