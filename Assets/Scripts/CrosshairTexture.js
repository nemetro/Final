var CrosshairTexture : Texture2D;
var position : Rect;
<<<<<<< HEAD
static var OriginalOn = true;

function Start(){
	position = Rect((Screen.width - CrosshairTexture.width) / 2, (Screen.height - CrosshairTexture.height)/2, CrosshairTexture.width, CrosshairTexture.height);
=======
var position1 : Rect;
var position2 : Rect;
static var OriginalOn = true;

function Start(){
	position = Rect((Screen.width - CrosshairTexture.width)/2, (Screen.height - CrosshairTexture.height)/2, CrosshairTexture.width, CrosshairTexture.height);
	position1 = Rect((Screen.width - CrosshairTexture.width)/4f, (Screen.height - CrosshairTexture.height)/2f, CrosshairTexture.width, CrosshairTexture.height);
	position2 = Rect((Screen.width - CrosshairTexture.width/2f)*3f/4f, (Screen.height - CrosshairTexture.height)/2f, CrosshairTexture.width, CrosshairTexture.height);
>>>>>>> master
}

function OnGUI()
{
	if(OriginalOn == true){
<<<<<<< HEAD
		GUI.DrawTexture(position, CrosshairTexture);
=======
		GUI.DrawTexture(position1, CrosshairTexture);
		GUI.DrawTexture(position2, CrosshairTexture);
>>>>>>> master
	}
}