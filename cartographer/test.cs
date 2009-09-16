

namespace cartographer
{
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]//telling NUnit that this class contains test functions
    public class test
    {

        [Test]//telling NUnit that this function should be run during the tests
        public void ConversionTest1Function()
        {
            Dms testDMS = new Dms(24.25);
            Assert.AreEqual(24, testDMS.m_degrees);
            Assert.AreEqual(15, testDMS.m_minutes);
            Assert.AreEqual(0, testDMS.m_seconds);
        }

        [Test]//telling NUnit that this function should be run during the tests
        public void ConversionTest2Function()
        {
            Dms testDMS = new Dms(102.31);
            Assert.AreEqual(102, testDMS.m_degrees);
            Assert.AreEqual(18, testDMS.m_minutes);
            Assert.AreEqual(35.9994, testDMS.m_seconds, 0.001);
        }



        [Test]//telling NUnit that this function should be run during the tests
        public void ImporterTest()
        {
            ElectorateImporter electorateImporter = new ElectorateImporter();
            electorateImporter.ParseMID("QLD_Federal_Electoral_Boundaries.MID");
            electorateImporter.ParseMIF("QLD_Federal_Electoral_Boundaries.mif");
            electorateImporter.ParseXLS();
            List<Electorate> Electorates = new List<Electorate>();
            Electorates = electorateImporter.MergeData();
            Electorate testElectorate = Electorates[0];
            Assert.AreEqual(testElectorate.Name, "Blair");
            Assert.AreEqual(testElectorate.Actual, 87171);
            Assert.AreEqual(testElectorate.Projected, 92524);
            Assert.AreEqual(testElectorate.TotalPopulation, 87454);
            Assert.AreEqual(testElectorate.Over18, 87454);
            Assert.AreEqual(testElectorate.Area, 14859.51, 0.001);

            //Assert.AreEqual(testElectorate.ALPVotes, 31.44);
            Assert.AreEqual(testElectorate.LPVotes, 52.19);
            Assert.AreEqual(testElectorate.NPVotes, 0);
            Assert.AreEqual(testElectorate.DEMVotes, 1.28);
            Assert.AreEqual(testElectorate.GRNVotes, 2.94);
            Assert.AreEqual(testElectorate.OTHVotes, 12.15);
            Assert.AreEqual(testElectorate.LNP2PVotes, 61.21);
            Assert.AreEqual(testElectorate.ALP2PVotes, 38.79);
        }

    }
}