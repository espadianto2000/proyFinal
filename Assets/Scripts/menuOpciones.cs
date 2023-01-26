using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class menuOpciones : MonoBehaviour
{
    public GameObject botonConexion;
    public GameObject cloudData;
    public Text txt;
    public void datos()
    {

    }
    private void Update()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            botonConexion.GetComponent<Button>().interactable = false;
            botonConexion.GetComponentInChildren<Text>().text = "CONNECTED";
            cloudData.GetComponent<Button>().interactable = true;
        }
        else
        {
            cloudData.GetComponent<Button>().interactable = false;
            botonConexion.GetComponent<Button>().interactable = true;
            botonConexion.GetComponentInChildren<Text>().text = "DISCONNECTED";
        }
    }
    public void login()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
            Debug.Log("se logea");
        }
        else
        {
            Debug.Log("continua sin logearse");
            showToast("error al intentar conectar con play services",2);
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }
    public void showToast(string text,
    int duration)
    {
        StartCoroutine(showToastCOR(text, duration));
    }

    private IEnumerator showToastCOR(string text,
        int duration)
    {
        Color orginalColor = txt.color;

        txt.text = text;
        txt.enabled = true;

        //Fade in
        yield return fadeInAndOut(txt, true, 0.5f);

        //Wait for the duration
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out
        yield return fadeInAndOut(txt, false, 0.5f);

        txt.enabled = false;
        txt.color = orginalColor;
    }

    public void ShowSelectUI()
    {
        uint maxNumToDisplay = 5;
        bool allowCreateNew = false;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
    }


    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            // handle selected game save
        }
        else
        {
            // handle cancel or error
        }
    }
    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {

        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }
        Color parentColor = targetText.transform.parent.GetComponent<Image>().color;
        Color currentColor = targetText.color;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            targetText.transform.parent.GetComponent<Image>().color = new Color(parentColor.r, parentColor.g, parentColor.b, alpha);
            yield return null;
        }
    }
}
