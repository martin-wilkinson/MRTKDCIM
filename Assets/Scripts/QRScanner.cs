using UnityEngine;
using UnityEngine.Networking;
using Microsoft.MixedReality.QR;
using System.Collections;
using System;
using MixedReality.Toolkit.UX;
using TMPro;

public class QRCodeDetector : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public DialogPool dialogPool;
    private QRCodeWatcher qrWatcher;
    private int checkCounter = 0;
    private QRCodeWatcherAccessStatus accessStatus;
    private bool QRCodeStarted = false;

    async protected virtual void Start()
    {
        Debug.Log("hello");
        textMeshProUGUI.text = "hello";
        if (QRCodeWatcher.IsSupported())
        {
            try
            {
                accessStatus = await QRCodeWatcher.RequestAccessAsync();
                Debug.Log(accessStatus.ToString());
                textMeshProUGUI.text = accessStatus.ToString();
            }
            catch
            {
                Debug.Log("Failed to get access status");
            }

        }
    }

    void Update()
    {
        if (checkCounter == 60)
        {
            textMeshProUGUI.text += accessStatus.ToString();
            checkCounter = 0;
        }
        {
           
        }
        if (accessStatus == QRCodeWatcherAccessStatus.Allowed)
        {
            if (QRCodeStarted == false)
            {
                textMeshProUGUI.text += "started check";
                qrWatcher = new QRCodeWatcher();
                qrWatcher.Added += OnQRCodeAdded;
                qrWatcher.Start();
                QRCodeStarted = true;
                textMeshProUGUI.text += "started true";
            }
        }
        checkCounter++;
    }



    private void OnQRCodeAdded(object sender, QRCodeAddedEventArgs args)
    {
        if (args.Code.Data == "Kris Sucks")
        {
            textMeshProUGUI.text += "QR read check";
            textMeshProUGUI.text += args.Code.Data;
            IDialog dialog = dialogPool.Get()
                .SetHeader("Kris Sucks and I'm Testing that")
                .SetBody("Please tell me this works");
            dialog.Show();
            textMeshProUGUI.text += "diag.Show()";
            //StartCoroutine(LoadJsonFromURL("http://headers.jsontest.com/"));
        }
    }

    IEnumerator LoadJsonFromURL(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                DisplayJsonContent(webRequest.downloadHandler.text);
            }
        }
    }



    protected virtual void OnEnable()
    {
        if (dialogPool == null)
        {
            dialogPool = GetComponent<DialogPool>();
        }
    }
    private void DisplayJsonContent(string jsonContent)
    {  

        IDialog dialog = dialogPool.Get()
            .SetHeader("Kris Sucks and I'm Testing that")
            .SetBody(jsonContent);
        dialog.Show();
        // Implement how you want to display the JSON content.
        // For example, update a UI Text element with the jsonContent.
        Debug.Log("JSON Content: " + jsonContent);
    }

    void OnDestroy()
    {
        if (qrWatcher != null)
        {
            qrWatcher.Stop();
            qrWatcher.Added -= OnQRCodeAdded;
        }
    }
}

