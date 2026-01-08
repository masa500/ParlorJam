using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Line : MonoBehaviour
{

    [SerializeField] private LineRenderer _lineRender;

    // Capacidad inicial para evitar que la lista crezca y se reasigne en memoria
    private List<Vector3> _points = new List<Vector3>(700); 

    public void updateLine(Vector2 point)
    {
        // Convertimos a Vector3 una vez
        Vector3 newPos = new Vector3(point.x, point.y, 0);

        if (_points.Count == 0)
        {
            AddPoint(newPos);
            return;
        }

        // Acceso directo por índice (0 basura, 0 ms)
        if (Vector3.Distance(_points[_points.Count - 1], newPos) > 0.1f)
        {
            AddPoint(newPos);
        }
    }

    private void AddPoint(Vector3 point)
    {
        _points.Add(point);
        _lineRender.positionCount = _points.Count;
        _lineRender.SetPosition(_points.Count - 1, point);
    }
    
    // Llama a esto cuando recicles la línea
    public void ClearLine() {
        _points.Clear();
        _lineRender.positionCount = 0;
    }
}
