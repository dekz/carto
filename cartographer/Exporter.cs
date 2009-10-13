using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace cartographer
{
    class Exporter
    {
        private List<Electorate> m_electorates;
        private string colourMode;
        public Exporter(List<Electorate> a_electorates)
        {
            m_electorates = a_electorates;
        }

        public string convertToKml(string mode)
        {
            colourMode = mode;
            string _kml = "";
            StreamReader tr = new StreamReader("data/KmlTemplate.txt");
            FileStream fs = new FileStream("data/kml.kml", FileMode.Create);
            fs.Close();
            StreamWriter tw = new StreamWriter("data/kml.kml");

            // Write the KML template styles.
            while (!tr.EndOfStream)
            {
                tw.WriteLine(tr.ReadLine());
            }
            tr.Close();
            writeCoordinates(tw);
            return _kml;
        }

        public bool exportKMLFile()
        {
            string _returnKML = convertToKml(colourMode);
            return true;
        }

        private void writeCoordinates(StreamWriter tw)
        {
            if (!(m_electorates == null))
            {
                foreach (Electorate elec in m_electorates)
                {
                    if (elec.Drawable)
                    {
                        foreach (Shape bounds in elec.Boundaries)
                        {
                            tw.WriteLine("<Placemark>");
                            tw.WriteLine("<name>" + elec.Name + "</name>");
                            tw.WriteLine("<description>" +
                                "Current Party: " + elec.WinningParty + "\n" +
                                "Seat Safety: " + elec.SeatSafety + "\n\n" +
                                "Population: " + elec.TotalPopulation + "\n" +
                                "Actual: " + elec.Actual + "\n" +
                                "Projected: " + elec.Projected + "\n" +
                                "Over 18: " + elec.Over18 + "\n" +
                                "Area: " + elec.Area + "\n\nVotes\n" +
                                "ALP: " + elec.ALPVotes + "\n" +
                                "LNP: " + elec.LPVotes + "\n" +
                                "NP: " + elec.NPVotes + "\n" +
                                "DEM: " + elec.DEMVotes + "\n" +
                                "GRN: " + elec.GRNVotes + "\n" +
                                "Other: " + elec.OTHVotes + "\n" +
                                "\nTwo Party Preferred\n" +
                                "ALP: " + elec.ALP2PVotes + "\n" +
                                "LNP: " + elec.LNP2PVotes + "\n" +
                                "</description>");

                            string style = "";
                            if (colourMode == "Party")
                            {
                                if (elec.WinningParty == "ALP")
                                {
                                    style = "#Labor";
                                }
                                else if ( elec.WinningParty == "LIB")
                                {
                                    style = "#Liberal";
                                }
                                else if ( elec.WinningParty == "GRN")
                                {
                                    style = "#Green";
                                }
                            }
                            else if (colourMode == "Safety")
                            {
                                switch (elec.SeatSafety)
                                {
                                    case "Marginal Seat":
                                        style = "#Marginal";
                                        break;
                                    case "Moderately Safe":
                                        style = "#Moderate";
                                        break;
                                    case "Safe":
                                        style = "#Safe";
                                        break;
                                    case "Very Safe":
                                        style = "#VSafe";
                                        break;
                                    case "Rock Solid":
                                        style = "#Rock";
                                        break;
                                    default:
                                        break;
                                }
                            }

                            tw.WriteLine("<styleUrl>" + style + "</styleUrl>");
                            tw.WriteLine("<Polygon>");
                            tw.WriteLine("<extrude>0</extrude>");
                            tw.WriteLine("<tessellate>0</tessellate>");
                            tw.WriteLine("<altitudeMode>clampToGround</altitudeMode>");
                            tw.WriteLine("<outerBoundaryIs>");
                            tw.WriteLine("<LinearRing>");
                            tw.WriteLine("<coordinates>");

                            foreach (Vector2 point in bounds.points)
                            {
                                string pts = (point.X.ToString() + "," + point.Y.ToString() + ",20");
                                tw.WriteLine(pts);
                            }
                            tw.WriteLine("</coordinates>");
                            tw.WriteLine("</LinearRing>");
                            tw.WriteLine("</outerBoundaryIs>");
                            tw.WriteLine("</Polygon>");
                            tw.WriteLine("</Placemark>");
                        }
                        // tw.WriteLine("</Placemark>");
                    }
                }

                tw.WriteLine("</Document>");
                tw.WriteLine("</kml>");
                Console.Out.WriteLine("Done");
                tw.Close();
            }
        }
    }
}
