using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cartographer
{
    public class Shape
    {
        public List<Vector2> points;
        public Vector2 center;

        public Shape() 
        {
            points = new List<Vector2>();
            center = new Vector2();
        }
    }
}
