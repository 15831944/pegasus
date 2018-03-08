using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class TSYSOwnerSSNValidation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        public void tSYSOwnerSSNValidation()
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

                executionLog.Log("TSYSOwnerSSNValidation", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("TSYSOwnerSSNValidation", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TSYSOwnerSSNValidation", "Go to All Processors");
                VisitOffice("processor_types");
                all_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("TSYSOwnerSSNValidation", "Enter Processor Name In Search field");
                all_ProcessorsHelper.TypeText("SearchProcName", "TSYS");
                all_ProcessorsHelper.WaitForWorkAround(3000);

                var loc = "//table[@id='list1']/tbody[1]/tr[2]/td[@title='TSYS']";

                if (all_ProcessorsHelper.IsElementPresent(loc))
                {
                    Console.WriteLine("TSYS Processor found");
                    executionLog.Log("TSYSOwnerSSNValidation", "Go to All Merchants");
                    VisitOffice("clients");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSOwnerSSNValidation", "Click on First Client");
                    office_ClientsHelper.ClickElement("Client1");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSOwnerSSNValidation", "Go to Company Details Tab");
                    office_ClientsHelper.ClickElement("CompanyDetailsTab");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSOwnerIdentificationType", "Select TSYS Processor");
                    office_ClientsHelper.Select("Processor", "TSYS");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSOwnerSSNValidation", "Go to Owners Tab");
                    office_ClientsHelper.ClickElement("OwnerTab");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSOwnerSSNValidation", "Enter 10 digits in SSN");
                    office_ClientsHelper.TypeText("Owner1SSN", "1234567890");
                    office_ClientsHelper.WaitForWorkAround(1000);

                    executionLog.Log("TSYSOwnerSSNValidation", "Verify only 9 digits saved");
                    office_ClientsHelper.verify9digtsSSN("Owner1SSN");
                   
                }

                else
                {
                    Console.WriteLine("TSYS Processor not found. Hence creating one");
                    executionLog.Log("TSYSOwnerSSNValidation", "Click on Create button");
                    all_ProcessorsHelper.ClickElement("Create");
                    all_ProcessorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSOwnerSSNValidation", "Enter Processor Name");
                    all_ProcessorsHelper.TypeText("ProcName", "TSYS");

                    executionLog.Log("TSYSOwnerSSNValidation", "Select Processor to fetch field from");
                    all_ProcessorsHelper.Select("FetchField", "TSYS");

                    executionLog.Log("TSYSOwnerSSNValidation", "Enter Processor Code");
                    all_ProcessorsHelper.TypeText("ProcCode", Code);

                    executionLog.Log("TSYSOwnerSSNValidation", "Click on Create button");
                    all_ProcessorsHelper.ClickElement("SaveBtn");
                    all_ProcessorsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSOwnerSSNValidation", "Go to All Merchants");
                    VisitOffice("clients");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSOwnerSSNValidation", "Click on First Client");
                    office_ClientsHelper.ClickElement("Client1");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSOwnerSSNValidation", "Go to Company Details Tab");
                    office_ClientsHelper.ClickElement("CompanyDetailsTab");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSOwnerSSNValidation", "Select TSYS Processor");
                    office_ClientsHelper.Select("Processor", "TSYS");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSOwnerSSNValidation", "Go to Owners Tab");
                    office_ClientsHelper.ClickElement("OwnerTab");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSOwnerSSNValidation", "Enter 10 digits in SSN");
                    office_ClientsHelper.TypeText("Owner1SSN", "1234567890");

                    executionLog.Log("TSYSOwnerSSNValidation", "Verify only 9 digits saved");
                    office_ClientsHelper.verify9digtsSSN("Owner1SSN");

                }


           }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TSYSOwnerSSNValidation");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("TSYS Owner SSN Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("TSYS Owner SSN Validation", "Bug", "Medium", "Office Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("TSYS Owner SSN Validation");
                        TakeScreenshot("TSYSOwnerSSNValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TSYSOwnerSSNValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TSYSOwnerSSNValidation");
                        string id = loginHelper.getIssueID("TSYS Owner SSN Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TSYSOwnerSSNValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("TSYS Owner SSN Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("TSYS Owner SSN Validation");
                //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("TSYSOwnerSSNValidation");
                executionLog.WriteInExcel("TSYS Owner SSN Validation", Status, JIRA, "Office Merchant");
            }
            
        }
    }
}
