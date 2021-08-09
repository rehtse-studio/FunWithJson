using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UIManager.Quiz;

namespace QuizGameAPI
{
    [System.Serializable]
    public class QuizAPIRoot
    {
        public List<QuizResult> results;
    }

    [System.Serializable]
    public class QuizResult
    {

        public string category;
        public string type;
        public string difficulty;
        public string question;
        public string correct_answer;
        public List<string> incorrect_answers;

    }

    public class QuizManager : MonoBehaviour
    {
        [Header("Count For Each Answer The User Do")]
        private int _numbersOfClick = 0;

        [Header("Numbers Of Questions")]
        [SerializeField] private int _questionAmount;

        [Header("Visual Help To See The Class Being Populated With The GetRequest")]
        [SerializeField] private QuizAPIRoot _quiz;

        [Header("Reference To The UIManagerQuiz")]
        [SerializeField] private UIManagerQuizGame _uiManager;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                _uiManager.GameOverPanel(false);
                _numbersOfClick = 0;
                StartCoroutine(SendQuizRequest());
            }
        }

        IEnumerator SendQuizRequest()
        {

            using(var quizWebRequest = UnityWebRequest.Get("https://opentdb.com/api.php?amount="+_questionAmount+"&category=27&difficulty=easy&type=boolean"))
            {
                quizWebRequest.SetRequestHeader("Accept", "application/json");

                yield return quizWebRequest.SendWebRequest();

                switch (quizWebRequest.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        _quiz = JsonUtility.FromJson<QuizAPIRoot>(quizWebRequest.downloadHandler.text);
                        ShowQuiz();
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                       Debug.LogError("ConnectionError :: " + quizWebRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        break;
                    default:
                        break;
                }
            }

        }

        private void ShowQuiz()
        {
            _uiManager.CongratsOrFailMessage(" ");

            if (_numbersOfClick + 1 > _quiz.results.Capacity)
            {
                _uiManager.GameOverPanel(true);
            }
            else
            {
                _uiManager.ShowQuestionText(_quiz.results[_numbersOfClick].question.ToString());
            }
        }
        
        public void QuizAnswer(string answer)
        {
            if(answer == _quiz.results[_numbersOfClick].correct_answer)
            {
                _uiManager.CongratsOrFailMessage("CORRECT");
            }
            else
            {
                _uiManager.CongratsOrFailMessage("WRONG");
            }

            _numbersOfClick++;
            Invoke("ShowQuiz", 2f);            
        }

    }

}

