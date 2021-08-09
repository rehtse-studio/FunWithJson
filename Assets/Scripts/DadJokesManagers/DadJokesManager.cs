using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a must to use Action Events and WebRequest
using UnityEngine.Networking;
using System;


namespace DadJokesAPI
{

    [System.Serializable]
    public class DadsJokes
    {
        public string id;
        public string joke;
        public int status;

    }

    public class DadJokesManager : MonoBehaviour
    {

        public static Action<string> onGetSendJokeToUI;

        private string _dadJokesURL = "https://icanhazdadjoke.com/";

        [SerializeField]
        private DadsJokes _dadJoke;

        private IEnumerator DadJokeRequest()
        {

            using (var dadJokesRequest = UnityWebRequest.Get(_dadJokesURL))
            {

                //Set Headers
                dadJokesRequest.SetRequestHeader("Accept", "application/json");

                yield return dadJokesRequest.SendWebRequest();
               
                switch (dadJokesRequest.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        //Debug.Log("Conection Done");
                        _dadJoke = JsonUtility.FromJson<DadsJokes>(dadJokesRequest.downloadHandler.text);
                        //Debug.Log("Dad Joke: " + _dadJoke.joke);
                        SendTextToUI(_dadJoke.joke);
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        Debug.LogError(dadJokesRequest.error);
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

        public void StartDadJokeRequest()
        {
            
            StartCoroutine(DadJokeRequest());

        }

        private void SendTextToUI(string _jokeText)
        {

            onGetSendJokeToUI(_jokeText);

        }

    }

}

