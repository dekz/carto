using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cartographer
{
    public class Electorate
    {
        private int m_ID;
        private string m_name;
        private string m_division;
        private string m_state;
        private List<Shape> m_boundaries;
        private int m_actual;
        private int m_projected;
        private int m_totalPopulation;
        private int m_over18;
        private float m_area;
        private double m_ALPVotes;
        private double m_LPVotes;
        private double m_NPVotes;
        private double m_DEMVotes;
        private double m_GRNVotes;
        private double m_OTHVotes;
        private double m_LNP2PVotes;
        private double m_ALP2PVotes;

        public int ID { get { return m_ID; } set { m_ID = value; } }
        public string Name { get { return m_name; } set { m_name = value; } }
        public string Division { get { return m_division; } set { m_division = value; } }
        public string State { get { return m_state; } set { m_state = value;} }
        public List<Shape> Boundaries { get { return m_boundaries; } set { m_boundaries = value; } }
        public int Actual { get { return m_actual; } set { m_actual = value; } }
        public int Projected { get { return m_projected; } set { m_projected = value; } }
        public int TotalPopulation { get { return m_totalPopulation; } set { m_totalPopulation = value; } }
        public int Over18 { get { return m_over18; } set { m_over18 = value; } }
        public float Area { get { return m_area; } set { m_area = value; } }
        public double ALPVotes { get { return m_ALPVotes; } set { m_ALPVotes = value; } }
        public double LPVotes { get { return m_LPVotes; } set { m_LPVotes = value; } }
        public double NPVotes { get { return m_NPVotes; } set { m_NPVotes = value; } }
        public double DEMVotes { get { return m_DEMVotes; } set { m_DEMVotes = value; } }
        public double GRNVotes { get { return m_GRNVotes; } set { m_GRNVotes = value; } }
        public double OTHVotes { get { return m_OTHVotes; } set { m_OTHVotes = value; } }
        public double LNP2PVotes { get { return m_LNP2PVotes; } set { m_LNP2PVotes = value; } }
        public double ALP2PVotes { get { return m_ALP2PVotes; } set { m_ALP2PVotes = value; } }


        public Electorate()
        {
            m_boundaries = new List<Shape>();
        }

        public Electorate(List<String> a_param)
        {
            this.m_division = (string)a_param[0];
            this.m_state = (string)a_param[1];
            this.m_ALPVotes = System.Convert.ToDouble(a_param[2]);
            this.m_LPVotes = System.Convert.ToDouble(a_param[3]);
            this.m_NPVotes = System.Convert.ToDouble(a_param[4]);
            this.m_DEMVotes = System.Convert.ToDouble(a_param[5]);
            this.m_GRNVotes = System.Convert.ToDouble(a_param[6]);
            this.m_OTHVotes = System.Convert.ToDouble(a_param[7]);
            this.m_LNP2PVotes = System.Convert.ToDouble(a_param[8]);
            this.m_ALP2PVotes = System.Convert.ToDouble(a_param[9]);

        }
    }
}
