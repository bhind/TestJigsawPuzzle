using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour {

	private static float MARGIN_X = 0.1f;
	private static float MARGIN_Y = 0.1f;
	private Vector3 screenPoint;
	private Vector3 offset;
	private bool isFixed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		if( this.isFixed ) return;
		this.screenPoint = Camera.main.WorldToScreenPoint (this.transform.position);
		this.offset = this.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	void OnMouseDrag() {
		if( this.isFixed ) return;
		Vector3 currentScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint (currentScreenPoint) + this.offset;
		this.transform.position = currentPosition;
	}
	void OnMouseUp() {
		if (this.isRightPlace ()) {
			this.isFixed = true;
		}
	}
	private bool isRightPlace() {
		/// FIXME
		var _spriteRenderer = GetComponent<SpriteRenderer>();
		var _sprite = _spriteRenderer.sprite;
		var _pos =  _spriteRenderer.transform.TransformPoint(new Vector3 (-_sprite.bounds.extents.x, _sprite.bounds.extents.y, 0f));
		var ret = false;
		Debug.Log ("X: " + _pos.x);
		Debug.Log ("Y: " + _pos.y);
		Debug.Log ("name: " + _sprite.name);

		if (_sprite.name == "peace1_1") {
			if (_pos.x > -4.00353f - DragObject.MARGIN_X && _pos.x < -4.00353f + DragObject.MARGIN_X &&
				_pos.y > 2.993599f - DragObject.MARGIN_Y && _pos.y < 2.993599f + DragObject.MARGIN_Y ) {
				this.transform.position = new Vector3 (this.transform.position.x - (_pos.x - (-4.00353f)),
													   this.transform.position.y - (_pos.y - 2.993599f),
													   screenPoint.z);
				Debug.Log ("HIT: " + _sprite.name);
				ret = true;
			}
		}
		return ret;
	}
}
