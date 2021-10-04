let https = require('https');
let parser = require('./static/js/parser')
let fs = require('fs');
const express = require('express');
const options = {
    key: fs.readFileSync('key.pem'),
    cert: fs.readFileSync('cert.pem')
  };
var app = express();
app.use(express.static('static'))
app.use(express.static('./'))
app.get("/read", (req, res) => {
    console.log("MESSAGE: " + req.headers.msg)
    res.writeHead(200, { 'Content-Type': 'text/html' });
    res.end("received" , 'utf-8');
}, () => {console.log("")})

app.get("/create", (req, res) => {
    console.log(req.headers.parselanguage)
    //for WebGL
    res.header("Access-Control-Allow-Credentials", "true")
    res.header("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time")
    res.header("Access-Control-Allow-Methods", "GET, POST, OPTIONS")
    res.header("Access-Control-Allow-Origin", "*")
    res.writeHead(200, { 'Content-Type': 'text/html' });
    res.end("https://localhost:8083/" + parser.createDynamically(JSON.parse(req.headers.parselanguage))    , 'utf-8');
}, () => {console.log("/create get registered")})



console.log("Init parser...")
parser.init()


console.log("Initiate the Server...")
httpsServer = https.createServer(options, app);
httpsServer.listen(8083);
console.log("https://localhost:8083\n")



