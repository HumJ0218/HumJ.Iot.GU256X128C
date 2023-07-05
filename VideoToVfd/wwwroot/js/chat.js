"use strict";

var width = 256;
var height = 128;
var buffer = new Array(width * height / 8);

var connection = new signalR.HubConnectionBuilder().withUrl("/vfdhub").build();
var video = document.querySelector('video');
var context = document.querySelector('canvas').getContext('2d');

document.querySelector('input').value = video.src;
document.querySelector('input').addEventListener('change', ev => {
    video.src = ev.target.value;
});

connection.start().then(function () {
    video.play();
    anime();
}).catch(function (err) {
    return console.error(err.toString());
});

function anime() {
    requestAnimationFrame(() => {
        // context.drawImage(video, 44, 1, 168, 126);
        context.drawImage(video, 0, 0, width, height);
        var imageData = context.getImageData(0, 0, width, height).data;

        for (var y = 0; y < height; y += 8) {
            for (var x = 0; x < width; x++) {

                var d = 0;

                for (var i = 0; i < 8; i++) {
                    var X = x;
                    var Y = y + (7 - i);
                    var dataOffset = (Y * width + X) * 4;
                    var r = imageData[dataOffset + 0];
                    var g = imageData[dataOffset + 1];
                    var b = imageData[dataOffset + 2];
                    var a = imageData[dataOffset + 3];

                    var brightness = (r + g + b) / (256 * 3);
                    var value = grayscale(brightness);
                    d |= (value << i);
                }

                var bufferOffset = (x * height + y) / 8;
                buffer[bufferOffset] = d;

            }
        }

        var message = buffer.map(m => m.toString(16).padStart(2, '0')).join('');
        connection.invoke('SendMessage', message).catch(function (err) {
            return console.error(err.toString());
        });

        anime();
    });
}

function grayscale(b) {
    b = b * 1.2 - 0.1;
    var v = Math.random() < Math.pow(b, Math.E) ? 1 : 0;
    return v;
}