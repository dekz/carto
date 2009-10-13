using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace cartographer
{
    public class FederalElectorate : Electorate
    {
        public int TermsInPower { get; set; }
        public double TPPMargin { get; set; }

        public FederalElectorate()
            : base()
        {

        }

        public void CalculateSafety(DataGrabber grabber)
        {
            double safety = this.TPPMargin;

            // Previously Won Factor
            if (this.TermsInPower >= 4)
            {
                safety += 10.0f;
            }
            else if (this.TermsInPower >= 2)
            {
                safety += 5.0f;
            }


            // State Impact Factor
            foreach (StateElectorate stateElec in grabber.StateElectorates)
            {
                if (stateElec.FederalElectorate.Name == this.Name)
                {
                    if (stateElec.WinningParty == this.WinningParty)
                    {
                        safety += 2.0f;
                    }

                    else
                    {
                        if (stateElec.WinningParty != null)
                        {
                            safety -= 4.0f;
                        }
                    }
                }
            }

            if (safety < 5)
            {
                this.SeatSafety = "Marginal Seat";
            }
            else if (safety > 5 && safety < 10)
            {
                this.SeatSafety = "Moderately Safe";
            }
            else if (safety > 10 && safety < 15)
            {
                this.SeatSafety = "Safe";
            }
            else if (safety > 15 && safety < 25)
            {
                this.SeatSafety = "Very Safe";
            }
            else if (safety > 25)
            {
                this.SeatSafety = "Rock Solid";
            }
        }

    }

    public class StateElectorate
    {
        public String Name { get; set; }
        public String WinningParty { get; set; }
        public Electorate FederalElectorate { get; set; }

        public StateElectorate()
        {

        }
    }

    public class DataGrabber
    {
        public List<FederalElectorate> FederalElectorates { get; set; }
        public List<StateElectorate> StateElectorates { get; set; }

        public DataGrabber()
        {
            FederalElectorates = new List<FederalElectorate>();
            StateElectorates = new List<StateElectorate>();
        }

        public void importData()
        {
            OleDbConnection _connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=data/db.mdb"); //!testing

            try
            {
                _connection.Open();

                OleDbDataAdapter federalData = new OleDbDataAdapter("Select * FROM FederalElectorate", _connection);
                DataSet ds = new DataSet();
                federalData.Fill(ds);
                var table = ds.Tables[0];
                _connection.Close();

                foreach (DataRow rows in table.Rows)
                {
                    FederalElectorate _electorate = new FederalElectorate();
                    _electorate.Name = rows.ItemArray[1].ToString();
                    _electorate.Actual = (int)rows.ItemArray[2];
                    _electorate.Projected = (int)rows.ItemArray[3];
                    _electorate.TotalPopulation = (int)rows.ItemArray[4];
                    _electorate.Over18 = (int)rows.ItemArray[5];
                    _electorate.Area = (int)rows.ItemArray[6];
                    _electorate.ALPVotes = (int)rows.ItemArray[7];
                    _electorate.LPVotes = (int)rows.ItemArray[8];
                    _electorate.NPVotes = (int)rows.ItemArray[9];
                    _electorate.DEMVotes = (int)rows.ItemArray[10];
                    _electorate.GRNVotes = (int)rows.ItemArray[11];
                    _electorate.OTHVotes = (int)rows.ItemArray[12];
                    _electorate.LNP2PVotes = (int)rows.ItemArray[13];
                    _electorate.ALP2PVotes = (int)rows.ItemArray[14];
                    _electorate.TermsInPower = (int)rows.ItemArray[15];
                    _electorate.TPPMargin = (double)rows.ItemArray[16];

                    if (_electorate.LNP2PVotes < _electorate.ALP2PVotes)
                    {
                        _electorate.WinningParty = "ALP";
                    }
                    else
                    {
                        _electorate.WinningParty = "LIB";
                    }

                    FederalElectorates.Add(_electorate);
                }

                _connection.Open();
                OleDbDataAdapter fstateData = new OleDbDataAdapter("Select * FROM StateElectorate", _connection);
                DataSet dsState = new DataSet();
                fstateData.Fill(dsState);
                var tableState = dsState.Tables[0];
                _connection.Close();

                foreach (DataRow rows in tableState.Rows)
                {
                    StateElectorate _stateElectorate = new StateElectorate();
                    _stateElectorate.Name = rows.ItemArray[1].ToString();
                    _stateElectorate.WinningParty = rows.ItemArray[2].ToString();
                    //_stateElectorate.FederalElectorate = rows.ItemArray[3].ToString();
                    //need to link the state electorates to the federal electorate data

                    foreach (Electorate electorate in FederalElectorates)
                    {
                        if (rows.ItemArray[3].ToString() == electorate.Name)
                        {
                            _stateElectorate.FederalElectorate = electorate;
                        }

                    }

                    StateElectorates.Add(_stateElectorate);
                }
            }
            finally
            {
                _connection.Close();

            }
            foreach (FederalElectorate fed in FederalElectorates)
            {
                fed.CalculateSafety(this);
            }
        }
    }
}
