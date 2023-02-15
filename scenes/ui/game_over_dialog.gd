extends Control

onready var _retry_btn: Button = $VBoxContainer/RetryBtn

func _ready():
	visible = false
	SignalMgr.register_subscriber(self, "GameOver")



func _on_GameOver() -> void:
	visible = true
	get_tree().paused = true
	_retry_btn.grab_focus()


func _on_RetryBtn_pressed():
	TransitionMgr.reload_current_scene()
