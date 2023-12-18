extends Node


signal connected
signal disconnected
signal message_received(String)


const CREATE_ROOM_COMMAND = ":ROOM"
const JOIN_ROOM_COMMAND = ":JOIN"
const LEAVE_ROOM_COMMAND = ":LEAV"

const MAX_RESPONSE_WAIT_TIME_MS = 3000


var tcp: StreamPeerTCP = StreamPeerTCP.new()
var udp: PacketPeerUDP = PacketPeerUDP.new()

var client_id: String = ""
var room_id: String = ""

var is_client_connected: bool:
	get:
		return !client_id.is_empty()


func _ready():
	set_process.call_deferred(false)


func connect_to_server(addr: String) -> Error:
	var connecting_start_time = Time.get_ticks_msec()
	var is_connection_timeout = func() -> bool:
		return Time.get_ticks_msec() - connecting_start_time \
				> MAX_RESPONSE_WAIT_TIME_MS
	
	tcp.poll()
	if tcp.get_status() == tcp.STATUS_CONNECTED:
		return OK
	
	disconnect_from_server()
	var host = addr.get_slice(":", 0)
	var port = addr.get_slice(":", 1).to_int()
	var error = tcp.connect_to_host(host, port)
	if error != OK:
		return error
	
	while tcp.get_status() == tcp.STATUS_CONNECTING:
		if is_connection_timeout.call():
			return ERR_TIMEOUT
		error = tcp.poll()
		if error != OK:
			return error
	
	if tcp.get_status() != tcp.STATUS_CONNECTED:
		return ERR_CONNECTION_ERROR
	
	while client_id.is_empty():
		if is_connection_timeout.call():
			return ERR_TIMEOUT
		var response = _get_string_from_tcp()
		if response.begins_with("ERR"):
			return ERR_CONNECTION_ERROR
		client_id = response
	
	error = udp.connect_to_host(host, port)
	if error != OK:
		return error
	
	emit_signal.call_deferred("connected")
	set_process.call_deferred(true)
	return OK


func disconnect_from_server():
	tcp.disconnect_from_host()
	udp.close()
	client_id = ""
	room_id = ""
	emit_signal.call_deferred("disconnected")
	set_process.call_deferred(false)


func create_room() -> Error:
	if not room_id.is_empty():
		push_warning("Room not created: Client already in room.")
		return ERR_CANT_CREATE
	var error = send_tcp(CREATE_ROOM_COMMAND)
	if error != OK:
		return error
	
	var response = ""
	while response.is_empty():
		response = _get_string_from_tcp()
		if response.begins_with("ERR"):
			push_warning("Creting room error: ", response)
			return ERR_QUERY_FAILED
	
	room_id = response
	return OK


func join_room(id: String) -> Error:
	if not room_id.is_empty():
		push_warning("Joining room failed: Client already in room.")
		return FAILED
	var message = "%s %s" % [JOIN_ROOM_COMMAND, id]
	var error = send_tcp(message)
	if error != OK:
		return error
	
	var response = ""
	while response.is_empty():
		response = _get_string_from_tcp()
		if response.begins_with("ERR"):
			push_warning("Joining room error: ", response)
			return ERR_QUERY_FAILED
	
	room_id = id
	return OK


func leave_room() -> Error:
	var error = send_tcp(LEAVE_ROOM_COMMAND)
	if error == OK:
		room_id = ""
	
	return error


func send_tcp(message: String) -> Error:
	message += '\n'
	return tcp.put_data(message.to_utf8_buffer())


func send_udp(message: String) -> Error:
	if client_id.is_empty():
		return ERR_UNCONFIGURED
	
	message = "%s: %s\n" % [client_id, message]
	return udp.put_packet(message.to_utf8_buffer())


func _process(_delta):
	_listen_tcp()
	_listen_udp()


func _listen_tcp():
	tcp.poll()
	var status = tcp.get_status()
	if status != tcp.STATUS_CONNECTED:
		emit_signal("disconnected")
		return

	var recieved = _get_string_from_tcp()
	if recieved.is_empty():
		return
		
	for message in recieved.split("\n", false, 0):
		message_received.emit(message)


func _listen_udp():
	if not udp.is_socket_connected():
		return
	
	if udp.get_available_packet_count() <= 0:
		return
	
	var message = udp.get_packet().get_string_from_utf8().strip_edges()
	message_received.emit(message)


func _get_string_from_tcp() -> String:
	var bytes = tcp.get_available_bytes()
	if bytes <= 0:
		return ""
		
	var data = tcp.get_partial_data(bytes)
	var error = data[0]
	if error != OK:
		push_warning("Error: Reading from TCP: ", error)
		return ""
	
	var string = data[1].get_string_from_utf8().strip_edges()
	return string
