using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using QuizGameAPI;

namespace UIManager.Quiz
{

    public class UIManagerQuizGame : MonoBehaviour
    {

        [Header("Reference To UI Buttons And Text")]
        [SerializeField] private Text _questionText;
        [SerializeField] private Text _congratsOrFailText;
        [SerializeField] private GameObject _gameOverPanel;

        [Header("Reference To The QuizManager Script")]
        [SerializeField] private QuizManager _quizManager;

        private void Start()
        {
            _questionText.text = "";
            _congratsOrFailText.text = "";
        }
                
        public void ShowQuestionText(string question)
        {
            _questionText.text = question.ToString();
        }

        public void QuestionAnswerUI(string stringAnswer)
        {
            _quizManager.QuizAnswer(stringAnswer);
        }

        public void CongratsOrFailMessage(string congratsOrFail)
        {

            _congratsOrFailText.text = congratsOrFail.ToString();

        }

        public void GameOverPanel(bool setActive)
        {
            _gameOverPanel.SetActive(setActive);
        }

    }


}

