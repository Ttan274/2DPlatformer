using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleSystem : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Sprite bgImage;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject field;
    [SerializeField] private int buttonCount;

    //Game Database
    private List<Button> btns = new List<Button>();
    private List<Sprite> gamePuzzles = new List<Sprite>();
    private Sprite[] sprites;

    //Game Logic
    private bool firstGuess, secondGuess;
    private int firstGuessIndex, secondGuessIndex;
    private string firstSelected, secondSelected;
    private string selectedBtn;
    private int gameGuesses, correctAnswerCounter;

    private void Awake()
    {
        sprites = Resources.LoadAll<Sprite>("Fruits");
        field.gameObject.SetActive(false);
    }

    public void StartPuzzle()
    {
        GenerateBtns();
        GenerateImages();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
        field.gameObject.SetActive(true);
    }

    private void GenerateBtns()
    {
        for (int i = 0; i < buttonCount; i++)
        {
            GameObject b = Instantiate(button);
            b.name = i.ToString();
            b.transform.SetParent(field.transform, false);

            btns.Add(b.GetComponent<Button>());
            btns[i].image.sprite = bgImage;
            btns[i].onClick.AddListener(() => CheckPuzzle(b.name));
        }
    }

    private void GenerateImages()
    {
        int len = btns.Count;
        int index = 0;

        for (int i = 0; i < len; i++)
        {
            if (index == len / 2)
                index = 0;

            gamePuzzles.Add(sprites[index]);
            index++;
        }
    }

    private void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randIndex];
            list[randIndex] = temp;
        }
    }

    private void CheckPuzzle(string name)
    {
        if (!firstGuess)
        {
            //Logic setup
            selectedBtn = name;
            firstGuess = true;
            firstGuessIndex = int.Parse(name);

            //Set button visualize
            firstSelected = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if (!secondGuess && selectedBtn != name)
        {
            //Logic setup
            secondGuess = true;
            secondGuessIndex = int.Parse(name);

            //Set button visualize
            secondSelected = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            StartCoroutine(CheckPuzzleRoutine());
        }
    }

    private IEnumerator CheckPuzzleRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstSelected == secondSelected)
        {
            yield return new WaitForSeconds(0.25f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            correctAnswerCounter++;
            if (correctAnswerCounter == gameGuesses)
                FinishGame();
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(0.25f);
        firstGuess = secondGuess = false;
    }

    private void FinishGame()
    {
        field.gameObject.SetActive(false);
    }
}