using Tanks;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    [SerializeField] private InputReader inputPlayer;


    private void OnEnable()
    {
        inputPlayer.MoveEvent += HandleMove;
    }

    private void HandleMove(Vector2 movement)
    {
        Debug.Log(movement);
    }

    private void OnDisable()
    {
        inputPlayer.MoveEvent -= HandleMove;
    }
}
