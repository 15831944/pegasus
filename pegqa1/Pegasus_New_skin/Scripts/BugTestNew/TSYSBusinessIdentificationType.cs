using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class TSYSBusinessIdentificationType : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        
        public void tSYSBusinessIdentificationType()
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

            executionLog.Log("TSYSBusinessIdentificationType", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("TSYSBusinessIdentificationType", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TSYSBusinessIdentificationType", "Go to All Processors");
                VisitOffice("processor_types");
                all_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("TSYSBusinessIdentificationType", "Enter Processor Name In Search field");
                all_ProcessorsHelper.TypeText("SearchProcName", "TSYS");
                all_ProcessorsHelper.WaitForWorkAround(3000);

                var loc = "//table[@id='list1']/tbody[1]/tr[2]/td[@title='TSYS']";

                if (all_ProcessorsHelper.IsElementPresent(loc))
                {
                    Console.WriteLine("TSYS Processor found");
                    executionLog.Log("TSYSBusinessIdentificationType", "Go to All Merchants");
                    VisitOffice("clients");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSBusinessIdentificationType", "Click on First Client");
                    office_ClientsHelper.ClickElement("Client1");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSBusinessIdentificationType", "Go to Company Details Tab");
                    office_ClientsHelper.ClickElement("CompanyDetailsTab");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSBusinessIdentificationType", "Select TSYS Processor");
                    office_ClientsHelper.Select("Processor", "TSYS");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSBusinessIdentificationType", "Go to Owners Tab");
                    office_ClientsHelper.ClickElement("OwnerTab");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSBusinessIdentificationType", "Click on Owner Identification drop down");
                    office_ClientsHelper.ClickElement("BusinessIdentificationType");

                    executionLog.Log("TSYSBusinessIdentificationType", "Verify options under drop down");
                    office_ClientsHelper.IsElementVisible("//option[@value='Govt. Issued Business License']");
                    Console.WriteLine("Govt. Issued Business License option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='Tax Return']");
                    Console.WriteLine("Tax Return option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='Entity Articles']");
                    Console.WriteLine("Entity Articles option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='Business Financial Statement']");
                    Console.WriteLine("Business Financial Statement option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='Government Entity']");
                    Console.WriteLine("Government Entity option present");
                }

                else
                {
                    Console.WriteLine("TSYS Processor not found. Hence creating one");
                    executionLog.Log("TSYSBusinessIdentificationType", "Click on Create button");
                    all_ProcessorsHelper.ClickElement("Create");
                    all_ProcessorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSBusinessIdentificationType", "Enter Processor Name");
                    all_ProcessorsHelper.TypeText("ProcName", "TSYS");

                    executionLog.Log("TSYSBusinessIdentificationType", "Select Processor to fetch field from");
                    all_ProcessorsHelper.Select("FetchField", "TSYS");

                    executionLog.Log("TSYSBusinessIdentificationType", "Enter Processor Code");
                    all_ProcessorsHelper.TypeText("ProcCode", Code);

                    executionLog.Log("TSYSBusinessIdentificationType", "Click on Create button");
                    all_ProcessorsHelper.ClickElement("SaveBtn");
                    all_ProcessorsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSBusinessIdentificationType", "Go to All Merchants");
                    VisitOffice("clients");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSBusinessIdentificationType", "Click on First Client");
                    office_ClientsHelper.ClickElement("Client1");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSBusinessIdentificationType", "Go to Company Details Tab");
                    office_ClientsHelper.ClickElement("CompanyDetailsTab");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSBusinessIdentificationType", "Select TSYS Processor");
                    office_ClientsHelper.Select("Processor", "TSYS");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSBusinessIdentificationType", "Go to Owners Tab");
                    office_ClientsHelper.ClickElement("OwnerTab");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSBusinessIdentificationType", "Click on Owner Identification drop down");
                    office_ClientsHelper.ClickElement("BusinessIdentificationType");

                    executionLog.Log("TSYSBusinessIdentificationType", "Verify options under drop down");
                    office_ClientsHelper.IsElementVisible("//option[@value='Govt. Issued Business License']");
                    Console.WriteLine("Govt. Issued Business License option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='Tax Return']");
                    Console.WriteLine("Tax Return option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='Entity Articles']");
                    Console.WriteLine("Entity Articles option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='Business Financial Statement']");
                    Console.WriteLine("Business Financial Statement option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='Government Entity']");
                    Console.WriteLine("Government Entity option present");
                }



            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TSYSBusinessIdentificationType");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("TSYS Business Identification Type");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("TSYS Business Identification Type", "Bug", "Medium", "Office Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("TSYS Business Identification Type");
                        TakeScreenshot("TSYSBusinessIdentificationType");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TSYSBusinessIdentificationType.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TSYSBusinessIdentificationType");
                        string id = loginHelper.getIssueID("TSYS Business Identification Type");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TSYSBusinessIdentificationType.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("TSYS Business Identification Type"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("TSYS Business Identification Type");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("TSYSBusinessIdentificationType");
                executionLog.WriteInExcel("TSYS Business Identification Type", Status, JIRA, "Office Merchant");
            }
        }
    }
}
