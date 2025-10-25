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

        if(_points == null || _points.Count == 0){
            _points = new List<Vector2>();
            SetPoints(point); 
            return;
        }
        
        else if(Vector2.Distance(_points.Last(), point) > 0.1f){
            SetPoints(point);
        }
    }
}
