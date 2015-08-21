using UnityEngine;
using System.Collections;
using AAR.Types;

namespace AAR
{
	public class AARAutoAspectRatio : MonoBehaviour
	{
		public Camera cameraObject;
		internal float  cameraInitialSize;

		public bool orthographicCamera;
		public bool livePreview;

		public Transform backgroundObject;
		internal Vector3 backgroundInitialScale;

		public Transform[] topUI;
		public Transform[] bottomUI;

		internal float[] topUIInitialPosition;
		internal float[] bottomUIInitialPosition;

		public CustomAspect[] customAspects;

		internal float currentAspect;

		private int index;

		/// <summary>
		/// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
		/// </summary>
		public void Start()
		{
			if ( cameraObject == null )    cameraObject = Camera.main;

			currentAspect = cameraObject.aspect;
			
			if ( orthographicCamera == true )
				cameraInitialSize = cameraObject.orthographicSize;
			else
				cameraInitialSize = cameraObject.transform.position.z;
			
			if ( backgroundObject )    
				backgroundInitialScale = backgroundObject.localScale;
			
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

		/// <summary>
		/// Update is called every frame, if this instance of MonoBehaviour is enabled.
		/// </summary>
		public void Update()
		{
			if ( cameraObject.aspect != currentAspect || livePreview == true )
			{
				currentAspect = cameraObject.aspect;
				
				UpdateUI();
			}
		}

		/// <summary>
		/// Updates the current camera to the current aspect ratio.
		/// </summary>
		public void UpdateUI()
		{
			float currentAspect = Mathf.Round(cameraObject.aspect * 100f)/100f;
			
			foreach ( CustomAspect aspect in customAspects )
			{
				float calculatedAspect = Mathf.Round((aspect.aspect.x/aspect.aspect.y) * 100f)/100f;
				
				if ( aspect.aspect.x/aspect.aspect.y == 0.625f )
					calculatedAspect = 0.63f;
				
				if ( currentAspect == calculatedAspect )
				{
					// Check if perspective or orthographic camera
					if ( orthographicCamera == true )
					{
						cameraObject.orthographicSize = cameraInitialSize + aspect.cameraSizeChange;
					}
					else
					{
						Vector3 cameraPositionCopy = cameraObject.transform.position; // Make a copy of the vector3 struct for modifications.
						cameraPositionCopy.z = cameraInitialSize + aspect.cameraSizeChange; // Modify the z value of vector3.

						cameraObject.transform.position = cameraPositionCopy; // Assign the modified copy back to the position.
					}
					
					if ( backgroundObject )
					{
						Vector3 backgroundObjectLocalScaleCopy = backgroundObject.localScale; // Copy localScale vector3 for modifications

						backgroundObjectLocalScaleCopy.x = backgroundInitialScale.x + aspect.backgroundScaleChange.x;
						backgroundObjectLocalScaleCopy.y = backgroundInitialScale.y + aspect.backgroundScaleChange.y;
						backgroundObjectLocalScaleCopy.z = backgroundInitialScale.z + aspect.backgroundScaleChange.z;

						backgroundObject.localScale = backgroundObjectLocalScaleCopy; // assign modified vector3 copy back to localScale.
					}
					
					// Go through all the top UI elements and place them based on their offset from the initial position of the element
					for ( index = 0 ; index < topUI.Length ; index++ )
					{
						if ( topUI[index] )    
						{
							Vector3 topUIPositionCopy = topUI[index].position; // copy the vector3 position field for modifications.

							if ( orthographicCamera == true )
								topUIPositionCopy.y = topUIInitialPosition[index] + aspect.cameraSizeChange;
							else
								topUIPositionCopy.y = topUIInitialPosition[index] + aspect.offsetUI;

							topUI[index].position = topUIPositionCopy; // assign modifications back to position.
						}
					}
					
					// Go through all the bottom UI elements and place them based on their offset from the initial position of the element
					for ( index = 0 ; index < bottomUI.Length ; index++ )
					{
						if ( bottomUI[index] )    
						{
							Vector3 bottomUIPositionCopy = bottomUI[index].position;

							if ( orthographicCamera == true )
								bottomUIPositionCopy.y = bottomUIInitialPosition[index] - aspect.cameraSizeChange;
							else
								bottomUIPositionCopy.y = bottomUIInitialPosition[index] - aspect.offsetUI;

							bottomUI[index].position = bottomUIPositionCopy;
						}
					}
				}
			}
		}
	}
}