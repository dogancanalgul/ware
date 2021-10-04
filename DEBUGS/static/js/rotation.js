/* global AFRAME, THREE */

AFRAME.registerComponent("rotate", {
    schema: {
      rotationAxis: { type:'vec3'}
    },
    multiple:true,
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
        console.log(this.data)
        this.el.sceneEl.addEventListener("markerFound", (e) => {
            this.isVisible = true;
          });

          this.el.sceneEl.addEventListener("markerLost", (e) => {
            this.isVisible = false;
          });
    },
    update: function(oldData){
    },

    tick: function (time, timeDelta) {
      timeDeltaS = timeDelta / 1000.0
        if(this.isVisible){
            this.el.object3D.rotation.x += THREE.Math.degToRad(timeDeltaS *this.data.rotationAxis.x)
            this.el.object3D.rotation.y += THREE.Math.degToRad(timeDeltaS *this.data.rotationAxis.y)
            this.el.object3D.rotation.z += THREE.Math.degToRad(timeDeltaS *this.data.rotationAxis.z)
        }

    },

    remove: function () {
    },
  });