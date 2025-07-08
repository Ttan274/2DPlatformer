using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private PuzzleSystem puzzleSystem;

    public void StartPuzzleSystem()
    {
        puzzleSystem.StartPuzzle();
    }
}
