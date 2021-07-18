using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private Button randomButton;

    private Button map1Button;
    
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        randomButton = root.Q<Button>("Random");
        randomButton.clicked += RandomButtonClicked;
        map1Button = root.Q<Button>("Map1");
        map1Button.clicked += Map1ButtonClicked;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomButtonClicked()
    {
        SceneManager.LoadScene("GenerateRandom");
    }
    void Map1ButtonClicked()
    {
        SceneManager.LoadScene("Map1");
    }
}
