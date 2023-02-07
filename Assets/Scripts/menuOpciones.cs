using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class menuOpciones : MonoBehaviour
{
    public GameObject botonConexion;
    public GameObject saveDataButton;
    public GameObject loadDataButton;
    public Text txt;
    public bool isSaving=false;
    public bool isLoading=false;
    public Text code;
    private ISavedGameClient savedGameClient;
    public void dataCode()
    {
        if(code.text == "7H380R3DH3R0-dev")
        {
            gameData gd = new gameData(gameManager.instance.gems + 10000, gameManager.instance.dinero + 10000, gameManager.instance.nivelVidaExtra, gameManager.instance.nivelVelocidadExtra, gameManager.instance.nivelDanoExtra, gameManager.instance.nivelCritExtra, gameManager.instance.nivelExpExtra, gameManager.instance.nivelPuntosExtra, gameManager.instance.nivelDineroExtra, gameManager.instance.nivelSpawnVida, gameManager.instance.nivelCuracionExtra, gameManager.instance.nivelVelocidadAtaqueExtra, true, true, gameManager.instance.highScore, true);
            gameManager.instance.cargaExterna(gd);
            showToast("Claimed secret dev code", 2);
            GameObject.Find("Menu").GetComponent<updateThings>().updateThingsFunc();
        }
    }
    private void Update()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            botonConexion.GetComponent<Button>().interactable = false;
            botonConexion.GetComponentInChildren<Text>().text = "CONNECTED";
            saveDataButton.GetComponent<Button>().interactable = true;
            loadDataButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            saveDataButton.GetComponent<Button>().interactable = false;
            loadDataButton.GetComponent<Button>().interactable = false;
            botonConexion.GetComponent<Button>().interactable = true;
            botonConexion.GetComponentInChildren<Text>().text = "DISCONNECTED";
        }
    }
    public void logout()
    {
        //PlayGamesPlatform.Instance.SignOut();
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
    //New save try

    public void uiOpenSave(bool saving)
    {
        OpenSave(saving);
        Debug.Log("uiOpenSave _ " + saving);
    }

    public void OpenSave(bool saving, bool loading = false, string guardado = "")
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            isSaving = saving;
            isLoading = loading;
            if (saving)
            {
                Debug.Log("guardando");
                PlayGamesPlatform.Instance.SavedGame.OpenWithAutomaticConflictResolution("Guardado_Unico", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseUnmerged, SaveGameOpen);
            }
            else if(!loading)
            {
                Debug.Log("cargando 1");
                ShowSelectUI();
            }
            else
            {
                Debug.Log("cargando 2");
                PlayGamesPlatform.Instance.SavedGame.OpenWithAutomaticConflictResolution(guardado, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, SaveGameOpen);
            }
            
            //((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("Guardado_Unico", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpen);
        }
    }

    void ShowSelectUI()
    {
        savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.FetchAllSavedGames(DataSource.ReadNetworkOnly, callbackLlamadoLista);
    }
    void callbackLlamadoLista(SavedGameRequestStatus status, List<ISavedGameMetadata> meta)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            uint maxNumToDisplay = 5;
            bool allowCreateNew = false;
            bool allowDelete = true;
            savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            LoadCallBack);
        }
        else
        {
            showToast("Error while attempting to fetch cloud games", 2);
        }
    }

    private void SaveGameOpen(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        Debug.Log("callback");
        if(status == SavedGameRequestStatus.Success)
        {
            Debug.Log("success callback");
            if (isSaving)
            {
                Debug.Log("callback guardando");
                isSaving = false;
                byte[] myData = GetSaveString();
                Debug.Log("update metadata");
                SavedGameMetadataUpdate updateForMetadata = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription("I have updated my game at: " + DateTime.Now.ToString()).Build();
                Debug.Log("commit save");
                PlayGamesPlatform.Instance.SavedGame.CommitUpdate(meta, updateForMetadata, myData, SaveCallBack);
                Debug.Log("saved");
                //((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta,updateForMetadata,myData,SaveCallBack);
            }
            else if(isLoading)
            {
                Debug.Log("callback cargando");
                isLoading = false;
                Debug.Log("cargar datos");
                PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(meta, loadCallback2);
                //((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, LoadCallBack);
            }
        }
    }
    //private void LoadCallBack(SelectUIStatus status, byte[] data)
    private void LoadCallBack(SelectUIStatus status, ISavedGameMetadata data)
    {
        
        if(status == SelectUIStatus.SavedGameSelected)
        {
            //
            //
            //TODO: Mostrar dialog que muestre datos sobre el guardado seleccionado y confirmar su carga o no
            //
            //
            OpenSave(false,true,data.Filename);
        }
        else
        {
            showToast("Error while attempting to load game data from cloud", 2);
        }
    }
    private void loadCallback2(SavedGameRequestStatus status, byte[] data)
    {
        gameData gd = null;
        if (status == SavedGameRequestStatus.Success)
        {
            MemoryStream ms = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();
            gd = formatter.Deserialize(ms) as gameData;
            ms.Close();
            gameManager.instance.cargaExterna(gd);
            showToast("Game data successfully loaded from cloud", 2);
            GameObject.Find("Menu").GetComponent<updateThings>().updateThingsFunc();
        }
        else
        {
            showToast("Error while attempting to load game data from cloud", 2);
        }
    }
    private byte[] GetSaveString()
    {
        gameManager.instance.guardar();
        string path = Application.persistentDataPath + $"/{gameManager.instance.userID}.ub";
        byte[] data = null;
        if (File.Exists(path))
        {
            data = File.ReadAllBytes(path);
        }
        return data;
    }
    private void SaveCallBack(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            showToast("Game data saved successfully in cloud",2);
            Debug.Log("successfully saved in cloud");
        }
        else
        {
            showToast("Error while attempting to save game data in cloud", 2);
            Debug.Log("failed saved in cloud");
        }
    }
    //



    /*
    public void SaveGamePropio()
    {
        uint maxNumToDisplay = 1;
        bool allowCreateNew = true;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
    }
    public void LoadGamePropio()
    {
        uint maxNumToDisplay = 1;
        bool allowCreateNew = true;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelectedLoad);
    }
    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            // handle selected game save
            byte[] temp = new byte[2];
            SaveGame(game, temp, game.TotalTimePlayed);
        }
        else
        {
            // handle cancel or error
        }
    }
    public void OnSavedGameSelectedLoad(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            OpenSavedGame(game.Filename);
        }
        else
        {
            // handle cancel or error
        }
    }

    public void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }
    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("data cargada");
        }
        else
        {
            // handle error
        }
    }
    void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
        Debug.Log("se guardo");
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
        }
        else
        {
            // handle error
        }
    }

    public Texture2D getScreenshot()
    {
        // Create a 2D texture that is 1024x700 pixels from which the PNG will be
        // extracted
        Texture2D screenShot = new Texture2D(1024, 700);

        // Takes the screenshot from top left hand corner of screen and maps to top
        // left hand corner of screenShot texture
        screenShot.ReadPixels(
            new Rect(0, 0, Screen.width, (Screen.width / 1024) * 700), 0, 0);
        return screenShot;
    }
    */

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
