using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DadJokesAPI;

namespace UIManager.DadJokes
{

    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text _dadJokeTextReference;

        private void Start()
        {
            _dadJokeTextReference.text = "";
        }
        private void OnEnable()
        {
            DadJokesManager.onGetSendJokeToUI += UpdateDadJokeText;
        }

        private void UpdateDadJokeText(string jokeText)
        {
            _dadJokeTextReference.text = jokeText.ToString();
        }

        private void OnDisable()
        {
            DadJokesManager.onGetSendJokeToUI -= UpdateDadJokeText;
        }

    }

}

