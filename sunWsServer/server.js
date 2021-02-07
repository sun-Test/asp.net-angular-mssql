const express = require('express');
const http = require('http')
const socketio = require('socket.io');
const app = express();

const server = http.createServer(app);
const sktIo = socketio(server, {
  cors: {
    origins: ["http://localhost:4200"],
    methods: ["GET", "POST"]
  }
});

const PORT = 3000;
const UPDATE_TREE_ROOT = 'updatetree01Root';
const UPDATE_TREE_CHILDREN = 'updatetree01Children';
const TREE_ROOT = 'tree01 root';
const TREE_CHILDREN = 'tree01 children';
const CREATE_USER = 'create-user';
const WS_READY='wsReady';


const createUser = (data) => {
  console.log('socket-io create-user ', data);
  sendKafkaMsg(CREATE_USER, data);
}

var util  = require('util');
var Kafka = require('no-kafka');
var producer = new Kafka.Producer();
var DefaultPartitioner = Kafka.DefaultPartitioner;
var consumer = new Kafka.SimpleConsumer();
 
var newUserHandler = function (messageSet, topic, partition) {
    messageSet.forEach(function (m) {
        console.log(topic, partition, m.offset, m.message.value.toString('utf8'));
        sktIo.emit('new-user', m.message.value.toString('utf8'));
    });
};

var newVotingHandler = function (messageSet, topic, partition) {
  messageSet.forEach(function (m) {
      console.log(topic, partition, m.offset, m.message.value.toString('utf8'));
      sktIo.emit('new-voting', m.message.value.toString('utf8'));
  });
};

function sendKafkaMsg(topic, msg){
  producer.init().then(function(){
    return producer.send({
        topic: topic,
        message: {
            value: msg
        }
    });
  })
  .then(function (result) {
    /*
    [ { topic: 'kafka-test-topic', partition: 0, offset: 353 } ]
    */
  });
}

class MyWsServer{
  constructor(){
  }

  startApp() {
    sktIo.on('connection', (socket) => {
      console.log('new webSocket connection...');
      socket.emit('wsReady', 'hello from ws server');
      socket.on(CREATE_USER, createUser);
  });

  server.listen(PORT, () => console.log('WebSocket Server running on port ' + PORT));

  consumer.init().then(function () {
    // Subscribe partitons 0 - 4 in a topic:
    consumer.subscribe('new-voting', [0,1,2,3,4], newVotingHandler);
    return consumer.subscribe('new-user', [0,1,2,3,4], newUserHandler);
  });
  }
}

let myServer = new MyWsServer();
myServer.startApp();

