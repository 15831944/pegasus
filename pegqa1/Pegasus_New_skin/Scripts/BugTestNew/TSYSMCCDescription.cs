using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class TSYSMCCDescription : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        
        public void tSYSMCCDescription()
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

                executionLog.Log("TSYSMCCDescription", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("TSYSMCCDescription", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TSYSMCCDescription", "Go to All Processors");
                VisitOffice("processor_types");
                all_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("TSYSMCCDescription", "Enter Processor Name In Search field");
                all_ProcessorsHelper.TypeText("SearchProcName", "TSYS");
                all_ProcessorsHelper.WaitForWorkAround(3000);

                var loc = "//table[@id='list1']/tbody[1]/tr[2]/td[@title='TSYS']";

                if (all_ProcessorsHelper.IsElementPresent(loc))
                {
                    Console.WriteLine("TSYS Processor found");
                    executionLog.Log("TSYSMCCDescription", "Go to All Merchants");
                    VisitOffice("clients");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSMCCDescription", "Click on First Client");
                    office_ClientsHelper.ClickElement("Client1");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSMCCDescription", "Go to Business Details Tab");
                    office_ClientsHelper.ClickElement("BusinessDetailsTab");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSMCCDescription", "Select TSYS Processor");
                    office_ClientsHelper.Select("Processor", "TSYS");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSMCCDescription", "Select and Option in MCC/SIC Code drop down");
                    office_ClientsHelper.SelectByText("MCC_SICCode", "1731 - Electrical Contractors");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSMCCDescription", "Verify MCC Description populated");
                    office_ClientsHelper.VerifyTextBoxValue("MCCDescription", "Electrical Contractors");
                    Console.WriteLine("MCC Description populated");

                }

                else
                {
                    Console.WriteLine("TSYS Processor not found. Hence creating one");
                    executionLog.Log("TSYSMCCDescription", "Click on Create button");
                    all_ProcessorsHelper.ClickElement("Create");
                    all_ProcessorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSMCCDescription", "Enter Processor Name");
                    all_ProcessorsHelper.TypeText("ProcName", "TSYS");

                    executionLog.Log("TSYSMCCDescription", "Select Processor to fetch field from");
                    all_ProcessorsHelper.Select("FetchField", "TSYS");

                    executionLog.Log("TSYSMCCDescription", "Enter Processor Code");
                    all_ProcessorsHelper.TypeText("ProcCode", Code);

                    executionLog.Log("TSYSMCCDescription", "Click on Create button");
                    all_ProcessorsHelper.ClickElement("SaveBtn");
                    all_ProcessorsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSMCCDescription", "Go to All Merchants");
                    VisitOffice("clients");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSMCCDescription", "Click on First Client");
                    office_ClientsHelper.ClickElement("Client1");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSMCCDescription", "Go to Business Details Tab");
                    office_ClientsHelper.ClickElement("BusinessDetailsTab");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSMCCDescription", "Select TSYS Processor");
                    office_ClientsHelper.Select("Processor", "TSYS");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSMCCDescription", "Select and Option in MCC/SIC Code drop down");
                    office_ClientsHelper.SelectByText("MCC_SICCode", "1731 - Electrical Contractors");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSMCCDescription", "Verify MCC Description populated");
                    office_ClientsHelper.VerifyTextBoxValue("MCCDescription", "Electrical Contractors");
                    Console.WriteLine("MCC Description populated");
                }

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TSYSMCCDescription");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("TSYS MCC Description");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("TSYS MCC Description", "Bug", "Medium", "Office Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("TSYS MCC Description");
                        TakeScreenshot("TSYSMCCDescription");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TSYSMCCDescription.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TSYSMCCDescription");
                        string id = loginHelper.getIssueID("TSYS MCC Description");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TSYSMCCDescription.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("TSYS MCC Description"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("TSYS MCC Description");
                //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("TSYSMCCDescription");
                executionLog.WriteInExcel("TSYS MCC Description", Status, JIRA, "Office Merchant");
            }
            
        }
    }
}
