using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;



namespace cartographer
{
    public class ElectorateImporter : Importer
    {
        private List<Electorate> m_ElectorateDataMID;
        private List<Electorate> m_ElectorateDataMIF;
        private List<Electorate> m_ElectorateDataXLS;
        private List<Electorate> m_ElectorateData;

        private StreamReader m_ElectorateReaderMID;
        private StreamReader m_ElectorateReaderMIF;

        public ElectorateImporter()
        {
            m_ElectorateDataMID = new List<Electorate>();
            m_ElectorateDataMIF = new List<Electorate>();
            m_ElectorateDataXLS = new List<Electorate>();
        }

        public List<Electorate> MergeData()
        {
            List<Electorate> _Electorates = new List<Electorate>();
            for (int i = 0; i < m_ElectorateDataMID.Count; i++)
            {
                Electorate _ElectorateData = new Electorate();
                _Electorates.Add(_ElectorateData);
            }
            for (int i = 0; i < m_ElectorateDataMID.Count; i++)
            {
                //MIF DATA
                _Electorates[i].Boundaries = m_ElectorateDataMIF[i].Boundaries;

                //MID DATA
                _Electorates[i].Actual = m_ElectorateDataMID[i].Actual;
                _Electorates[i].Area = m_ElectorateDataMID[i].Area;
                _Electorates[i].Division = m_ElectorateDataMID[i].Division;
                _Electorates[i].ID = m_ElectorateDataMID[i].ID;
                _Electorates[i].Name = m_ElectorateDataMID[i].Name;
                _Electorates[i].Over18 = m_ElectorateDataMID[i].Over18;
                _Electorates[i].Projected = m_ElectorateDataMID[i].Projected;
                _Electorates[i].TotalPopulation = m_ElectorateDataMID[i].TotalPopulation;

                //XLS DATA
                for (int j = 0; j < m_ElectorateDataMID.Count; j++)
                {
                    for (int k = 0; k < m_ElectorateDataXLS.Count; k++)
                    {
                        if (m_ElectorateDataMID[i].Name == m_ElectorateDataXLS[k].Division)
                        {
                            _Electorates[i].ALP2PVotes = m_ElectorateDataXLS[k].ALP2PVotes;
                            _Electorates[i].ALPVotes = m_ElectorateDataXLS[k].ALPVotes;
                            _Electorates[i].DEMVotes = m_ElectorateDataXLS[k].DEMVotes;
                            _Electorates[i].GRNVotes = m_ElectorateDataXLS[k].GRNVotes;
                            _Electorates[i].LNP2PVotes = m_ElectorateDataXLS[k].LNP2PVotes;
                            _Electorates[i].LPVotes = m_ElectorateDataXLS[k].LPVotes;
                            _Electorates[i].NPVotes = m_ElectorateDataXLS[k].NPVotes;
                            _Electorates[i].OTHVotes = m_ElectorateDataXLS[k].OTHVotes;
                        }
                    }
                }
            }
            return _Electorates;
        }
        public void ParseStateResultsXLS(string a_filename)
        {
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + a_filename + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";

            OleDbConnection xlsConnection = new OleDbConnection(connectionString);
            xlsConnection.Open();

            //OleDbCommand commandtest = new OleDbCommand("SELECT * FROM [2004 Election$]", connection);
            OleDbDataAdapter _allData = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", xlsConnection);
            DataSet ds = new DataSet();
            _allData.Fill(ds);
            var table = ds.Tables[0];
            int i = 1;

            OleDbConnection _connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=data/db.mdb"); //!testing
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    _connection.Open();
                    string command = "UPDATE StateElectorate SET WinningParty='" + row.ItemArray[4] + "' WHERE ElectorateName='" + row.ItemArray[1] + "';";
                    OleDbCommand updateCommand = new OleDbCommand(command, _connection);
                    updateCommand.ExecuteNonQuery();
                }
                finally
                {
                    _connection.Close();
                }
                i++;

            }
        }
        public void ParseFederalXLS(string a_filename)
        {
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + a_filename + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";

            OleDbConnection xlsConnection = new OleDbConnection(connectionString);
            xlsConnection.Open();

            //OleDbCommand commandtest = new OleDbCommand("SELECT * FROM [2004 Election$]", connection);
            OleDbDataAdapter _allData = new OleDbDataAdapter("SELECT * FROM [2004 Election Results$]", xlsConnection);
            DataSet ds = new DataSet();
            _allData.Fill(ds);
            var table = ds.Tables[0];
            int i = 1;

            OleDbConnection _connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=data/db.mdb"); //!testing
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    _connection.Open();
                    string command = "UPDATE FederalElectorate SET TermsInPower=" + (2004 - int.Parse(row.ItemArray[4].ToString())) / 4 + ", TPP = "+ row.ItemArray[2] +" WHERE ElectorateName='"+ row.ItemArray[0] +"';";
                    OleDbCommand updateCommand = new OleDbCommand(command, _connection);
                    updateCommand.ExecuteNonQuery();
                }
                finally
                {
                    _connection.Close();
                }
                i++;

            }
        }
        public void ParseStateFederalRelationalXLS(string a_filename)
        {
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + a_filename + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";

            OleDbConnection xlsConnection = new OleDbConnection(connectionString);
            xlsConnection.Open();

            //OleDbCommand commandtest = new OleDbCommand("SELECT * FROM [2004 Election$]", connection);
            OleDbDataAdapter _allData = new OleDbDataAdapter("SELECT * FROM [Seat Mappings$]", xlsConnection);
            DataSet ds = new DataSet();
            _allData.Fill(ds);
            var table = ds.Tables[0];
            int i = 1;

            OleDbConnection _connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=data/db.mdb"); //!testing
            _connection.Open();
            OleDbCommand deleteCommand = new OleDbCommand("DELETE StateElectorate.* FROM StateElectorate;", _connection);
            deleteCommand.ExecuteNonQuery();
            _connection.Close();
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    _connection.Open();
                    string values = "'" + row.ItemArray[1] + "', '" + row.ItemArray[0] + "'";
                    string command = "INSERT INTO [StateElectorate] (ElectorateName, FederalElectorate) VALUES (" + values + ");";
                    OleDbCommand insertCommand = new OleDbCommand(command, _connection);
                    insertCommand.ExecuteNonQuery();   
                }
                finally
                {
                    _connection.Close();
                }
                i++;

            }
        }
        public List<Electorate> MergeDataPhaseTwo(List<Electorate> a_ElectorateData, string stateFederalRelationFilename, string federalElectionTPPHistoryFilename, string stateResultFilename)
        {
            OleDbConnection _connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=data/db.mdb"); //!testing

            try
            {
                _connection.Open();
                OleDbCommand deleteCommand = new OleDbCommand("DELETE FederalElectorate.* FROM FederalElectorate;", _connection);
                deleteCommand.ExecuteNonQuery();
                _connection.Close();
                _connection.Open();
                foreach (var electorate in a_ElectorateData)
                {
                    string columns = "ID, ElectorateName, Actual, Projected, TotalPopulation, Over18, Area, ALPVotes, LPVotes, NPVotes, DEMVotes, GRNVotes, OTHVotes, LNP2PVotes, ALP2PVotes";
                    //string columns = "";//, TermsInPower, TPP";
                    string values = electorate.ID + ", '" + electorate.Name + "', " + electorate.Actual + ", " + electorate.Projected + ", " + electorate.TotalPopulation + ", " + electorate.Over18 + ", " + electorate.Area + ", " + electorate.ALPVotes + ", " + electorate.LPVotes + ", " + electorate.NPVotes + ", " + electorate.DEMVotes + ", " + electorate.GRNVotes + ", " + electorate.OTHVotes + ", " + electorate.LNP2PVotes + ", " + electorate.ALP2PVotes;
                    string command = "INSERT INTO [FederalElectorate] (" + columns + ") VALUES (" + values + ");";
                    OleDbCommand insertCommand = new OleDbCommand(command, _connection);

                    insertCommand.ExecuteNonQuery();
                }
            }
            finally
            {
                _connection.Close();
            }
            ParseStateFederalRelationalXLS(stateFederalRelationFilename);
            ParseFederalXLS(federalElectionTPPHistoryFilename);
            ParseStateResultsXLS(stateResultFilename);
            return null;
        }
        public bool ParseMID(string filename)
        {
            try
            {
                m_ElectorateReaderMID = new StreamReader(filename);
            }
            catch
            {
                //Environment.Exit(0); //HAAHAHAHAHAHAHA
            }
            while (!m_ElectorateReaderMID.EndOfStream)
            {
                ParseLineMID(m_ElectorateReaderMID.ReadLine());    
            }
            return true;
        }

        protected bool ParseLineMID(string line)
        {
            string lineSoFar = "";
            string[] electorateData = new string[9];
            int electorateDataPosition = 0;
            //id,"electoratename",numccds,actual,projected,totalpop,over18,area,"name"
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ',')
                {
                    electorateData[electorateDataPosition] = lineSoFar;
                    lineSoFar = "";
                    electorateDataPosition++;
                }
                else if (line[i] == '\"') { /*ignore*/ }
                else
                {
                    lineSoFar += line[i];
                }
            }
            electorateData[electorateDataPosition] = lineSoFar;
            Electorate electorate = new Electorate();
            electorate.ID = int.Parse(electorateData[0]);
            electorate.Name = electorateData[1];
            //numccds???????????
            electorate.Actual = int.Parse(electorateData[3]);
            electorate.Projected = int.Parse(electorateData[4]);
            electorate.TotalPopulation = int.Parse(electorateData[5]);
            electorate.Over18 = int.Parse(electorateData[6]);
            electorate.Area = float.Parse(electorateData[7]);
            m_ElectorateDataMID.Add(electorate);
            return true;
        }

        public bool ParseMIF(string filename)
        {
            
            try
            {
                m_ElectorateReaderMIF = new StreamReader(filename);
            }
            catch
            {
                //shits fucked
            }
            for (int i = 1; i <= 17; i++)
            {
                //drop the first 17 lines -> irrelevant
                m_ElectorateReaderMIF.ReadLine();
            }   
            int currentShape = -1;
            int currentRegion = -1;
            int currentPoint = -1;
            while (!m_ElectorateReaderMIF.EndOfStream)
            {

                string line = m_ElectorateReaderMIF.ReadLine();
                if (line[0] == 'R')
                {
                    //number of polys
                    Electorate electorate = new Electorate();
                    m_ElectorateDataMIF.Add(electorate);
                    currentRegion++;
                    string lineSoFar = "";
                    for (int i = 7; i < line.Length; i++)
                    {
                        lineSoFar += line[i];
                    }
                    for (int i = 1; i <= int.Parse(lineSoFar); i++)
                    {
                        Shape shape = new Shape();
                        electorate.Boundaries.Add(shape);
                    }
                }
                else if (line[0] == ' ' && line[1] == ' ' && line[2] != ' ')
                {
                    //num points
                    string lineSoFar = "";
                    for (int i = 2; i < line.Length; i++)
                    {
                        lineSoFar += line[i];
                    }
                    currentShape++;
                    currentPoint = -1;
                    for (int i = 0; i < int.Parse(lineSoFar); i++)
		            {
                        Vector2 tempVector2 = new Vector2();
                        m_ElectorateDataMIF[currentRegion].Boundaries[currentShape].points.Add(tempVector2);
		            }                    
                }
                else if (line[0] == ' ' && line[1] == ' ' && line[2] == ' ')
                {
                    //pen brush center
                    if (line[4] == 'C')
                    {
                        m_ElectorateDataMIF[currentRegion].Boundaries[currentShape].center = PointParse(line.Substring(11));

                        currentPoint = -1;
                        currentShape = -1;
                    }

                }
                else
                {
                    //its a point
                    currentPoint++;
                    m_ElectorateDataMIF[currentRegion].Boundaries[currentShape].points[currentPoint] = PointParse(line);
                }
                
            }
            return true; //cos it so works
        }

        private Vector2 PointParse(string line)
        {
            string[] points = line.Split(' ');
            Vector2 point = new Vector2();
            point.X = double.Parse(points[0]);
            point.Y = float.Parse(points[1]);
            return point;
        }

        protected bool ParseLineMIF(string line)
        {
            return true;
        }

        public bool ParseXLS(string a_xls)
        {
            string _file = Directory.GetCurrentDirectory();
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + a_xls + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
            //|DataDirectory|\data\Qld_FederalResults by Electorate-2004.xls;
 
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();

            //OleDbCommand commandtest = new OleDbCommand("SELECT * FROM [2004 Election$]", connection);
            OleDbDataAdapter _allData = new OleDbDataAdapter("SELECT * FROM [2004 Election$]", connection);
            DataSet ds = new DataSet();
            _allData.Fill(ds);
            var table = ds.Tables[0];
            int i = 1;
            foreach (DataRow row in table.Rows)
            {
                //Console.Out.WriteLine(row.ItemArray);
                 if (i == 149) { break; }

                List<String> _param = new List<String>();
                foreach (var thing in row.ItemArray)
                {
                   
                    if (thing.ToString().Length != 0)
                    {
                        _param.Add(thing.ToString());
                    }
                    
                }
                if (_param.Count() > 1)
                {
                    Electorate _electorate = new Electorate(_param);
                    m_ElectorateDataXLS.Add(_electorate);
                    Console.Out.WriteLine("Creating {0} size is {1}", _param[0], _param.Count()); 
                }

                i++;

            }


          
            return false;
        }

        protected bool ParseLineXLS()
        {
            return false;
        }

    }
}
