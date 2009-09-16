using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cartographer
{
    public class Dms
    {
        public int m_degrees;
        public int m_minutes;
        public double m_seconds;

        public Dms()
        {

        }

        public Dms(double a_coord)
        {
            double _coord;
            _coord = Math.Abs(a_coord);
            this.m_degrees = System.Convert.ToInt32(Math.Truncate(_coord));
            double _leftover = _coord-m_degrees;
            this.m_minutes = System.Convert.ToInt32(Math.Truncate((_coord - m_degrees) * 60));
            this.m_seconds = System.Convert.ToDouble((((_coord - m_degrees) * 60) - Math.Truncate((_coord - m_degrees) * 60)) * 60);
        }
    }


}
