/* global AFRAME, THREE */

AFRAME.registerComponent("swipe-to-action", {
    schema: {
      enabled: { default: true },
      direction: { type:"string"},
      action: {type:"string"},
    },

    multiple: true,

    httpGet: function(text) {
        var xmlHttp = new XMLHttpRequest();
        console.log(text);
        if(navigator.userAgent.match("Win64"))
            console.log("Welcome pc")
        else{
            xmlHttp.open( "GET", "https://192.168.1.242:8083/read", false ); // false for synchronous request
            xmlHttp.setRequestHeader('msg', JSON.stringify(text));
            xmlHttp.send();
            return xmlHttp.responseText;
        }
      },
    init: function () {
    this.OnMovementStart = this.OnMovementStart.bind(this);
    this.OnMovementCnt = this.OnMovementCnt.bind(this);
    this.OnMovementEnd = this.OnMovementEnd.bind(this);
      this.isVisible = false;
      this.el.sceneEl.addEventListener("markerFound", (e) => {
      this.isVisible = true;
      });

      this.el.sceneEl.addEventListener("markerLost", (e) => {
        this.isVisible = false;
      });

      this.movement = {x: 0, y: 0};
      this.moveSpeed = 0.8;
    },


    update: function () {
      if (this.data.enabled) {
        this.el.sceneEl.addEventListener("onefingerstart", this.OnMovementStart);
        this.el.sceneEl.addEventListener("onefingermove", this.OnMovementCnt);
        this.el.sceneEl.addEventListener("onefingerend", this.OnMovementEnd);
      } else {
        this.el.sceneEl.removeEventListener("onefingerstart", this.OnMovementStart);
        this.el.sceneEl.removeEventListener("onefingermove", this.OnMovementCnt);
        this.el.sceneEl.removeEventListener("onefingerend", this.OnMovementEnd);
      }
    },

    remove: function () {
      this.el.sceneEl.removeEventListener("onefingerstart", this.OnMovementStart);
      this.el.sceneEl.removeEventListener("onefingermove", this.OnMovementCnt);
      this.el.sceneEl.removeEventListener("onefingerend", this.OnMovementEnd);
    },

    tick: function(){
    },

    swipeUp: function(){
      if(this.data.direction != "up")
        return;
        this.swipeAction({x: 0, y: this.moveSpeed})
    },

    swipeDown: function(){
      if(this.data.direction != "down")
        return;
        this.swipeAction({x: 0, y: -this.moveSpeed})
    },
    swipeLeft: function(){
      if(this.data.direction != "left")
        return;
        this.swipeAction({x: -this.moveSpeed, y: 0})
    },
    swipeRight: function(){
      if(this.data.direction != "right")
        return;
        this.swipeAction({x: this.moveSpeed, y: 0})
    },

    swipeAction: function(movePlace){
      if(this.data.action == 'destroy')
        this.el.remove();
      else if(this.data.action == 'show')
        this.el.object3D.visible = true;
      else if(this.data.action == 'hide')
        this.el.object3D.visible = false;
      else if(this.data.action == 'move'){
        this.el.object3D.position.x += movePlace.x;
        this.el.object3D.position.y += movePlace.y;
      }
    },

    OnMovementStart: function (event) {
      if(this.isVisible){
        this.movement.x = 0;
        this.movement.y = 0;
      }
    },
    OnMovementCnt: function (event) {
      if (this.isVisible) {
        this.movement.x += event.detail.positionChange.x;
        this.movement.y += event.detail.positionChange.y;
      }
  },
  OnMovementEnd: function (event) {
      if (this.isVisible) {
        if(this.movement.y < -0.1)
          this.swipeUp()
        else if(this.movement.y > 0.1)
          this.swipeDown();
        if(this.movement.x > 0.1)
          this.swipeRight();
        else if(this.movement.x < -0.1)
          this.swipeLeft();

    }
  },

  });