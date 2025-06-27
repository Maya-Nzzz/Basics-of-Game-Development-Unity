using UnityEngine;
using TMPro;

public class CubeControl : MonoBehaviour
{
    public Rigidbody[] cubes;
    public TMP_Text resultText;
    public TMP_Text promptText;

    private int correctCubeIndex;
    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;

    void Start()
    {
        SaveInitialTransforms();
        StartGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    void StartGame()
    {
        correctCubeIndex = Random.Range(0, cubes.Length);
        resultText.text = "";
        promptText.text = "Выберите куб";
        SetCubesKinematic(true);
    }

    public void OnCubeButtonClick(int index)
    {
        if (index == correctCubeIndex)
        {
            resultText.text = "Победа!\nДля продолжения нажмите пробел";
        }
        else
        {
            resultText.text = "Проигрыш!\nДля продолжения нажмите пробел";
        }

        for (int i = 0; i < cubes.Length; i++)
        {
            if (i != correctCubeIndex)
                cubes[i].isKinematic = false;
        }

        promptText.text = "";
    }

    private void SetCubesKinematic(bool state)
    {
        foreach (var cube in cubes)
        {
            cube.isKinematic = state;
        }
    }

    private void SaveInitialTransforms()
    {
        initialPositions = new Vector3[cubes.Length];
        initialRotations = new Quaternion[cubes.Length];
        for (int i = 0; i < cubes.Length; i++)
        {
            initialPositions[i] = cubes[i].transform.position;
            initialRotations[i] = cubes[i].transform.rotation;
        }
    }

    private void RestartGame()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].linearVelocity = Vector3.zero;
            cubes[i].angularVelocity = Vector3.zero;
            cubes[i].transform.position = initialPositions[i];
            cubes[i].transform.rotation = initialRotations[i];
        }

        StartGame();
    }
}
