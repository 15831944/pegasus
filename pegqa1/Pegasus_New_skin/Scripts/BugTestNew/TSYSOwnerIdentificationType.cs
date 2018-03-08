using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class TSYSOwnerIdentificationType : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void tSYSOwnerIdentificationType()
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
                
                executionLog.Log("TSYSOwnerIdentificationType", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: "+username[0]+" / "+password[0]);

                executionLog.Log("TSYSOwnerIdentificationType", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TSYSOwnerIdentificationType", "Go to All Processors");
                VisitOffice("processor_types");
                all_ProcessorsHelper.WaitForWorkAround(2000);

                executionLog.Log("TSYSOwnerIdentificationType", "Enter Processor Name In Search field");
                all_ProcessorsHelper.TypeText("SearchProcName", "TSYS");
                all_ProcessorsHelper.WaitForWorkAround(3000);

                var loc = "//table[@id='list1']/tbody[1]/tr[2]/td[@title='TSYS']";

                if (all_ProcessorsHelper.IsElementPresent(loc))
                {
                    Console.WriteLine("TSYS Processor found");
                    executionLog.Log("TSYSOwnerIdentificationType", "Go to All Merchants");
                    VisitOffice("clients");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSOwnerIdentificationType", "Click on First Client");
                    office_ClientsHelper.ClickElement("Client1");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSOwnerIdentificationType", "Go to Company Details Tab");
                    office_ClientsHelper.ClickElement("CompanyDetailsTab");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSOwnerIdentificationType", "Select TSYS Processor");
                    office_ClientsHelper.Select("Processor", "TSYS");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSOwnerIdentificationType", "Go to Owners Tab");
                    office_ClientsHelper.ClickElement("OwnerTab");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSOwnerIdentificationType", "Click on Owner Identification drop down");
                    office_ClientsHelper.ClickElement("OwnerIdentificationType");

                    executionLog.Log("TSYSOwnerIdentificationType", "Verify options under drop down");
                    office_ClientsHelper.IsElementVisible("//option[@value='Driver&#039;s License']");
                    Console.WriteLine("Driver's Licence option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='State ID']");
                    Console.WriteLine("State ID option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='Passport']");
                    Console.WriteLine("Passport option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='Miltary ID']");
                    Console.WriteLine("Miltary ID option present");
                }

                else
                {
                    Console.WriteLine("TSYS Processor not found. Hence creating one");
                    executionLog.Log("TSYSOwnerIdentificationType", "Click on Create button");
                    all_ProcessorsHelper.ClickElement("Create");
                    all_ProcessorsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSOwnerIdentificationType", "Enter Processor Name");
                    all_ProcessorsHelper.TypeText("ProcName", "TSYS");

                    executionLog.Log("TSYSOwnerIdentificationType", "Select Processor to fetch field from");
                    all_ProcessorsHelper.Select("FetchField", "TSYS");

                    executionLog.Log("TSYSOwnerIdentificationType", "Enter Processor Code");
                    all_ProcessorsHelper.TypeText("ProcCode", Code);

                    executionLog.Log("TSYSOwnerIdentificationType", "Click on Create button");
                    all_ProcessorsHelper.ClickElement("SaveBtn");
                    all_ProcessorsHelper.WaitForWorkAround(3000);
                    
                    executionLog.Log("TSYSOwnerIdentificationType", "Go to All Merchants");
                    VisitOffice("clients");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSOwnerIdentificationType", "Click on First Client");
                    office_ClientsHelper.ClickElement("Client1");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSOwnerIdentificationType", "Go to Company Details Tab");
                    office_ClientsHelper.ClickElement("CompanyDetailsTab");
                    office_ClientsHelper.WaitForWorkAround(2000);

                    executionLog.Log("TSYSOwnerIdentificationType", "Select TSYS Processor");
                    office_ClientsHelper.Select("Processor", "TSYS");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSOwnerIdentificationType", "Go to Owners Tab");
                    office_ClientsHelper.ClickElement("OwnerTab");
                    office_ClientsHelper.WaitForWorkAround(3000);

                    executionLog.Log("TSYSOwnerIdentificationType", "Click on Owner Identification drop down");
                    office_ClientsHelper.ClickElement("OwnerIdentificationType");

                    executionLog.Log("TSYSOwnerIdentificationType", "Verify options under drop down");
                    office_ClientsHelper.IsElementVisible("//option[@value='Driver&#039;s License']");
                    Console.WriteLine("Driver's Licence option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='State ID']");
                    Console.WriteLine("State ID option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='Passport']");
                    Console.WriteLine("Passport option present");
                    office_ClientsHelper.IsElementVisible("//option[@value='Miltary ID']");
                    Console.WriteLine("Miltary ID option present");
                }

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TSYSOwnerIdentificationType");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("TSYS Owner Identification Type");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("TSYS Owner Identification Type", "Bug", "Medium", "Office Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("TSYS Owner Identification Type");
                        TakeScreenshot("TSYSOwnerIdentificationType");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TSYSOwnerIdentificationType.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TSYSOwnerIdentificationType");
                        string id = loginHelper.getIssueID("TSYS Owner Identification Type");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TSYSOwnerIdentificationType.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("TSYS Owner Identification Type"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("TSYS Owner Identification Type");
                //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("TSYSOwnerIdentificationType");
                executionLog.WriteInExcel("TSYS Owner Identification Type", Status, JIRA, "Office Merchant");
            }
            
        }
    }
}
