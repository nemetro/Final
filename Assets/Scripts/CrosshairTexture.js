var CrosshairTexture : Texture2D;
var position : Rect;
static var OriginalOn = true;

function Start(){
	position = Rect((Screen.width - CrosshairTexture.width) / 2, (Screen.height - CrosshairTexture.height)/2, CrosshairTexture.width, CrosshairTexture.height);
}

function OnGUI()
{
	if(OriginalOn == true){
		GUI.DrawTexture(position, CrosshairTexture);
	}
}