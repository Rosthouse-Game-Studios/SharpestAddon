extends VehicleBody3D

@export var max_engine_force := 50.0

var _max_steering_angle := 45.0
@export var max_steering_angle: float:
  get:
    return rad_to_deg(max_steering_angle)
  set(value):
    _max_steering_angle = clamp(deg_to_rad(value), 0, PI)


func _physics_process(delta: float) -> void:
  var steering_input = Input.get_axis("ui_right", "ui_left")
  var power_input = Input.get_axis("ui_down", "ui_up")

  steering = move_toward(steering, _max_steering_angle * steering_input, 20 * delta)
  engine_force =  power_input * max_engine_force
