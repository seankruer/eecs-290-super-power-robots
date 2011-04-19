﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Screens;
using Microsoft.Xna.Framework;
using Project290.Rendering;
using Project290.GameElements;
using Project290.Inputs;
using Microsoft.Xna.Framework.Graphics;
using Project290.Clock;
using Project290.Physics.Dynamics;
using Project290.Physics.Collision.Shapes;
using Project290.Physics.Factories;
using Project290.Games.SuperPowerRobots.Entities;

namespace Project290.Games.SuperPowerRobots
{
    class SPRGameScreen : GameScreen
    {
        /// <summary>
        /// The message on the screen.
        /// </summary>
        private string display;

        /// <summary>
        /// Where to display display :p
        /// </summary>
        private Vector2 displayPosition;

        /// <summary>
        /// The origin of display
        /// </summary>
        private Vector2 displayOrigin;

        /// <summary>
        /// Where to draw the score.
        /// </summary>
        private Vector2 scorePosition;

        /// <summary>
        /// Where to draw the count-down timer.
        /// </summary>
        private Vector2 countDownTimerPosition;

        /// <summary>
        /// The time (in ticks since the start of the game) to end it.
        /// </summary>
        private long gameOverTime, previousGameTime;

        private SPRWorld sprWorld;
        
        // DEBUG!!
        Body wall_e;
        World fantastica;
        // </DEBUG!!>

        /// <summary>
        /// Initializes a new instance of the <see cref="StupidGameScreen"/> class.
        /// </summary>
        /// <param name="scoreboardIndex">The game-specific index into the scoreboard.</param>
        public SPRGameScreen(int scoreboardIndex)
            : base(scoreboardIndex)
        {
            // Tom's messing around with the physics engine!
            previousGameTime = GameClock.Now;
            fantastica = new World(Vector2.Zero);

            this.sprWorld = new SPRWorld(fantastica);

//            wall_e.CreateFixture(new CircleShape(20f, 1.2f));
            
            // Here should be the construction of all objects and the setting of objects that do not change.
            // The Reset method should be used to set objects that do change.
            this.display = "Going to change this little part";
            this.displayPosition = new Vector2(1920 / 2, 300); // Remember, screen resolution is guarenteed to be 1920*1080
            this.displayOrigin = FontStatic.Get("defaultFont").MeasureString(this.display) / 2f;
            this.scorePosition = new Vector2(1920 / 2, 500);
            this.countDownTimerPosition = new Vector2(1920 / 2, 700);

            // Always call reset at the end of the constructor!
            this.Reset();
        }

        /// <summary>
        /// Resets this instance. This will be called when a new game is played. Reset all of
        /// your objects here so that you do not need to reallocate the memory for them.
        /// </summary>
        internal override void Reset()
        {
            base.Reset();
            
            this.gameOverTime = GameClock.Now + 10000000 * 10; // 10 Seconds.
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public override void Update()
        {
            base.Update();

            // Check to see if it's game over.
            if (GameClock.Now > this.gameOverTime)
            {
                this.GameOver();
                return;
            }

            this.sprWorld.Update(GameClock.Now);

            fantastica.Step(GameClock.Now - previousGameTime);
            previousGameTime = GameClock.Now;
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public override void Draw()
        {
            base.Draw();

            this.sprWorld.Draw();

            string secondsRemaining = Math.Max((Math.Ceiling((this.gameOverTime - GameClock.Now) / 10000000f)), 0).ToString();
            Drawer.DrawOutlinedString(
                FontStatic.Get("defaultFont"),
                secondsRemaining,
                this.countDownTimerPosition,
                Color.White,
                0f,
                FontStatic.Get("defaultFont").MeasureString(secondsRemaining) / 2f,
                1f,
                SpriteEffects.None,
                1f);
        }
    }
}
