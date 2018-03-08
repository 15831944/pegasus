/* Documented by Khalil Shakir
* 
* The CorpFieldRelationships script tests the Pegasus Corporate Portal.
* It goes through specific sections of the Corp Portal where selecting certain options for certain fields
* will automatically reveal new fields in Pegasus. 
* CorpFieldRelationships.xml file stores the locators for this script. 
*/


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace Pegasus_New_skin.Scripts.Pegasus_Corp
{
    // Creating Test Class
    [TestClass]
    public class CorpFieldRelationships : DriverTestCase
    {

        // Creating the Test Method
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void checkCorpFieldRelationshps()
        {

            string[] username = null;
            string[] password = null;

            //Connecting XML Documents

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects

            var loginHelper = new LoginHelper(GetWebDriver());
            var fieldHelper = new CorpFieldRHelper(GetWebDriver());
            var executionLog = new ExecutionLog();

            // Testing Variables
            var name = "Testing Subject" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                //Beginning Test
                Login(username[0], password[0]);

                //Checking Residual Income Relationship

                //       VisitCorp("rir/master_revenue/create");

                executionLog.Log("CorpFieldRelationships", "Hover ResidualIncomeTab over");
                fieldHelper.MouseHover("ResidualIncomeTab");

                executionLog.Log("CorpFieldRelationships", "Hover MasterDataSubTab over");
                fieldHelper.MouseHover("MasterDataSubTab");

                executionLog.Log("CorpFieldRelationships", "Click RevenueShareSetup");
                fieldHelper.clickJS("RevenueShareSetup");
                fieldHelper.WaitForWorkAround(5000);

                executionLog.Log("CorpFieldRelationships", "Verify the page title");
                fieldHelper.VerifyPageText("Residual Master Rules - Revenue Share");

                executionLog.Log("CorpFieldRelationships", "Select the processor Type");
                fieldHelper.selectByText("ProcessorType", "First Data North");
                fieldHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpFieldRelationships", "Check if NewThemeOfficeRevPercentage is present");
                fieldHelper.IsElementPresent("//*[@id='text1']");

                // Checking Settings Relationship
                executionLog.Log("CorpFieldRelationships", "Testing Settings Tab Relationships");

                executionLog.Log("CorpFieldRelationships", "Hover over System");
                fieldHelper.MouseHover("System");

                executionLog.Log("CorpFieldRelationships", "Click Settings");
                fieldHelper.clickJS("Settings");
                fieldHelper.WaitForWorkAround(3000);

                //   VisitCorp("settings");

                executionLog.Log("CorpFieldRelationships", "Verify the title of the page");
                fieldHelper.VerifyPageText("Global Settings");

                fieldHelper.selectByText("Country", "Canada");
                fieldHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpFieldRelationships", "Check if CanadaDollar is present");
                fieldHelper.IsElementPresent("CurrenyCanada");

                fieldHelper.selectByText("Country", "United States");
                fieldHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpFieldRelationships", "Check if USDollar  is present");
                fieldHelper.IsElementPresent("CurrencyUS");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("Jtable_Commom_Layout_Admin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Jtable Common Layout OfficeMain");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Jtable Common Layout OfficeMain", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Jtable Common Layout OfficeMain");
                        TakeScreenshot("Jtable_Commom_Layout_Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Jtable_Commom_Layout_Admin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("Jtable_Commom_Layout_Admin");
                        string id = loginHelper.getIssueID("Jtable Common Layout OfficeMain");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Jtable_Commom_Layout_Admin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Jtable Common Layout OfficeMain"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Jtable Common Layout OfficeMain");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("Jtable_Commom_Layout_Admin");
                executionLog.WriteInExcel("Jtable Common Layout OfficeMain", Status, JIRA, "Activities");
            }
        }
        // Ending Aslam's code for JIRA 

    }

}

