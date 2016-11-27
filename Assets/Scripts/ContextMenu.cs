using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ContextMenu : MonoBehaviour {

    public Sprite icon;
    public Transform spawnPoint;
    public GameObject thingToSpawn;
    public DisplayManager displayManager;

    private ModalPanel modalPanel;

    void Awake() {
        modalPanel = ModalPanel.Instance();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f)) {
                Debug.Log("You have clicked" + hit.transform.name);
            }

            TestC();
        }
	}

    //  Send to the Modal Panel to set up the Buttons and functions to call
    public void TestC() {
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails();
        modalPanelDetails.panelLocation = Input.mousePosition;
        modalPanelDetails.question = "This is an announcement!\nIf you don't like it, shove off!";
        modalPanelDetails.button1Details = new EventButtonDetails();
        modalPanelDetails.button1Details.buttonTitle = "Examine";
        modalPanelDetails.button1Details.action = TestExamineFunction;

        modalPanelDetails.button2Details = new EventButtonDetails();
        modalPanelDetails.button2Details.buttonTitle = "Cancel";
        modalPanelDetails.button2Details.action = TestCancelFunction;

        modalPanel.NewChoice(modalPanelDetails);
    }

    void TestExamineFunction() {
        displayManager.DisplayMessage("Examining!");
    }

    void TestCancelFunction() {
        displayManager.DisplayMessage("Menu Closed");
    }
}
