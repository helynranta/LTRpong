// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    //Own Variables begin

    var canvas, context, stage;
    var bgImage, p1Image, p2Image, title, endGameImage, ammoImage;
    var bgBitmap, p1Bitmap, p2Bitmap, ammoBitmap;
    var preload;
    var SHOTS = 3;
    var shotsLeft = SHOTS;
    var texti;

    var isShotFlying = false;
    var playerFire = false;
    var shotVelocity;

    var MAX_SHOT_POWER = 10;
    var GRAVITY = 0.07;

    //Own variables end

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize
                // your application here.
            } else {
                // TODO: This application has been reactivated from suspension.
                // Restore application state here.
            }
            args.setPromise(WinJS.UI.processAll());
        }
    };


    //Own functions Begin

    function initialize()
    {
        canvas = document.getElementById("gameCanvas");
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;
        context = canvas.getContext("2d");
        stage = new createjs.Stage(canvas);

        //Using PreloadJS to make sure images and sounds are loaded
        //before beginning to process
        preload = new createjs.LoadQueue(true);
        preload.onComplete = prepareGame;
        var manifest = [
            { id: "screenImage", src: "images/sonic.jpg" },
            { id: "ammoImage", src: "images/asciicat150.png" },
            { id: "player1", src: "images/cutecloud.png" }
        ];
        preload.loadManifest(manifest);
    }

    function prepareGame()
    {
        //Draw background first then others on top
        bgImage = preload.getResult("screenImage");
        bgBitmap = new createjs.Bitmap(bgImage);
        stage.addChild(bgBitmap);
        

        //draw Player
        p1Image = preload.getResult("player1");
        p1Bitmap = new createjs.Bitmap(p1Image);
        p1Bitmap.scaleX = 0.5;
        p1Bitmap.scaleY = 0.5;
        stage.addChild(p1Bitmap);

        //ammo
        ammoImage = preload.getResult("ammoImage");
        ammoBitmap = new createjs.Bitmap(ammoImage);
        ammoBitmap.visible = false; // hidden until further notice
        stage.addChild(ammoBitmap);

        //Shots left for player
        
        context.fillText("Shots left: " + shotsLeft, 300, 300);

        stage.update();
        context.fillText("Shots left: " + shotsLeft, 300, 300);
        startGame();
    }
    function startGame()
    {
        createjs.Ticker.setInterval(window.requestAnimationFrame);
        createjs.Ticker.addListener(gameLoop);
    }

    function gameLoop()
    {
        update();
        draw();
    }

    function update()
    {
        if (isShotFlying)
        {
            //shot in the air
            ammoBitmap.x += shotVelocity.x;
            ammoBitmap.y += shotVelocity.y;
            shotVelocity.y += GRAVITY; //apply gravity
        }

            //Should player fire?
        else
        {
            ammoBitmap.x = p1Bitmap.x + (p1Bitmap.image.width * 0.5);
            ammoBitmap.y = p1Bitmap.y;
            shotVelocity = new createjs.Point(
                Math.random() * (4 * 0.5) + 3,
                Math.random() * (-3 * 0.5) - 1);
            fireShot();
        }
    }

    function fireShot()
    {
        ammoBitmap.visible = true;
        isShotFlying = true;
    }

    function draw()
    {
        stage.update();
    }
    //Own functions END

    app.oncheckpoint = function (args) {
        // TODO: This application is about to be suspended. Save any state
        // that needs to persist across suspensions here. You might use the
        // WinJS.Application.sessionState object, which is automatically
        // saved and restored across suspension. If you need to complete an
        // asynchronous operation before your application is suspended, call
        // args.setPromise().
    };

    document.addEventListener("DOMContentLoaded", initialize, false);
    app.start();
})();
