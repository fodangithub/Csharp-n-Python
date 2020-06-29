from concurrent import futures
import logging

import grpc

import addTwoNumbers_pb2
import addTwoNumbers_pb2_grpc

class TwoNumberAdder(addTwoNumbers_pb2_grpc.AddTwoNumberServiceServicer):
    def AddTwo(self, request, context):
        return addTwoNumbers_pb2.AddTwoNumberReply(resultVal = request.firstNum + request.secondNum)

logging.basicConfig()

server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
addTwoNumbers_pb2_grpc.add_AddTwoNumberServiceServicer_to_server(TwoNumberAdder(), server)
server.add_insecure_port('[::]:34567')
server.start()
print('Server started...')
server.wait_for_termination()
