from time import sleep
import zmq

context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:55")

while(1):
  message = socket.recv()
  print("Received:",message)
  socket.send_string("World")
input()
