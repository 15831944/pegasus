using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class TSYSDepositType : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]

        public void tSYSDepositType()
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


           // var DBA = "ClientDBA" + RandomNumber(111, 99999);
            var Code = "" + RandomNumber(1, 999);

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("TSYSDepositType", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: "+username[0]+" / "+password[0]);

                executionLog.Log("TSYSDepositType", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TSYSDepositType", "Go to All Processors");
                VisitOffice("processor_types");
                all_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("TSYSDepositType", "Enter Processor Name In Search field");
                all_ProcessorsHelper.TypeText("SearchProcName", "TSYS");
                all_ProcessorsHelper.WaitForWorkAround(3000);

                var loc = "//table[@id='list1']/tbody[1]/tr[2]/td[@title='TSYS']";

                if (all_ProcessorsHelper.IsElementPresent(loc))
                {
                    Console.WriteLine("TSYS Processor found");
                    executionLog.Log("TSYSDepositType", "Go to All Merchants");
                    VisitOffice("clients");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSDepositType", "Click on First Client");
                    office_ClientsHelper.ClickElement("Client1");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSDepositType", "Go to Business Details Tab");
                    office_ClientsHelper.ClickElement("BusinessDetailsTab");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSDepositType", "Select TSYS Processor");
                    office_ClientsHelper.Select("Processor", "TSYS");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSDepositType", "Expand Business Banking Account Tab");
                    office_ClientsHelper.ClickElement("BusinessBankingAccHeading");
                    office_ClientsHelper.WaitForWorkAround(1000);

                    executionLog.Log("TSYSDepositType", "Click on Deposit Type drop down");
                    office_ClientsHelper.ClickElement("DepositType");

                    executionLog.Log("TSYSDepositType", "Verify By Batch Option is present");
                    office_ClientsHelper.IsElementVisible("//option[@value='By Batch']");

                    executionLog.Log("TSYSDepositType", "Click on Deposit Type drop down");
                    office_ClientsHelper.IsElementVisible("//option[@value='Combined']");
                    Console.WriteLine("Options are visible");
                 
                }

                else
                {
                    Console.WriteLine("TSYS Processor not found. Hence creating one");
                    executionLog.Log("TSYSDepositType", "Click on Create button");
                    all_ProcessorsHelper.ClickElement("Create");
                    all_ProcessorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSDepositType", "Enter Processor Name");
                    all_ProcessorsHelper.TypeText("ProcName", "TSYS");

                    executionLog.Log("TSYSDepositType", "Select Processor to fetch field from");
                    all_ProcessorsHelper.Select("FetchField", "TSYS");

                    executionLog.Log("TSYSDepositType", "Enter Processor Code");
                    all_ProcessorsHelper.TypeText("ProcCode", Code);

                    executionLog.Log("TSYSDepositType", "Click on Create button");
                    all_ProcessorsHelper.ClickElement("SaveBtn");
                    all_ProcessorsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSDepositType", "Go to All Merchants");
                    VisitOffice("clients");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSDepositType", "Click on First Client");
                    office_ClientsHelper.ClickElement("Client1");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSDepositType", "Go to Business Details Tab");
                    office_ClientsHelper.ClickElement("BusinessDetailsTab");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSDepositType", "Select TSYS Processor");
                    office_ClientsHelper.Select("Processor", "TSYS");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSDepositType", "Expand Business Banking Account Tab");
                    office_ClientsHelper.ClickElement("BusinessBankingAccHeading");
                    office_ClientsHelper.WaitForWorkAround(1000);

                    executionLog.Log("TSYSDepositType", "Click on Deposit Type drop down");
                    office_ClientsHelper.ClickElement("DepositType");

                    executionLog.Log("TSYSDepositType", "Verify By Batch Option is present");
                    office_ClientsHelper.IsElementVisible("//option[@value='By Batch']");

                    executionLog.Log("TSYSDepositType", "Click on Deposit Type drop down");
                    office_ClientsHelper.IsElementVisible("//option[@value='Combined']");
                    Console.WriteLine("Options are visible");
                }

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TSYSDepositType");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("TSYS Deposit Type");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("TSYS Deposit Type", "Bug", "Medium", "Office Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("TSYS Deposit Type");
                        TakeScreenshot("TSYSDepositType");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TSYSDepositType.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TSYSDepositType");
                        string id = loginHelper.getIssueID("TSYS Deposit Type");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TSYSDepositType.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("TSYS Deposit Type"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("TSYS Deposit Type");
                //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("TSYSDepositType");
                executionLog.WriteInExcel("TSYS Deposit Type", Status, JIRA, "Office Merchant");
            }
            
        }
    }
}
