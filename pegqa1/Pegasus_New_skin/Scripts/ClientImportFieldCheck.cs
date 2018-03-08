using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientImportFieldCheck : DriverTestCase
    {
        void mapFields(ClientImportFieldCheckHelper helper)
        {
            helper.SelectByText("MapBusinessDBA", "Business DBA Name");
            helper.WaitForWorkAround(1000);
            helper.SelectByText("MapBusinessLegal","Business Legal Name");
            helper.WaitForWorkAround(1000);
            helper.SelectByText("MapStatus","Status");
            helper.WaitForWorkAround(1000);
            helper.Select("MapAddress1", "client_details.location_address_line_1");
            helper.WaitForWorkAround(1000);
            helper.Select("MapAddress2", "client_details.location_address_line_2");
            helper.WaitForWorkAround(1000);
            helper.Select("MapCity", "client_details.location_city");
            helper.WaitForWorkAround(1000);
            helper.Select("MapState", "client_details.location_state");
            helper.WaitForWorkAround(1000);
            helper.Select("MapCountry", "client_details.location_country");
            helper.WaitForWorkAround(1000);
            helper.ClickElement("ImportMap");

        }
        void checkField(String path, ClientImportFieldCheckHelper helper, ExecutionLog log)
        {
            
            var readImport = new StreamReader(File.OpenRead(path));
            var line = readImport.ReadLine();
            while(!readImport.EndOfStream)
            {
                line = readImport.ReadLine();
                if (line.Length == 0)
                {
                    continue;
                }
                var seperateValues = line.Split(',');
                VisitOffice("clients");
                helper.TypeText("SearchClient", seperateValues[0]);
                helper.WaitForWorkAround(2000);
                helper.clickByText(seperateValues[0]);
                helper.WaitForWorkAround(2000);
                helper.ClickElement("CompanyDetailsTab");
                helper.WaitForWorkAround(2000);
                log.Log("ImportClientsField", "check BusinessLegalName"+ seperateValues[1]);
                helper.VerifyText("BusinessLegalName", seperateValues[1]);
                log.Log("ImportClientsField", "check AddressLine1" + seperateValues[3]);
                helper.VerifyText("AddressLine1", seperateValues[3]);
                log.Log("ImportClientsField", "check AddressLine2" + seperateValues[4]);
                helper.VerifyText("AddressLine2", seperateValues[4]);
                log.Log("ImportClientsField", "check City" + seperateValues[5]);
                helper.VerifyText("City", seperateValues[5]);
                log.Log("ImportClientsField", "check State"+ seperateValues[6]);
                helper.VerifyValue("State", seperateValues[6]);
                log.Log("ImportClientsField", "check Country"+ seperateValues[7]);
                helper.VerifyValue("Country", seperateValues[7]);
                log.Log("ImportClientsField", "delete client");
                deleteClients(seperateValues[0],helper);
            }
        }
        
        void deleteClients(String name, ClientImportFieldCheckHelper helper)
        {
            VisitOffice("clients");
            helper.TypeText("SearchClient", name);
            helper.WaitForWorkAround(3000);
            helper.ClickByTitle(name);
            helper.ClickElement("Delete");
            helper.WaitForWorkAround(2000);
            helper.AcceptAlert();
            helper.WaitForWorkAround(3000);
        }
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        public void importClientsFromCSV()
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
            var ClientImportFieldCheckHelper = new ClientImportFieldCheckHelper(GetWebDriver());

            // Variable

            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";
           // try
           // {
                executionLog.Log("ImportClientsField", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);
   
            executionLog.Log("ImportClientsField", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("ImportClientsField", "Click on Clients tab");
            VisitOffice("clients");
            ClientImportFieldCheckHelper.WaitForWorkAround(2000);

            executionLog.Log("ImportClientsField", "Click On Import");
            ClientImportFieldCheckHelper.ClickElement("ImportTab");
            ClientImportFieldCheckHelper.WaitForWorkAround(2000);

            executionLog.Log("ImportClientsField", "Upload file");
            var Path = GetPathToFile() + "clientfieldCheck.csv";
            ClientImportFieldCheckHelper.Upload("SelectFile", Path);
            ClientImportFieldCheckHelper.WaitForWorkAround(2000);

            executionLog.Log("ImportClientsField", "Click On Import");
            ClientImportFieldCheckHelper.ClickElement("ClickOnImportClint");
            ClientImportFieldCheckHelper.WaitForWorkAround(2000);

            executionLog.Log("ImportClientsField", "Map import fields");
            mapFields(ClientImportFieldCheckHelper);
            executionLog.Log("ImportClientsField", "start check");
            checkField(Path, ClientImportFieldCheckHelper, executionLog);

            // }
            //  catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("ImportClientsFromCSV");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Import Clients From CSV");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 5)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Import Clients From CSV", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Import Clients From CSV");
            //            TakeScreenshot("ImportClientsFromCSV");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\ImportClientsFromCSV.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 5)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("ImportClientsFromCSV");
            //            string id = loginHelper.getIssueID("Import Clients From CSV");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\ImportClientsFromCSV.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Import Clients From CSV"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Import Clients From CSV");
            //    executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("ImportClientsFromCSV");
            //    executionLog.WriteInExcel("Import Clients From CSV", Status, JIRA, "Client Management");
            // }
        }
    }
}