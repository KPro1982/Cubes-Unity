using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        button = root.Q<Button>("Button");
        button.clicked += ButtonClicked;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonClicked()
    {
        Debug.Log("Button clicked");
    }
}
