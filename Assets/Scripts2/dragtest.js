#pragma strict

var pointDistance = 3.0;
var pullSpeed = 8.0;
var throwForce = 40.0;
 
private var mGrabbed : boolean = false;
private var heldObject : Transform;
 
function Update()
{
    if(mGrabbed){
        var targetPoint = Camera.main.transform.TransformPoint(Vector3.forward * pointDistance);
       
        heldObject.position=Vector3.Lerp(heldObject.position, targetPoint, Time.deltaTime * pullSpeed);
    }
    if(Input.GetButtonDown("Fire2")){
        if(!mGrabbed){
            var ray : Ray = GetComponent.<Camera>().ViewportPointToRay (Vector3(0.5,0.5,0));
            var hit : RaycastHit;
            if (Physics.Raycast (ray, hit)){
                print(hit.transform.name);
                if(hit.rigidbody){
                    heldObject = hit.transform;
                    heldObject.GetComponent.<Rigidbody>().useGravity=false;
                    mGrabbed = true;
                }
            }
        }else{
            heldObject.GetComponent.<Rigidbody>().velocity=Camera.main.transform.forward * throwForce;
            mGrabbed = false;
            heldObject.GetComponent.<Rigidbody>().useGravity=true;
        }
    }
}

