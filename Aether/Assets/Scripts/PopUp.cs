using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PopUp : MonoBehaviour
{
    public TMP_Text HintText;
    public TMP_Text HintTitle;
	public string VideoURL;

    // public Button SkipOneButton;
    // public Button SkipAllButton;
    
    public GameObject Layer;
    public GameObject pauseButton = null;
	
	public VideoPlayer vidplayer;

    public PlayerMovement pm;

    private static List<(int Z_Value, PopUpEnums popUpEnums)> tutorialCoordinates;
    private static int highestLevel = 0;

    public static void setTutorialCoordinates(int level)
    {
        if (level <= highestLevel)
        {
            return;
        }

        highestLevel = level;
        
        switch(level) {
			case 1: tutorialCoordinates = FindObjectOfType<GroundSpawner>().tutorialCoordinates;
				break;
			case 2: tutorialCoordinates = FindObjectOfType<GroundSpawner>().tutorialCoordinates2;
				break;
			case 3: tutorialCoordinates = FindObjectOfType<GroundSpawner>().tutorialCoordinates3;
				break;
		}
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initially Hide the PopUp for Hint
        HideLayer();

        // Get the Player Movement Object
        pm = FindObjectOfType<PlayerMovement>();

		// Initialize Video Player
    	// vidplayer = GetComponent<VideoPlayer>();
    }

    // Hide the PopUp Dialog Box
    void HideLayer()
    {
        Layer.SetActive(false);
        pauseButton.SetActive(true);
    }
    
    // Show the PopUp Dialog Box
    void ShowLayer()
    {
        Layer.SetActive(true);
        pauseButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int location = pm.getCurrentPosition();
        if (tutorialCoordinates.Count > 0 && location >= tutorialCoordinates[0].Z_Value)
        {
            pm.FreezeGame();
            SetDialogAttributes(tutorialCoordinates[0].popUpEnums);
            ShowLayer();
            tutorialCoordinates.Remove(tutorialCoordinates[0]);
        }
    }

    public void SetDialogAttributes(PopUpEnums em)
    {
        switch (em)
        {
            // Controls 
            case PopUpEnums.CONTROLS:
                HintTitle.text = "GAME CONTROLS";
                HintText.text = "RIGHT ARROW KEY - Move Right\n\nLEFT ARROW KEY - Move Left\n\nSPACEBAR - Jump";
                VideoURL = "https://nkhl-p.github.io/aether-vid/nahi.mp4";
				break;
            
            // Paths
            case PopUpEnums.BLUEPATH:
                HintTitle.text = "PATH - SPEED RELATIONSHIP";
                HintText.text = "BLUE PATH - BASE SPEED\n\nGREEN PATH - SPEED INCREASES\n\nYELLOW PATH - SPEED DECREASES\n\nRED PATH - BASE SPEED & HEALTH LOSS";
                VideoURL = "https://nkhl-p.github.io/aether-vid/thats_what_she_said.mp4";
				break;
            
            case PopUpEnums.GREENPATH:
                HintText.text = "GREEN PATH";
                HintTitle.text = "<GREENPATH Title Text>";
                VideoURL = "";
				break;
            
            case PopUpEnums.YELLOWPATH:
                HintText.text = "YELLOW PATH";
                HintTitle.text = "<YELLOWPATH Title Text>";
                VideoURL = "";
				break;
            
            case PopUpEnums.REDPATH:
                HintText.text = "RED PATH";
                HintTitle.text = "<REDPATH Title Text>";
                VideoURL = "";
				break;

            // Power Ups
            case PopUpEnums.TIME:
                HintTitle.text = "TIME POWERUP";
                HintText.text = "PICKUP TO ADD +5s\n\nTO YOUR TIMER";
                VideoURL = "";
				break;
            
            case PopUpEnums.SIZE:
                HintTitle.text = "SIZE POWERUP";
                HintText.text = "PICK UP TO INCREASE\n\nTHE SIZE OF THE PLAYER\n\nPLAYER CAN GO THROUGH OBSTACLES\n\nWHEN THIS IS PICKED UP";
                VideoURL = "";
				break;
            
            case PopUpEnums.SPEED:
                HintTitle.text = "SPEED POWERUP";
                HintText.text = "PICK UP TO INCREASE\n\nTHE SPEED OF THE PLAYER";
                VideoURL = "";
				break;

            case PopUpEnums.SHOOT:
                HintTitle.text = "SHOOT POWERUP";
                HintText.text = "PICK UP TO GET ENABLE\n\nSHOOTING FUCNTIONALITY\n\n\nPRESS F TO SHOOT";
                VideoURL = "https://nkhl-p.github.io/aether-vid/nahi.mp4";
				break;
            
            case PopUpEnums.PERMEATE:
                HintTitle.text = "IMMUNITY POWERUP";
                HintText.text = "PICK UP TO BECOME\n\nIMMUNE TO OBSTACLES & RED PATH";
                VideoURL = "";
				break;

            case PopUpEnums.LEVITATE:
                HintTitle.text = "LEVITATE POWERUP";
                HintText.text = "PICK UP TO\n\nLEVITATE OVER THE PATHS";
                VideoURL = "";
				break;

            case PopUpEnums.WORMHOME:
                HintTitle.text = "WORMHOME POWERUP";
                HintText.text = "PICK UP TO\n\nTELEPORT TO A FURTHER LOCATION";
                VideoURL = "";
				break;

            default:
                HintText.text = "<Default Hint Text>";
                HintTitle.text = "<Default Title Text>";
                VideoURL = "";
				break;
        }
		
		if (VideoURL != "") {
			vidplayer.url = VideoURL;
			vidplayer.Play();
       		vidplayer.isLooping = true;
		}
    }

    public void NextButtonClicked()
    {
        HideLayer();
        pm.UnFreezeGame();
    }
    
    public void SkipAllButtonClicked()
    {
        if (tutorialCoordinates.Count > 0)
        {
            tutorialCoordinates.RemoveAll(r => true);    
        }
        HideLayer();
        pm.UnFreezeGame();
    }
}
