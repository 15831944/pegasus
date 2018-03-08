using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class TSYSSeasonalMonthsOutOfOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void tSYSSeasonalMonthsOutOfOrder()
        {
            string[] username = null;
            string[] password = null;

           

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");


            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var all_ProcessorsHelper = new All_ProcessorsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());


            var DBA = "ClientDBA" + RandomNumber(1, 500);
            var Code = "" + RandomNumber(1, 999);

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: "+username[0]+" / "+password[0]);

                executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Go to All Processors");
                VisitOffice("processor_types");
                all_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Enter Processor Name In Search field");
                all_ProcessorsHelper.TypeText("SearchProcName", "TSYS");
                all_ProcessorsHelper.WaitForWorkAround(3000);

                var loc = "//table[@id='list1']/tbody[1]/tr[2]/td[@title='TSYS']";

                if (all_ProcessorsHelper.IsElementPresent(loc))
                {
                    Console.WriteLine("TSYS Processor found");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Go to All Merchants");
                    VisitOffice("clients");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Click on First Client");
                    office_ClientsHelper.ClickElement("Client1");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Go to Business Details Tab");
                    office_ClientsHelper.ClickElement("BusinessDetailsTab");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Select TSYS Processor");
                    office_ClientsHelper.Select("Processor", "TSYS");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Click on High Volume Month drop down");
                    office_ClientsHelper.ClickElement("HighVolumeMonth");

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("FirstOption", "January");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("SecondOption", "February");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("ThirdOption", "March");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("FourthOption", "April");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("FifthOption", "May");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("SixthOption", "June");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("SeventhOption", "July");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("EighthOption", "August");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("NinthOption", "September");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("TenthOption", "October");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("EleventhOption", "November");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("TwelevthOption", "December");
                    Console.WriteLine("Months are arranged according to Jan-Dec");
                    
                }

                else
                {
                    Console.WriteLine("TSYS Processor not found. Hence creating one");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Click on Create button");
                    all_ProcessorsHelper.ClickElement("Create");
                    all_ProcessorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Enter Processor Name");
                    all_ProcessorsHelper.TypeText("ProcName", "TSYS");

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Select Processor to fetch field from");
                    all_ProcessorsHelper.Select("FetchField", "TSYS");

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Enter Processor Code");
                    all_ProcessorsHelper.TypeText("ProcCode", Code);

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Click on Create button");
                    all_ProcessorsHelper.ClickElement("SaveBtn");
                    all_ProcessorsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Go to All Merchants");
                    VisitOffice("clients");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Click on First Client");
                    office_ClientsHelper.ClickElement("Client1");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Go to Business Details Tab");
                    office_ClientsHelper.ClickElement("BusinessDetailsTab");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Select TSYS Processor");
                    office_ClientsHelper.Select("Processor", "TSYS");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Click on High Volume Month drop down");
                    office_ClientsHelper.ClickElement("HighVolumeMonth");

                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("FirstOption", "January");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("SecondOption", "February");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("ThirdOption", "March");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("FourthOption", "April");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("FifthOption", "May");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("SixthOption", "June");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("SeventhOption", "July");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("EighthOption", "August");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("NinthOption", "September");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("TenthOption", "October");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("EleventhOption", "November");
                    executionLog.Log("TSYSSeasonalMonthsOutOfOrder", "Verify options under drop down");
                    office_ClientsHelper.VerifyText("TwelevthOption", "December");
                    Console.WriteLine("Months are arranged according to Jan-Dec");
                }

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TSYSSeasonalMonthsOutOfOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("TSYS Seasonal Months Out Of Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("TSYS Seasonal Months Out Of Order", "Bug", "Medium", "Office Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("TSYS Seasonal Months Out Of Order");
                        TakeScreenshot("TSYSSeasonalMonthsOutOfOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TSYSSeasonalMonthsOutOfOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TSYSSeasonalMonthsOutOfOrder");
                        string id = loginHelper.getIssueID("TSYS Seasonal Months Out Of Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TSYSSeasonalMonthsOutOfOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("TSYS Seasonal Months Out Of Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("TSYS Seasonal Months Out Of Order");
                //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("TSYSSeasonalMonthsOutOfOrder");
                executionLog.WriteInExcel("TSYS Seasonal Months Out Of Order", Status, JIRA, "Office Merchant");
            }
            
        }
    }
}
