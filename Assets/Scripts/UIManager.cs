using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public UIDocument HudRoot;
    public GameObject leukocytePrefab;
    public GameManager gameManager;


    #region [ UI Elements ]
    private Label infectionLabel;
    private Label fundLabel;
    private Button leukocytesButton;
    #endregion

    void OnEnable()
    {
        var root = HudRoot.rootVisualElement;
        infectionLabel = root.Q<Label>("infectionValue");
        fundLabel = root.Q<Label>("ATPValue");
        leukocytesButton = root.Q<Button>("shopLeukocyte");

        leukocytesButton.RegisterCallback<ClickEvent>(ev => buyLeukocyte());

        UpdateInfectionDisplay();
        UpdateFundDisplay();
    }

    private void buyLeukocyte()
    {
        var objectPlacer = GetComponent<ObjectPlacer>();
        if (objectPlacer.currentPlaceableObject != null) return;

        if (!gameManager.Spend(20)) return;

        var leukocyte = Instantiate(leukocytePrefab, transform.position, Quaternion.identity);
        objectPlacer.currentPlaceableObject = leukocyte.GetComponent<PlaceableObject>();

        UpdateFundDisplay();
    }

    private void UpdateFundDisplay()
    {
        fundLabel.text = gameManager.GetFund().ToString();
    }

    private void UpdateInfectionDisplay()
    {
        infectionLabel.text = gameManager.GetInfection().ToString() + " / 100";
    }
}
