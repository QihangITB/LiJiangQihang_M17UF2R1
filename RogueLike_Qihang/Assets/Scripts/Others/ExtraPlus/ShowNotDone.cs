using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ShowNotDone : MonoBehaviour
{
    private string DefaultMessage = "Press {0} but NOT DONE :(";
    private float InScreenTime = 2f;

    [SerializeField] private TMP_Text _message;

    public IEnumerator ActiveMessage(string number)
    {
        yield return ActiveMessage(number, InScreenTime);
    }

    public IEnumerator ActiveMessage(string number, float screenTime)
    {
        ShowMessage(string.Format(DefaultMessage, number)); // Cambiamos texto
        _message.gameObject.SetActive(true); // Activamos el mensaje
        yield return new WaitForSeconds(screenTime); // Esperamos un tiempo
        _message.gameObject.SetActive(false); // Desactivamos el mensaje
    }

    public void ShowMessage(string message)
    {
        _message.text = message;
    }

}
