//This script refits the game area to fit multiple devices. It works by resizing the camera, moving top/bottom user interface elements, and rescalling the 
//background object based on the detected aspect ratio. You can set any number of aspect ratios or resolutions for your game, in addition to the ones defined 
//by default (1:1,3:2,4:3,5:3,5:4,16:9,16;10,128:75, and their portrait equivalents).
#pragma strict

//The camera object holder. If you leave this unassigned, the main camera in the scene will be assigned.
var cameraObject:Camera;

//The initial size ( or Z position if we are using an Orthographic camera ) value of the camera.
internal var cameraInitialSize:float;

//Are we using an orthographic camera? 
//If true, the variable cameraSizeChange will be assigned to the orthographic size of the camera.
//If false, the variable cameraSizeChange will be assigned to the Z position of the camera object.
var orthographicCamera:boolean = true;

//Should changes to the camera size and UI positions be updated during gameplay? 
//This is used only to preview any changes you want, and should be set to false before you build your game.
var livePreview:boolean = false;

//The background object which is rescaled based on detected aspect ratio
var backgroundObject:Transform;
internal var backgroundInitialScale:Vector3;

//A list of the top/bottom UI objects which will be moved up/down based on detected aspect ratio, and their initial positions
var topUI:Transform[];
var bottomUI:Transform[];
internal var topUIInitialPosition:float[];
internal var bottomUIInitialPosition:float[];

//A list of custom aspect ratios and the changes that need to be made to the game area when a certain aspect ratio is detected
var customAspects:CustomAspect[];

class CustomAspect
{
	//The name of this aspect ratio. Used just for easier display ( instead of having a list of Element0, Element1, etc )
	var name:String = "Default";
	
	//The aspect ratio we are targeting. This can work for set resolutions as well. ex: Vector2(1024,768)
	var aspect:Vector2 = Vector2(16,9);
	
	//The change in size ( or Z position if we aren't using an Orthographic camera ) of the camera
	var cameraSizeChange:float = 0;
	
	//The change in scale of the background object
	var backgroundScaleChange:Vector3 = Vector3(0,0,0);
	
	//The change in position of the UI elements. topUI objects are moved up, while bottomUI object are moved down
	var offsetUI:float = 0;
}

//The current aspect that was detected
internal var currentAspect:float;

//A general use index
private var index:int = 0;

function Start() 
{
	//If we haven't assigned a camera in the component, get it automatically from the main camrea in the scene
	if ( cameraObject == null )    cameraObject = Camera.main;
	
	//Set the current aspect ratio based on the aspect ratio of the camera we have
	currentAspect = cameraObject.aspect;

	//If we using an orthographic camera, set cameraSizeChange to the orthographic size of the camera. Otherwise, set cameraSizeChange to the Z position of the camera object.
	if ( orthographicCamera == true )    cameraInitialSize = cameraObject.orthographicSize;
	else    cameraInitialSize = cameraObject.transform.position.z;
	
	//If we have a background object, set its scale based on backgroundInitialScale
	if ( backgroundObject )    backgroundInitialScale = backgroundObject.localScale;
	
	//If we have user interface elements assigned to the top UI, record their initial position
	if ( topUI.Length > 0 )
	{
		//An array that holds all the initial positions of the UI elements
		topUIInitialPosition = new float[topUI.Length];
		
		for ( index = 0 ; index < topUI.Length ; index++ )
		{
			topUIInitialPosition[index] = topUI[index].position.y;
		}
	}
	
	//If we have user interface elements assigned to the bottom UI, record their initial position
	if ( bottomUI.Length > 0 )
	{
		//An array that holds all the initial positions of the UI elements
		bottomUIInitialPosition = new float[bottomUI.Length];
		
		for ( index = 0 ; index < bottomUI.Length ; index++ )
		{
			bottomUIInitialPosition[index] = bottomUI[index].position.y;
		}
	}
	
	//Set the UI changes
	UpdateUI();
}

function Update()
{
	//If we detect changes in the aspect ratio of the camera or we have livePreview set to true, update the UI
	if ( cameraObject.aspect != currentAspect || livePreview == true )
	{
		currentAspect = cameraObject.aspect;
		
		UpdateUI();
	}
}

function UpdateUI()
{
	//Calculate the current aspect ratio
	var currentAspect:float = Mathf.Round(cameraObject.aspect * 100f)/100f;
	
	//Go through all possible aspects and find the aspect ratio definition which fits currentAspect
	for ( var aspect in customAspects )
	{
		//Calculate the aspect ratio that is used for comparison
		var calculatedAspect:float = Mathf.Round((aspect.aspect.x/aspect.aspect.y) * 100f)/100f;
		
		//This is a special case for 10:16 aspect ratio
		if ( aspect.aspect.x/aspect.aspect.y == 0.625 )    calculatedAspect = 0.63;
		
		//If the current aspect equals our calculated aspect ratio, apply the changes to the game area
		if ( currentAspect == calculatedAspect )
		{
			//Check if perspective or orthographic camera
			if ( orthographicCamera == true )    cameraObject.orthographicSize = cameraInitialSize + aspect.cameraSizeChange;
			else    cameraObject.transform.position.z = cameraInitialSize + aspect.cameraSizeChange;
			
			//If we have a background, rescale it.
			if ( backgroundObject )
			{
				backgroundObject.localScale.x = backgroundInitialScale.x + aspect.backgroundScaleChange.x;
				backgroundObject.localScale.y = backgroundInitialScale.y + aspect.backgroundScaleChange.y;
				backgroundObject.localScale.z = backgroundInitialScale.z + aspect.backgroundScaleChange.z;
			}
			
			//Go through all the top UI elements and place them based on their offset from the initial position of the element
			for ( index = 0 ; index < topUI.Length ; index++ )
			{
				if ( topUI[index] )    
				{
					if ( orthographicCamera == true )    topUI[index].position.y = topUIInitialPosition[index] + aspect.cameraSizeChange;
					else   topUI[index].position.y = topUIInitialPosition[index] + aspect.offsetUI;
				}
			
			}
			
			//Go through all the bottom UI elements and place them based on their offset from the initial position of the element
			for ( index = 0 ; index < bottomUI.Length ; index++ )
			{
				if ( bottomUI[index] )    
				{
					if ( orthographicCamera == true )    bottomUI[index].position.y = bottomUIInitialPosition[index] - aspect.cameraSizeChange;
					else   bottomUI[index].position.y = bottomUIInitialPosition[index] - aspect.offsetUI;
				}
			}
		}
	}
}
