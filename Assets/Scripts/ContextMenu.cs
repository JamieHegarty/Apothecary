using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ContextMenu : MonoBehaviour {

    public Sprite icon;
    public Transform spawnPoint;
    public GameObject thingToSpawn;
    public DisplayManager displayManager;

    public GameObject objectClicked;
    public Vector3 walkLocation;

    public ModalPanel modalPanel;
    private PlayerInteraction playerInteraction;

    void Awake() {
        modalPanel = ModalPanel.Instance();
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    //  Send to the Modal Panel to set up the Buttons and functions to call
    public void TestC(string useTitle) {

        ModalPanelDetails modalPanelDetails = new ModalPanelDetails();
        modalPanelDetails.panelLocation = Input.mousePosition;
        modalPanelDetails.question = "This is an announcement!\nIf you don't like it, shove off!";

        modalPanelDetails.button1Details = new EventButtonDetails();
        modalPanelDetails.button1Details.buttonTitle = useTitle;
        if(!objectClicked) {
            modalPanelDetails.button1Details.action = TestWalkFunction;
        } else {
            modalPanelDetails.button1Details.action = TestUseFunction;
        }

        modalPanelDetails.button2Details = new EventButtonDetails();
        modalPanelDetails.button2Details.buttonTitle = "Examine";
        modalPanelDetails.button2Details.action = TestExamineFunction;

        modalPanelDetails.button3Details = new EventButtonDetails();
        modalPanelDetails.button3Details.buttonTitle = "Cancel";
        modalPanelDetails.button3Details.action = TestCancelFunction;

        modalPanel.NewChoice(modalPanelDetails);
    }

    void TestWalkFunction() {
        displayManager.DisplayMessage("Walking");
        playerInteraction.ClickedNonObject(walkLocation);
    }

    void TestUseFunction() {
        displayManager.DisplayMessage("USING!");
        playerInteraction.ClickedInteractableObject(objectClicked);
    }

    void TestExamineFunction() {
        displayManager.DisplayMessage("Examining!");
    }

    void TestCancelFunction() {
        displayManager.DisplayMessage("Menu Closed");
    }
}
