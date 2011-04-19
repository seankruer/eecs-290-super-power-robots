﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Physics.Collision.Shapes;
using Project290.Physics.Dynamics;

namespace Project290.Games.SuperPowerRobots.Entities
{
    class Projectile : Entity
    {
        private CircleShape shape;

        public Projectile(SPRWorld sprWorld, Body body)
            : base(sprWorld, body)
        {
        }
    }
}
