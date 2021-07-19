using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private Button map1Button;
    private Button randomButton;

    // Start is called before the first frame update
    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        randomButton = root.Q<Button>("Random");
        randomButton.clicked += RandomButtonClicked;
        map1Button = root.Q<Button>("Map1");
        map1Button.clicked += Map1ButtonClicked;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void RandomButtonClicked()
    {
        SceneManager.LoadScene("GenerateRandom");
    }

    private void Map1ButtonClicked()
    {
        SceneManager.LoadScene("Map1");
    }
}