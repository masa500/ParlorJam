using System.Threading.Tasks;
using UnityEngine;

public interface IState
{
    Task<GameStateResult> DoAction(object data);
}
