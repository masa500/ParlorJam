using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Line : MonoBehaviour
{

    [SerializeField] private LineRenderer _lineRender;

    private List<Vector2> _points = new List<Vector2>();

    void SetPoints(Vector2 point){

        _points.Add(point);

        _lineRender.positionCount = _points.Count;
        _lineRender.SetPosition(_points.Count - 1, point);
    }

    public void updateLine(Vector2 point){

        Vector2 refinedPoint = new Vector2((Mathf.Round(point.x * 10f)) / 10f, (Mathf.Round(point.y * 10f)) / 10f);

        if(_points == null || _points.Count == 0){
            _points = new List<Vector2>();
            SetPoints(refinedPoint); 
            return;
        }

        else if(Vector2.Distance(_points.Last(), refinedPoint) > 0.2f){

            if(!_points.Contains(refinedPoint))
                SetPoints(refinedPoint);
        }
    }
}
