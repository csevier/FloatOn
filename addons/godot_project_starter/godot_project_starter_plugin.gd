tool
extends EditorPlugin

const PROJ_SETTING_TRANSITION_MGR_DEF_SPEED = "global/transition_mgr_default_speed"


const AUTOLOADS = {
	"TransitionMgr": "res://addons/godot_project_starter/utils/transition_mgr.tscn",
	"ScreenshotMgr": "res://addons/godot_project_starter/utils/screenshot_mgr.tscn",
	"InputMapMgr": "res://addons/godot_project_starter/utils/input_map/input_map_mgr.gd",
	"VolumeSettingsMgr": "res://addons/godot_project_starter/utils/volume_settings_mgr.gd",
}


func _input_event_key(scancode: int, physical: bool = true):
	var e = InputEventKey.new()
	if physical:
		e.physical_scancode = scancode
	else:
		e.scancode = scancode
	return e


func _input_event_joy_button(button_index):
	var e = InputEventJoypadButton.new()
	e.button_index = button_index
	return e


var ACTIONS = {
	"toggle_screenshots": [_input_event_key(KEY_F12, false)],
	"pause": [_input_event_key(KEY_ESCAPE, false), _input_event_joy_button(JOY_BUTTON_11)],
	"game_over": [],
	"next_settings_tab": [_input_event_joy_button(JOY_BUTTON_5)],
	"previous_settings_tab": [_input_event_joy_button(JOY_BUTTON_4)],
	"move_up": [_input_event_key(KEY_W), _input_event_key(KEY_UP, false)],
	"move_down": [_input_event_key(KEY_S), _input_event_key(KEY_DOWN, false)],
	"move_left": [_input_event_key(KEY_A), _input_event_key(KEY_LEFT, false)],
	"move_right": [_input_event_key(KEY_D), _input_event_key(KEY_RIGHT, false)],
}


func _enter_tree():
	_add_remove_autoloads(true)
	_add_input_map_entries()
	
	if !ProjectSettings.has_setting(PROJ_SETTING_TRANSITION_MGR_DEF_SPEED):
		ProjectSettings.set_setting(PROJ_SETTING_TRANSITION_MGR_DEF_SPEED, .5)
	


func _exit_tree():
	_add_remove_autoloads(false)


func _get_autoloads() -> Array:
	var autoloads = []
	for p in ProjectSettings.get_property_list():
		var s: String = p.name
		if s.begins_with("autoload/"):
			autoloads.append(s.replace("autoload/", ""))
	return autoloads


func _add_remove_autoloads(add: bool) -> void:
	var current_autoloads = _get_autoloads()
	for autoload_name in AUTOLOADS.keys():
		if add:
			if !current_autoloads.has(autoload_name):
				add_autoload_singleton(autoload_name, AUTOLOADS[autoload_name])
		elif current_autoloads.has(autoload_name):
			remove_autoload_singleton(autoload_name)


func _get_action_dictionary(action_name) -> Dictionary:
	var d := {"deadzone": .5, "events" : []}
	var events: Array = d["events"]
	for e in ACTIONS[action_name]:
		events.append(e)
	return d


func _add_input_map_entries() -> void:
	var existing_actions = InputMap.get_actions()
	
	var action_added := false

	
	for action_name in ACTIONS.keys():
		var settings_key:String = "input/%s" % action_name
		if ProjectSettings.has_setting(settings_key):
			continue
		ProjectSettings.set_setting(settings_key, _get_action_dictionary(action_name))
		action_added = true

	if action_added:
		print("Input map entries added by Godot Project Starter plugin.  Please re-load project to see these entries.")

