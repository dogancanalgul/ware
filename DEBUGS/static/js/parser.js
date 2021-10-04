let fs = require('fs');

module.exports = {
    init: function(){
        this.startOfHTML = ""
        this.endOfHTML = ""
        this.count = 0
        this.initDynamics()
    },

    initDynamics: function (){
        fs.readFile("./templates/startOfHTML", (error, content) => {
            if(error){ console.log("startOfHTML Reading: " + error);
                return ;
            }
            this.startOfHTML = content.toString();
        });

        fs.readFile("./templates/endOfHTML", (error, content) => {
            if(error){ console.log("endOfHTML Reading: " + error)
                return ;
            }
            this.endOfHTML = content.toString()});
        fs.readdir("./static", (err, files) => {
            if(err){ console.log("Error while deleting: " + err)
                return ;
            }
            files.forEach(file => {
                if(file.substring(0, 4) == "temp")//search if temp
                    fs.unlink("./static/" + file, () => {console.log( file + " is erased." )})
            });
        });
    },

    createDynamically: function(parselanguage){
        tempFile = "temp" + this.count + ".html";
        this.count += 1

        dynamicCode = this.createDynamicCode(parselanguage.entities)

        attributes = ""
        if(parselanguage.global_gesture)
            attributes = "gesture-handler"
        if(dynamicCode.dDyn.length != 0)
            dynamicCode.dDyn = this.wrapMarker("d", this.wrapEntityScaler(attributes, dynamicCode.dDyn, 0.1))
        if(dynamicCode.hiroDyn.length != 0)
            dynamicCode.hiroDyn = this.wrapMarker("hiro", this.wrapEntityScaler(attributes, dynamicCode.hiroDyn, 0.1))

        dynamicCode = dynamicCode.hiroDyn + dynamicCode.dDyn
        fs.writeFile( "./static/" + tempFile, this.startOfHTML + dynamicCode + this.endOfHTML,
            (err) => {if(err) console.log("Couldn't write temp File :" + err)});

        return tempFile
    },

    createDynamicCode: function(entities){
        dynamicCode = {hiroDyn: "", dDyn: ""}
        entities.forEach(element => {
            if(element.marker == 'd')
                dynamicCode.dDyn += this.createEntity(element)
            if(element.marker == 'hiro')
                dynamicCode.hiroDyn += this.createEntity(element)
        });
        return dynamicCode;
    },

    createEntity: function(element){
        attributes = ""
        if(element.animation.rotation)
            attributes = ' rotate="rotationAxis:' + this.vec3tostr(element.animation.rotationAxis) + '" '
        attributes += this.swipeAnimations(element.swipe);
        //if(element.animation["swipe"])
        switch (element.type) {
            case "box":
                return this.giveCube(attributes, element);
            case "sphere":
                return this.giveSphere(attributes, element);
            case "naruto":
                return this.giveNaruto(attributes, element);
        }
    },

    swipeAnimations: function(swipe){
            element = "";
            directions = ["up", 'down', 'left', 'right'];
            for (let i = 0; i < swipe.actions.length; i++) {
                if(swipe.actions[i] != 'none'){
                        element += ' swipe-to-action__' + directions[i]
                        + '="direction:' + directions[i] + ';action:' + swipe.actions[i] + ';" ';
                }
            }
            return element;

    },


    giveCube: function (attributes, cube){
        return '\n<a-box ' + attributes + ' position="'+ this.vec3tostr(cube.position) +'" color="#' + cube.color
        + '" scale="' + this.vec3tostr(cube.scale) + '" rotation="' + this.vec3tostr(cube.rotation) + '" ></a-box>\n';
    },
    giveSphere: function (attributes, sphere){
        return '\n<a-sphere ' + attributes + ' radius="0.5" position="'+ this.vec3tostr(sphere.position) +'" color="#' + sphere.color
        + '" scale="' + this.vec3tostr(sphere.scale) + '" rotation="' + this.vec3tostr(sphere.rotation) + '" ></a-sphere>\n';
    },
    giveNaruto: function (attributes, anaruto){
        return '\n<a-entity ' + attributes + ' \ngltf-model="models/naruto_rigged/scene.gltf" position="'
        + (anaruto.position.x )  + " " + (anaruto.position.y)+ " " + (anaruto.position.z + 0.5)
        + '" scale="' + (anaruto.scale.x * 0.005)  + " " + (anaruto.scale.y * 0.005) + " " + (anaruto.scale.z * 0.005)
        +'" rotation="' +  (anaruto.rotation.x )  + " " + (anaruto.rotation.y - 180)+ " " + anaruto.rotation.z + '" >\n</a-entity>\n';
    },

    vec3tostr: function (pos){
        return pos.x + " " + pos.y + " " + pos.z;
    },

    giveDinosaur: function (){
        return '\n<a-entity\n\tposition="0 0 0"\n\tscale="0.05 0.05 0.05"\n\trotation="-135 0 0"\n\tgltf-model="https://arjs-cors-proxy.herokuapp.com/https://raw.githack.com/AR-js-org/AR.js/master/aframe/examples/image-tracking/nft/trex/scene.gltf">\n\t</a-entity>\n\t</a-marker>\n'
    },
    wrapEntityScaler: function (attributes, wrap, scale){
        return '\n<a-entity ' + attributes + ' position="0 0 0" rotation="225 0 0" scale="' + scale + " " + scale + " " + scale + '">'+ wrap + '\n</a-entity>\n'
    },
    wrapMarker: function (marker, str){
        return '\n<a-marker preset="custom" type="pattern" url="'+ marker + '.patt">\n' + str + "\n</a-marker>\n"
    }
}