# Ecommerce
A Simple Ecommerce Application.


# Microservice Architecture

![alt text](https://github.com/mo2274/Ecommerce/blob/master/Architecture-Diagram.PNG?raw=true)


# Used Design Pattern:
API Gateway.

  API Gateway acts as the entry point for all the microservices and creates fine-grained APIs’ for different types of clients.
  With the help of the API Gateway design pattern, the API gateways can convert the protocol request from one type to other. Similarly, it can also offload the             authentication/authorization responsibility of the microservice.
  
Asynchronous Messaging.
   
   In synchronous messaging the client gets blocked or has to wait for a long time, But, if you do not want the consumer, to wait for a long time, then you can opt for the Asynchronous Messaging. In this type of microservices design pattern, all the services can communicate with each other, but they do not have to communicate with each other sequentially. and we can achieve that using message queueing system like RabbitMQ.
   
  To run RabbitMQ Locally using docker:
    1. docker pull rabbitmq:3-management.
    2. docker run --rm -it -p 15672:15672 -p 5672:5672 rabbitmq:3-management.
