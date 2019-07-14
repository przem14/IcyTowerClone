using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    [SerializeField] float displayingTime = 3f;

    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.enabled = false;
    }

    IEnumerator showMessage(string message)
    {
        text.text = message;
        text.enabled = true;
        yield return new WaitForSeconds(displayingTime);
        text.enabled = false;
    }

    public void ShowMessage(string message)
    {
        StartCoroutine(showMessage(message));
    }

}
