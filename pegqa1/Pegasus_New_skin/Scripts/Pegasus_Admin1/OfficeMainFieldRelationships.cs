/* Documented by Khalil Shakir 

The OfficeMainFieldRealationships.cs file is a test which will view the fields in the
Office Main account and check any changes that take place in fields that rely on others
to be activated. It will also validate that those fields exitst.

*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using PegasusTests.Locators;
using System.IO;

namespace Pegasus_New_skin.Scripts.Pegasus_Admin
{

    // Creating Test Class
    [TestClass]
    public class OfficeMainFieldRelationships : DriverTestCase
    {

        // Creating the Test Method
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin1")]
        public void checkFieldRelationshipsOfficeMain()
        {
            string[] username = null;
            string[] password = null;


            //Connecting XML Documents
            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");


            //Initializing the objects
            ExecutionLog executionLog = new ExecutionLog();
            LoginHelper loginHelper = new LoginHelper(GetWebDriver());
            Office_ClientsHelper clienthelper = new Office_ClientsHelper(GetWebDriver());

            // Testing Variables including JIRA variables 
            var name = "Testing Subject" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";
            try
            {

                // Initiating Test

                Login(username[0], password[0]);

                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OfficeMainFieldRelationships", " Testing A Client");
                VerifyTitle("Dashboard");

                executionLog.Log("OfficeMainFieldRelationships", " Testing A Client");
                VisitOffice("clients");

                executionLog.Log("OfficeMainFieldRelationships", " Testing A Client");
                clienthelper.ClickElement("Client1");

                executionLog.Log("OfficeMainFieldRelationships", "Wait");
                clienthelper.WaitForWorkAround(2000);

                // Testing Rates and Fees Relationships

                executionLog.Log("OfficeMainFieldRelationships", "Clicking tabRateFees");
                clienthelper.ClickElement("TabRatesandFees");

                executionLog.Log("OfficeMainFieldRelationships", "Wait");
                clienthelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeMainFieldRelationships", "Clicking tabRateFees");
                clienthelper.ClickElement("SelectProcessorRF");

                executionLog.Log("OfficeMainFieldRelationships", "Wait");
                clienthelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeMainFieldRelationships", "Clicking tabRateFees");
                clienthelper.ClickElement("FirstDataNorthOption");

                executionLog.Log("OfficeMainFieldRelationships", "Wait");
                clienthelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeMainFieldRelationships", " Testing A Client");
                clienthelper.IsElementPresent("DiscountFrequency");

                executionLog.Log("OfficeMainFieldRelationships", "Clicking tabRateFees");
                clienthelper.ClickElement("SelectProcessorRF");

                executionLog.Log("OfficeMainFieldRelationships", "Wait");
                clienthelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeMainFieldRelationships", "Clicking tabRateFees");
                clienthelper.ClickElement("FirstDataOmahaOption");

                executionLog.Log("OfficeMainFieldRelationships", "Wait");
                clienthelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeMainFieldRelationships", " Testing A Client");
                clienthelper.IsElementPresent("DiscountCollected");

                // Testing Marketting Tab relationships 

                executionLog.Log("OfficeMainFieldRelationships", "Clicking tabRateFees");
                clienthelper.ClickElement("ClickOnMarketingTab");

                executionLog.Log("OfficeMainFieldRelationships", "Wait");
                clienthelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeMainFieldRelationships", " Testing A Client");
                clienthelper.IsElementPresent("SelectDidMerchantChooseOffice");

                executionLog.Log("OfficeMainFieldRelationships", "Wait");
                clienthelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeMainFieldRelationships", "DidMerchantChooseOffice-Yes");
                clienthelper.ClickElement("DidMerchantChooseOffice-Yes");

                executionLog.Log("OfficeMainFieldRelationships", "Wait");
                clienthelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeMainFieldRelationships", "PreviousMerchantAccnt");
                clienthelper.ClickElement("PreviousMerchantAccnt");

                executionLog.Log("OfficeMainFieldRelationships", "Wait");
                clienthelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeMainFieldRelationships", "PreviousMerchantAccnt-Yes");
                clienthelper.ClickElement("PreviousMerchantAccnt-Yes");

                executionLog.Log("OfficeMainFieldRelationships", "Wait");
                clienthelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeMainFieldRelationships", "PreviousMerchantNumber");
                clienthelper.IsElementPresent("PreviousMerchantNumber");

                executionLog.Log("OfficeMainFieldRelationships", "Wait");
                clienthelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeMainFieldRelationships", "DidMerchantChooseOffice-No");
                clienthelper.ClickElement("DidMerchantChooseOffice-No");

                executionLog.Log("OfficeMainFieldRelationships", "Wait");
                clienthelper.WaitForWorkAround(2000);

                executionLog.Log("OfficeMainFieldRelationships", "ReasonForNotChoosing");
                clienthelper.IsElementPresent("ReasonForNotChoosing");
                // Beginning Aslam's code for JIRA
            }

            catch (Exception e)
            {
                Console.WriteLine("ERRROROOR");
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OfficeMainFieldRelationships");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("OfficeMainFieldRelationships");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("OfficeMainFieldRelationships", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("OfficeMainFieldRelationships");
                        TakeScreenshot("OfficeMainFieldRelationships");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeMainFieldRelationships.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OfficeMainFieldRelationships");
                        string id = loginHelper.getIssueID("Jtable Common Layout OfficeMain");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OfficeMainFieldRelationships.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Jtable Common Layout OfficeMain"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Jtable Common Layout OfficeMain");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("OfficeMainFieldRelationships");
                executionLog.WriteInExcel("Jtable Common Layout OfficeMain", Status, JIRA, "Activities");
            }
        }

        // Ending Aslam's code for JIRA
    }
}

